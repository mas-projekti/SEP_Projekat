import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from 'src/app/services/auth.service';
import { AUTHENTICATION_HEADER, environment, PAYMENT_CARD_API } from 'src/environments/environment';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {
  public user: any = {};
  public loadPasswordDiv: boolean = false;
  public invalidPasswordChange: boolean = false;
  public invalidText: string = "";
  public isAdmin: boolean = false;
  public users: any = [];
   @ViewChild('pf') public cardForm!: NgForm;
  userId: number = 0;

  constructor(private router: Router, private http: HttpClient, private route: ActivatedRoute, private jwtHelper: JwtHelperService, private datepipe: DatePipe) { }

  ngOnInit(): void {
    let token = localStorage.getItem('jwt')?.toString();
    let decodedToken = this.jwtHelper.decodeToken(token);
    let role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    this.userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    this.user = {
      name: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
      lastName: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"],
      role: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
    }
    console.log(this.user);
    this.isAdmin = role === "Admin";


    if (this.isAdmin) this.getAllUsers();

    let userName = this.route.snapshot.paramMap.get("username"); 

    // this.getUser(userName);
  }

  getUser(username : any): void {
    this.http.get(environment.USER_API + username , AUTHENTICATION_HEADER())
    .subscribe({
      next: (resp) => {
        this.user = resp;
      },
      error: (err) => {
        this.router.navigate([".."]);
      }
    });
  }

  getAllUsers() {
    this.http.get(environment.USER_API , AUTHENTICATION_HEADER())
    .subscribe({
      next: (resp) => {
        this.users = resp;
      },
      error: (err) => {
        this.router.navigate([".."]);
      }
    });
  }

  togglePassword() {
    this.loadPasswordDiv = !this.loadPasswordDiv;
  }

  createNewCard() {
    this.cardForm.resetForm();
    this.loadPasswordDiv = false;
    this.loadPasswordDiv = true;  
    
    this.http.post(PAYMENT_CARD_API() + this.userId, AUTHENTICATION_HEADER())
    .subscribe({
      next: (resp : any) => {
        let cardNumberString: string = resp.cardNumber;
        let beautifulCardNumber = cardNumberString.substring(0,4) + '-' + cardNumberString.substring(4,8) + '-' + cardNumberString.substring(8,12) + '-' + cardNumberString.substring(12,16);
        let dateTimeString =  new Date(resp.exipiringDate);
        let formatedDateTime = this.datepipe.transform(dateTimeString, "dd.MM.yyyy. HH:mm:ss");
        this.cardForm.setValue(
          { 
            cardNumber: beautifulCardNumber,
            cardHolderName: resp.cardHolderName,
            cardHolderLastName: resp.cardHolderLastName,
            cardSecurityCode: resp.securityCode,
            expiringDate: formatedDateTime
          }
        );
      },
      error: () => {

      }
    })
  }

}

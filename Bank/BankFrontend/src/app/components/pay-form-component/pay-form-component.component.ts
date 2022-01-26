import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { PAY_API } from 'src/environments/environment';

@Component({
  selector: 'app-pay-form-component',
  templateUrl: './pay-form-component.component.html',
  styleUrls: ['./pay-form-component.component.css']
})
export class PayFormComponentComponent implements OnInit {
invalidRegister: number = 0;

  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
  }

  pay(form: NgForm) {
    if (form.value.cardHolderName === "" || form.value.cardHolderLastName === "" || form.value.cardNumber === "" || form.value.cardSecurityCode === "") {
      this.invalidRegister = 4;
      return;
    }
    console.log(form.value);
    let ccn: string = form.value.cardNumber;
    ccn = ccn.replace("-", "").replace("-", "").replace("-", "");
    console.log(ccn);
    const credentials = {
      'cardHolderName': form.value.cardHolderName,
      'cardHolderLastName': form.value.cardHolderLastName,
      'cardNumber': ccn,
      'securityCode': form.value.cardSecurityCode
    }

    console.log(credentials);
    this.http.post(PAY_API(), credentials)
    .subscribe({ 
      next: (resp) => {
        this.invalidRegister = 0;
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.log(err);
        this.invalidRegister = 2;
      }
    }); 


  }


}

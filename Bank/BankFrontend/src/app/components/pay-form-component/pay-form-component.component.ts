import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PAY_API } from 'src/environments/environment';

@Component({
  selector: 'app-pay-form-component',
  templateUrl: './pay-form-component.component.html',
  styleUrls: ['./pay-form-component.component.css']
})
export class PayFormComponentComponent implements OnInit {
  invalidRegister: number = 0;
  public errorText: string = "";
  paymentId: number = 0;
  constructor(private router: Router, private http: HttpClient, private route: ActivatedRoute) {
    this.paymentId = +this.route.snapshot.paramMap.get("paymentId")!; 
   }

  ngOnInit(): void {
  }

  pay(form: NgForm) {
    if (form.value.cardHolderName === "" || form.value.cardHolderLastName === "" || form.value.cardNumber === "" || form.value.cardSecurityCode === "") {
      this.invalidRegister = 4;
      return;
    }
    // Parsing Credit Card Number 
    let ccn: string = form.value.cardNumber;
    ccn = ccn.replace("-", "").replace("-", "").replace("-", "");

    // Parsing VALID THRU Date
    let dateString: string = <string>(form.value.cardValidDate);
    let parts: Array<string> = dateString.split("/");
    let year: number = 2000 + +parts[1];
    let month: number = +parts[0];
    let dateParse: Date = new Date(year, month, 0);

    const credentials = {
      'cardHolderName': form.value.cardHolderName,
      'cardHolderLastName': form.value.cardHolderLastName,
      'cardNumber': ccn,
      'securityCode': form.value.cardSecurityCode,
      'exipiringDate': dateParse,
      'paymentId': this.paymentId
    }

    this.http.post(PAY_API(), credentials)
    .subscribe({ 
      next: (resp: any) => {
        this.invalidRegister = 0;
        window.open(resp.successURL)//Moras uzeti successURL iz responsa
        //window.open("http://localhost:3000/transaction-passed/955ea526-4e4a-44c8-c223-08d9b998c036","_self");
      },
      error: (err) => {
        console.log(err);
        this.invalidRegister = 2;
        this.errorText = err.error;
      }
    }); 


  }


}

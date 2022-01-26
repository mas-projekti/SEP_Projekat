import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { environment, REGISTER_API } from 'src/environments/environment';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {
  invalidRegister: number = 0;

  constructor(private router: Router, private http: HttpClient, private authService: AuthService) { }

  ngOnInit(): void {
  }

  register(form: NgForm) {
    if (form.value.password === "" || form.value.confirmPassword === "" || form.value.username === "" || form.value.name === "" || form.value.surname === "") {
      this.invalidRegister = 4;
      return;
    }

    if (form.value.password !== form.value.confirmPassword) {
      this.invalidRegister = 2;
      return;
    }

    if (!form.value.clientType) {
      this.invalidRegister = 3;
      return;
    }

    const credentials = {
      'username': form.value.username,
      'password': form.value.password,
      'name': form.value.name,
      'lastName': form.value.lastname,
      'clientType': form.value.clientType ? 'Merchant' : 'Standard'
    }

    this.http.post(REGISTER_API(), credentials)
    .subscribe({ 
      next: (resp) => {
        this.invalidRegister = 0;
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.log(err);
        this.invalidRegister = 1;
      }
    }); 


  }

}

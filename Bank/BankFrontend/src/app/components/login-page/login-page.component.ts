import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core'
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { environment, LOGIN_API } from 'src/environments/environment';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  invalidLogin: boolean = false;

  constructor(private router: Router, private http: HttpClient, private authService: AuthService) {}

  ngOnInit(): void {
  }

  login(form: NgForm) {
    let credentials = {
      'username': form.value.username,
      'password': form.value.password
    }

    this.http.post(LOGIN_API(), credentials)
    .subscribe({ 
      next: (resp:any) => {
        if (!resp.isSuccess)  {
          console.log('Not Successful');
          return;
        }
        let token = resp.token;
        localStorage.removeItem("jwt");
        localStorage.setItem("jwt", token);
        this.invalidLogin = false;
        this.authService.sendTokenChangeEvent();
        this.router.navigate(['/']);
      },
      error: () => {
        this.invalidLogin = true;
      }
    }); 


  }

}

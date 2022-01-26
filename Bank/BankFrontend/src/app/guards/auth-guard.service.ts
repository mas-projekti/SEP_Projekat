import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth.service';
import { AUTHENTICATION_HEADER } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate{
  private MINUTES: number = 10  // SET HOW ON MUCH MINUTES UNTIL IT EXPIRES TO REFRESH THE TOKEN 
  constructor(private jwtHelper: JwtHelperService, private router: Router, private authService: AuthService, private http: HttpClient) { 

  }

  canActivate() {
    var token = localStorage.getItem("jwt");

      if (token && !this.jwtHelper.isTokenExpired(token)) {     
        return true;
      }
      else if (this.jwtHelper.isTokenExpired(token?.toString())) {
        localStorage.removeItem("jwt");
        this.authService.sendTokenChangeEvent();
      }
      
      localStorage.removeItem("jwt");
      this.router.navigate(["login"]);
      return false;
  }


  tryRefereshingToken() {

  }
}

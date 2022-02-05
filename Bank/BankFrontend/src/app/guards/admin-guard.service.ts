import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AUTHENTICATION_HEADER, environment } from 'src/environments/environment';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuardService {
  private MINUTES: number = 10  // SET HOW ON MUCH MINUTES UNTIL IT EXPIRES TO REFRESH THE TOKEN 

  constructor(private jwtHelper: JwtHelperService, private router: Router, private authService: AuthService, private http: HttpClient) { }

  canActivate() {
      let token = localStorage.getItem("jwt");

      if (token && !this.jwtHelper.isTokenExpired(token)) {     
        return true;
      }
      else if (this.jwtHelper.isTokenExpired(token?.toString())) {
        localStorage.removeItem("jwt");
        this.authService.sendTokenChangeEvent();
      }
      this.router.navigate(["login"]);
      return false;

  }

  isAdmin(token: string) {
    try {
      let decodedToken = this.jwtHelper.decodeToken(token);
      let role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      return role === "Admin";
    }
    catch {
      this.router.navigate(["login"]);
      return false;
    }
  }
}

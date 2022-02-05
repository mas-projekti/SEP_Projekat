import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class LoginAndRegisterGuardService {

  constructor(private jwtHelper: JwtHelperService, private router: Router) { }

  canActivate() {
    try {
      let token = localStorage.getItem("jwt");

      if (!(token && !this.jwtHelper.isTokenExpired(token))) {
        return true;
      }
      return false;
    }
    catch {
      this.router.navigate([""]);
      return false;
    }
    
    
  }
}

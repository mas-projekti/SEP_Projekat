import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean = false;
  username: string = '';
  isAdmin: boolean = false;
  loggedInEventSubscription: Subscription = new Subscription();

  public env: any = environment;


  constructor(private router: Router, private authService: AuthService, private jwtHelper: JwtHelperService) {
    this.isUserAuthenticated();
    this.loggedInEventSubscription = this.authService.getTokenChangeEvent().subscribe({
      next: () => {
        this.isUserAuthenticated();
      }
    });
    
   }

  ngOnInit(): void {
  }

  isUserAuthenticated() {
    try {
      let token: string | null = localStorage.getItem("jwt");
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        this.isLoggedIn = true;
        this.decodeToken(token);
        return true;
      }
      else {
        this.isLoggedIn = false;
        return false;
      }
    }
    catch {
      this.router.navigate(["login"]);
      return false;
    }
    
  }

  logOut() {
    localStorage.removeItem("jwt");
    this.isLoggedIn = false;
    this.router.navigate(["/login"]);
  }

  decodeToken(token: string) {
    try {
      let decodedToken = this.jwtHelper.decodeToken(token);
      this.username = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      let role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      this.isAdmin = role === "Admin";
    }
    catch {
      localStorage.removeItem("jwt");
      this.router.navigate(["login"]);
    }
    
  }

  navigateToProfile() {
    this.router.navigate(["profile", this.username]);
  }
}

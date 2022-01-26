import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { AdminGuardService } from './guards/admin-guard.service';
import { AuthGuardService } from './guards/auth-guard.service';
import { LoginAndRegisterGuardService } from './guards/login-and-register-guard.service';
import { PayFormComponentComponent } from './components/pay-form-component/pay-form-component.component';

const routes: Routes = [
  { path: '', component: HomePageComponent },

  { path: 'login', component: LoginPageComponent, canActivate: [LoginAndRegisterGuardService] },
  { path: 'register', component: RegisterPageComponent, canActivate: [LoginAndRegisterGuardService]},


  { path: 'profile/:username', component: ProfilePageComponent, canActivate: [AuthGuardService] },

  //payment
  { path: 'payment/card/pay', component: PayFormComponentComponent}

];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes, { enableTracing: false })],
  exports: [RouterModule],
  providers: [AuthGuardService, AdminGuardService, LoginAndRegisterGuardService]
})
export class AppRoutingModule { }

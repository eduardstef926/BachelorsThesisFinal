import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatPageComponent } from './chat-page/chat-page.component';
import { EmployeePageComponent } from './employee-page/employee-page.component';
import { LocationPageComponent } from './locations-page/locations-page.component';
import { ForgotPasswordComponent } from './login-page/forgot-password/forgot-password.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { ModifyPasswordComponent } from './login-page/modify-password/modify-password.component';
import { SignUpPageComponent } from './login-page/sign-up-page/sign-up-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { SecondPhaseComponent } from './second-phase/second-phase.component';
import { SymptomPageComponent } from './symptom-page/symptom-page.component';
import { DoctorDetailsComponent } from './doctor-details/doctor-details.component';

const routes: Routes = [
  {
    path: '',
    component: MainPageComponent,
  },
  {
    path: 'main',
    component: MainPageComponent,
  },
  {
    path: 'main/:userId',
    component: MainPageComponent,
  },
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    path: 'sign-up',
    component: SignUpPageComponent,
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent,
  },
  {
    path: 'locations',
    component: LocationPageComponent,
  },
  {
    path: 'employees',
    component: EmployeePageComponent,
  },
  {
    path: 'modify-password/:userId',
    component: ModifyPasswordComponent,
  },
  {
    path: 'chat',
    component: ChatPageComponent,
  },
  {
    path: 'symptom-page',
    component: SymptomPageComponent,
  },
  {
    path: 'second-phase',
    component: SecondPhaseComponent,
  },
  {
    path: 'doctor-details/:firstName/:lastName',
    component: DoctorDetailsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

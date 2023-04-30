import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoggedUserDto } from '../model/loginUser.model';
import { AuthService } from '../services/auth.service';
import { LocalStorageService } from '../services/localstorage.service';
import * as Fingerprint2 from 'fingerprintjs2';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {  
  loginErrorMessage = false;
  inputErrorMessage = false;
  pacientSelected = true;
  doctorSelected = false;

  formControl = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });
  
  getPassword() {
    return this.formControl.get('password')?.value;
  }

  getEmail() {
    return this.formControl.get('email')?.value;
  }

  constructor(private router: Router,
              private userService: AuthService,
              private cookieService: CookieService,
              private snackBar: MatSnackBar,
              private localStorage: LocalStorageService) {}
  
  ngOnInit(): void {
  }

  switchToDoctorLogin() {
    if (this.pacientSelected) {
      this.pacientSelected = false;
      this.doctorSelected = true;
    } 
  }

  switchToPatientLogin() {
    if (!this.pacientSelected) {
      this.pacientSelected = true;
      this.doctorSelected = false;
    } 
  }

  logInAsPacient() {
    if (
      this.getPassword().length == 0 || 
      this.getEmail().length == 0
    ) {
      this.inputErrorMessage = true;
    } else {
      Fingerprint2.get((components: any) => {
        const values = components.map((component: any) => component.value);
        const identifier = Fingerprint2.x64hash128(values.join(''), 31);
        const loggedUser = {
          password: this.getPassword(), 
          email: this.getEmail(),
          name: "user",
          identifier: identifier
        } as LoggedUserDto;

        this.userService.login(loggedUser).subscribe((data: any) => {
            console.log(this.cookieService);
            this.cookieService.set(data.identifier, data.dateTime);
            this.localStorage.set("loggedUserEmail", loggedUser.email);
            this.router.navigate(['/main']);
          },
          (errors) => {
            if (errors.status == 400) {
                this.loginErrorMessage = true;
            }
          }
        );
      });
    }
  }

  showMessage(message: string): void {
    this.snackBar.open(message, 'X');
  }

  createAccount(event : Event) {
    event.preventDefault();
    this.router.navigate(['/sign-up']);
  }

  forgotPassword(event : Event) {
    event.preventDefault();
    this.router.navigate(['/forgot-password']);
  }

  closeErrorMessage() {
    if (this.loginErrorMessage) { 
      this.loginErrorMessage = false;
    } else if (this.inputErrorMessage) {
      this.inputErrorMessage = false;
    }
  }
}

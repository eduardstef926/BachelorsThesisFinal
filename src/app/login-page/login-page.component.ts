import { Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoggedUserDto } from '../model/loginUser.model';
import { AuthService } from '../services/auth.service';
import { LocalStorageService } from '../services/localstorage.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {  
  loginErrorMessage = false;
  inputErrorMessage = false;

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

  constructor(
    private router: Router,
    private userService: AuthService,
    private snackBar: MatSnackBar,
    private localStorage: LocalStorageService,
  ) {}
  
  ngOnInit(): void {}

  logIn() {
    const password = this.getPassword();
    const email = this.getEmail();
  
    if (password.length === 0 || email.length === 0) {
      this.inputErrorMessage = true;
    } else {
      const loggedUser: LoggedUserDto = {
        password,
        email
      };
  
      this.userService.login(loggedUser).subscribe(
        (data: any) => {
          window.scrollTo(0, 0);
          this.localStorage.set("loggedUserId", data);
          this.router.navigate(['']).then(() => {
            setTimeout(() => {
              window.location.reload();
            }, 5);
          });
        },
        (errors) => {
          if (errors.status === 400) {
            this.loginErrorMessage = true;
          }
        }
      );
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

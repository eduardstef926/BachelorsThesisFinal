import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserDto } from '../../model/user.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent implements OnInit {
  confirmationErrorMessage = false;
  inputErrorMessage = false;

  formControl = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    phoneNumber: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmedPassword: new FormControl(''),
  });

  getFirstName() {
    return this.formControl.get('firstName')?.value;
  }

  getLastName() {
    return this.formControl.get('lastName')?.value;
  }

  getPhoneNumber() {
    return this.formControl.get('phoneNumber')?.value;
  }

  getPassword() {
    return this.formControl.get('password')?.value;
  }

  getConfirmedPassword() {
    return this.formControl.get('confirmedPassword')?.value;
  }

  getEmail() {
    return this.formControl.get('email')?.value;
  }

  constructor(private authService: AuthService,
              private router: Router) {}

  register() {
    if (
      this.getFirstName().length == 0 ||
      this.getLastName().length == 0 ||
      this.getPhoneNumber().length == 0 ||
      this.getPassword().length == 0 ||
      this.getConfirmedPassword().length == 0 ||
      this.getEmail().length == 0
    ) {
      this.inputErrorMessage = true;
    } else if (
      this.getConfirmedPassword() != this.getPassword()
    ) {
      this.confirmationErrorMessage = true;
    } else {
      const user = {
        firstName: this.getFirstName(), 
        lastName: this.getLastName(), 
        phoneNumber: this.getPhoneNumber(), 
        password: this.getPassword(), 
        email: this.getEmail()
      } as UserDto;

      this.authService.register(user).subscribe(() => {
          this.router.navigate(['/main']);
      });
    }
  }

  ngOnInit(): void {
  }

  closeErrorMessage() {
    if (this.confirmationErrorMessage) {
      this.confirmationErrorMessage = false;
    } else if (this.inputErrorMessage) { 
      this.inputErrorMessage = false;
    }
  }
}

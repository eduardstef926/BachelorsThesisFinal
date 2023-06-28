import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserDto } from '../../model/user.model';
import { AuthService } from '../../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent implements OnInit {
  confirmationErrorMessage = false;
  invalidPhoneNumber = false;
  invalidName = false;
  emptyFieldErrorMessage = false;

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

  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {}

  register() {
    const firstName = this.getFirstName();
    const lastName = this.getLastName();
    const phoneNumber = this.getPhoneNumber();
    const password = this.getPassword();
    const confirmedPassword = this.getConfirmedPassword();
    const email = this.getEmail();
    if (
      firstName.length === 0 || 
      lastName.length === 0 || 
      phoneNumber.length === 0 || 
      password.length === 0 || 
      confirmedPassword.length === 0 || 
      email.length === 0
    ) {
      this.emptyFieldErrorMessage = true;
    } else if (password !== confirmedPassword) {
      this.confirmationErrorMessage = true;
    } else if (/[a-zA-Z]/.test(phoneNumber)) {
      this.invalidPhoneNumber = true;
    } else if (/[^a-zA-Z]/.test(firstName) || /[^a-zA-Z]/.test(lastName)) {
      this.invalidName = true;
    } else {
      this.addUser();
    }
  }

  addUser() {
    const newUser = {
      firstName: this.getFirstName(), 
      lastName: this.getLastName(), 
      phoneNumber: this.getPhoneNumber(), 
      password: this.getPassword(), 
      email: this.getEmail()
    } as UserDto;

    this.authService.register(newUser).subscribe(() => {
        this.snackBar.open('Successful registration!', 'X', {
          duration: 5000,
          panelClass: ['my-snackbar']
        });
        window.scrollTo(0, 0);
        this.router.navigate(['']);
      }
    );
  }

  closeErrorMessage() {
    if (this.confirmationErrorMessage) {
      this.confirmationErrorMessage = false;
    } else if (this.emptyFieldErrorMessage) { 
      this.emptyFieldErrorMessage = false;
    } else if (this.invalidPhoneNumber) {
      this.invalidPhoneNumber = false;
    } else if (this.invalidName) {
      this.invalidName = false;
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-modify-password',
  templateUrl: './modify-password.component.html',
  styleUrls: ['./modify-password.component.scss']
})
export class ModifyPasswordComponent implements OnInit {
  cookieId!: number;
  confirmationErrorMessage = false;
  inputErrorMessage = false;

  formControl = new FormGroup({
    password: new FormControl(''),
    confirmedPassword: new FormControl(''),
  });

  getPassword() {
    return this.formControl.get('password')?.value;
  }

  getConfirmedPassword() {
    return this.formControl.get('confirmedPassword')?.value;
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.cookieId = Number(params.get('userId'));
    });
  }

  modifyPassword() {
    const password = this.getPassword();
    const confirmedPassword = this.getConfirmedPassword();

    if (password.length === 0 || confirmedPassword.length === 0) {
      this.inputErrorMessage = true;
    } else if (password !== confirmedPassword) {
      this.confirmationErrorMessage = true;
    } else {
      this.authService.modifyPassword(this.cookieId, password)
        .subscribe((response: any) => {
          const message = response ? 'Password modified successfully!' : 'Expired link!';
          this.snackBar.open(message, 'X', {
            duration: 5000,
            panelClass: ['my-snackbar']
          });
          window.scrollTo(0, 0);
          this.router.navigate(['']);
        }
      );
    }
  }
  
  closeErrorMessage() {
    if (this.confirmationErrorMessage) {
      this.confirmationErrorMessage = false;
    } else if (this.inputErrorMessage) {
      this.inputErrorMessage = false;
    }
  }
}

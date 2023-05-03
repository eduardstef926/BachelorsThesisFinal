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
  userId!: number;
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

  constructor(private route: ActivatedRoute,
              private router: Router,
              private snackBar: MatSnackBar,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('userId'));
    });
  }

  modifyPassword() {
    if (
        this.getPassword().length == 0 ||
        this.getConfirmedPassword().length == 0
    ) {
        this.inputErrorMessage = true;
    } 
    else if (
        this.getPassword() != this.getConfirmedPassword()
    ) {
        this.confirmationErrorMessage = true;
    } else {
      this.authService.modifyPassword(this.userId, this.getPassword())
        .subscribe(() => {
          this.snackBar.open('Password modified successfully!', 'X', {
            duration: 5000,
            panelClass: ['my-snackbar']
          });
          window.scrollTo(0, 0);
          this.router.navigate(['']);
        }
      );
    }
  }
  
  deleteErrorMessage() {
    if (this.confirmationErrorMessage) {
      this.confirmationErrorMessage = false;
    } else if (this.inputErrorMessage) {
      this.inputErrorMessage = false;
    }
  }
}

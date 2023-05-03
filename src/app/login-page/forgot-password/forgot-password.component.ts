import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { EmailService } from 'src/app/services/email.service';

@Component({
  selector: 'app-forgout-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  showWindowMessage = false;
  windowMessage = "An email was sent to the address regarding the future steps of the process";

  formGroup = new FormGroup({
    email: new FormControl(''),
  });

  getEmail() {
    return this.formGroup.get('email')?.value;
  }
  
  constructor(private emailService: EmailService) {}

  ngOnInit(): void {
  }

  sendVerificationEmail() {
    this.emailService.sendForgotPasswordEmail(this.getEmail()).subscribe(
      (data) => {
      }
    );
    this.showWindowMessage = true;
  }
}

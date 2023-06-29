import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { UserService } from '../services/user.service';
import { LocalStorageService } from '../services/localstorage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentDto } from '../model/appointment.model';
import { ReviewDto } from '../model/review.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'appointment-review',
  templateUrl: './appointment-review.component.html',
  styleUrls: ['./appointment-review.component.scss'],
  encapsulation: ViewEncapsulation.Emulated,
  animations: [
    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(1000, style({ opacity: 1 }))
      ]),
    ])
  ]
})
export class AppointmentReviewComponent implements OnInit {
  appointment!: AppointmentDto;
  isReviewed!: string;
  appointmentDate!: string;
  evaluationNumber!: number;
  evaluationMessage!: string;
  loginErrorMessage = false;
  loggedIn = false;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    private localStorage: LocalStorageService
  ) {}

  ngOnInit(): void {
    this.loggedIn = this.localStorage.get("loggedIn");
    if (this.loggedIn) {
      const appointmentId = Number(this.route.snapshot.paramMap.get('id'));
      this.userService.getAppointmentById(appointmentId).subscribe((appointment: any) => {
        if (appointment != null) {
          this.appointment = appointment;
          this.isReviewed = appointment.isReviewed;
          this.appointmentDate = appointment.appointmentDate.split('T')[0];
        }
      });  
    } else {
      this.router.navigate(['/login']);
    }
  }

  closeErrorMessage() {
    this.loginErrorMessage = false;
  }

  submitReview() {
    if (this.isValidEvaluationNumber()) {
      const review = this.createReviewDto();
      this.userService.addAppointmentReview(review).subscribe(
        (data: any) => {
          this.handleReviewSuccess();
        },
        (error: any) => {
          this.handleReviewError();
        }
      );
    } else {
      this.loginErrorMessage = true;
    }
  }
  
  isValidEvaluationNumber(): boolean {
    return this.evaluationNumber >= 0 && this.evaluationNumber <= 10 && Number(this.evaluationNumber) == this.evaluationNumber;
  }
  
  createReviewDto(): ReviewDto {
    return {
      cookieId: this.localStorage.get("loggedUserId"),
      appointmentId: this.appointment.appointmentId,
      number: this.evaluationNumber,
      message: this.evaluationMessage,
    };
  }
  
  handleReviewSuccess() {
    this.router.navigate(['']);
    this.snackBar.open('Review was added successfully!', 'X', {
      duration: 3500,
      panelClass: ['my-snackbar']
    });
  }
  
  handleReviewError() {
    this.snackBar.open('Error adding the review!', 'X', {
      duration: 3500,
      panelClass: ['error-snackbar']
    });
  }
}


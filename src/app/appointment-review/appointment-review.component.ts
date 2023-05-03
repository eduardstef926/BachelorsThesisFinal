import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { UserService } from '../services/user.service';
import { LocalStorageService } from '../services/localstorage.service';
import { ActivatedRoute } from '@angular/router';
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
  appointmentDate!: string;
  evaluationNumber!: number;
  evaluationMessage!: string;

  constructor(private userService: UserService,
              private route: ActivatedRoute,
              private snackBar: MatSnackBar,
              private localStorage: LocalStorageService) {}

  ngOnInit(): void {
    const appointmentId = Number(this.route.snapshot.paramMap.get('id'));
    this.userService.getAppointmentById(appointmentId).subscribe((appointment: any) => {
      this.appointment = appointment;
      this.appointmentDate = appointment.appointmentDate.split('T')[0];
    }); 
  }

  submitReview() {
    const review = {
      cookieId: this.localStorage.get("loggedUserId"),
      appointmentId: this.appointment.appointmentId,
      number: this.evaluationNumber,
      message: this.evaluationMessage,
    } as ReviewDto;

    this.userService.addAppointmentReview(review).subscribe(
      (data: any) => {},
      (error: any) => {
        this.snackBar.open('Error adding the review!', 'X', {
          duration: 3500,
          panelClass: ['error-snackbar']
        });
      }
    );
  }
}


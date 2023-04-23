import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from '../services/localstorage.service';
import { animate, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { AppointmentDto } from '../model/appointment.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-appointment-price',
  templateUrl: './appointment-price.component.html',
  styleUrls: ['./appointment-price.component.scss'],
  animations: [
    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(1000, style({ opacity: 1 }))
      ]),
    ])
  ]
})
export class AppointmentPriceComponent implements OnInit {
  appointmentPrice!: number;
  updatedPrice!: number;
  totalPrice!: number;
  
  constructor(private router: Router,
              private userService: UserService,
              private localStorage: LocalStorageService,
              private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    const userEmail = this.localStorage.get("loggedUserEmail");
    this.appointmentPrice = this.localStorage.get("appointment").price;
    this.userService.checkUserSubscription(userEmail).subscribe((data: any) => {
      this.updatedPrice = data == false ? 0 : this.appointmentPrice;
      this.totalPrice = this.appointmentPrice - this.updatedPrice;
    });
  }

  confirmAppointment() {
    const appointment = this.localStorage.get("appointment");
    const appointmentDate = appointment.date.substring(0, 11) + appointment.startTime;
    const newAppointment = {
      appointmentDate: appointmentDate,
      location: appointment.location,
      doctorFirstName: appointment.firstName,
      doctorLastName: appointment.lastName,
      hospitalName: appointment.hospitalName,
      price: this.totalPrice,
      userEmail: this.localStorage.get("loggedUserEmail")
    } as AppointmentDto;

    this.userService.scheduleAppointment(newAppointment).subscribe((data: any) => {
      this.snackBar.open('Successful confirmation!', 'X', {
        duration: 5000,
        panelClass: ['my-snackbar']
      });
      this.router.navigate(['']);
    });
  }

}

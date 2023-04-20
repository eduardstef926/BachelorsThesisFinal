import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from '../services/localstorage.service';
import { DoctorService } from '../services/doctor.service';
import { animate, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { AppointmentDto } from '../model/appointment.model';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  consultationPrice!: number;
  
  constructor(private router: Router,
              private localStorage: LocalStorageService,
              private doctorService: DoctorService,
              private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.consultationPrice = 200;  
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
      price: this.consultationPrice,
      userEmail: this.localStorage.get("loggedUserEmail")
    } as AppointmentDto;
    
    this.doctorService.scheduleAppointment(newAppointment).subscribe((data: any) => {
      this.snackBar.open('Successful confirmation!', 'X', {
        duration: 5000,
        panelClass: ['my-snackbar']
      });
      this.router.navigate(['']);
    });
  }

}

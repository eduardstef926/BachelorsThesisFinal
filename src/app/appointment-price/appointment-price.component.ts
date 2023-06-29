import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from '../services/localstorage.service';
import { animate, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { AppointmentDto } from '../model/appointment.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from '../services/user.service';
import { environment } from 'src/environments/environment';

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
  handler: any = null;
  
  constructor(
    private router: Router,
    private userService: UserService,
    private localStorage: LocalStorageService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    const cookieId = this.localStorage.get("loggedUserId");
    this.appointmentPrice = this.localStorage.get("appointment").price;
    this.userService.checkUserSubscription(cookieId).subscribe((data: any) => {
      this.updatedPrice = data == false ? 0 : this.appointmentPrice;
      this.totalPrice = this.appointmentPrice - this.updatedPrice;
    });
  }

  confirmAppointment() {
    if (this.totalPrice != 0) {
      this.loadStripe();
    } else {
      this.submitAppointment();
    }
  }

  loadStripe() {
    if (!window.document.getElementById('stripe-script')) {
      const s = window.document.createElement('script');
      s.id = 'stripe-script';
      s.type = 'text/javascript';
      s.src = 'https://checkout.stripe.com/checkout.js';
      s.onload = () => {
        this.configureStripeCheckout();
      };
      window.document.body.appendChild(s);
    } else {
      this.configureStripeCheckout();
    }
  }
  
  configureStripeCheckout() {
    this.handler = (<any>window).StripeCheckout.configure({
      key: environment.testKey,
      locale: 'auto',
      token: (stripeToken: any) => {
        this.submitAppointment();
      }
    });
    this.openStripeCheckout();
  }

  openStripeCheckout() {
    if (this.handler) {
      this.handler.open({
        name: 'Medical Appointment',
        description: 'Appointment confirmed',
      });
    }
  }

  submitAppointment() {
    const appointment = this.localStorage.get("appointment");
    const appointmentDate = appointment.date.substring(0, 11) + appointment.startTime;
    const newAppointment = {
      appointmentDate: appointmentDate,
      location: appointment.location,
      firstName: appointment.doctorFirstName,
      lastName: appointment.doctorLastName,
      hospitalName: appointment.hospitalName,
      price: this.totalPrice,
      cookieId: this.localStorage.get("loggedUserId")
    } as AppointmentDto;

    this.userService.scheduleAppointment(newAppointment).subscribe((data: any) => {
      window.scrollTo(0, 0)
      this.snackBar.open('Successful confirmation!', 'X', {
        duration: 5000,
        panelClass: ['my-snackbar']
      });
      this.router.navigate(['/main']);
    });
  }
}

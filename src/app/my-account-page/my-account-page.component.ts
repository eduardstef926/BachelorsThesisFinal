import { Component, OnInit } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';
import { LocalStorageService } from '../services/localstorage.service';
import { UserService } from '../services/user.service';
import { UserDto } from '../model/user.model';
import { AppointmentDto } from '../model/appointment.model';
import { AuthService } from '../services/auth.service';
import * as Fingerprint2 from 'fingerprintjs2';
import { CookieService } from 'ngx-cookie-service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-account-page',
  templateUrl: './my-account-page.component.html',
  styleUrls: ['./my-account-page.component.scss'],
  animations: [
    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(1000, style({ opacity: 1 }))
      ]),
    ])
  ]
})
export class MyAccountPageComponent implements OnInit {
  user!: UserDto;
  firstName!: string;;
  lastName!: string;
  email!: string;
  endDate!: string;
  phoneNumber!: string;
  tableCopy: AppointmentDto[] = [];
  appointments: AppointmentDto[] = [];
  pageSize = 2;
  pageIndex = 0;
  showAppointments = false;
  showSubscription = false;
  paginatorLength = 5;
  accountPages: {
    [key: string]: boolean;
  } = {
    showProfilePage: true,
    showSubscriptionPage: false,
    showAppointmentPage: false,
  };

  constructor(private snackBar: MatSnackBar,
              private router: Router,
              private userService: UserService,
              private authService: AuthService,
              private cookieService: CookieService,
              private localStorage: LocalStorageService) {}

  ngOnInit(): void {
    var userEmail = this.localStorage.get("loggedUserEmail");
    this.userService.getFullUserDataByEmail(userEmail)
      .subscribe((user: any) => {
        this.user = user;
        this.firstName = user.firstName;
        this.lastName = user.lastName;
        this.email = user.email;
        this.phoneNumber = user.phoneNumber;
      }
    )
  }

  updateProfile() {
    var modifiedUser = {
      userId: this.user.userId,
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      phoneNumber: this.phoneNumber
    } as UserDto;

    this.userService.updateUserData(modifiedUser)
      .subscribe((response: any) => {
        this.localStorage.set("loggedUserEmail", this.email);
        this.snackBar.open('Profile updated successfully!', 'X', {
          duration: 5000,
          panelClass: ['my-snackbar']
        });
        window.scrollTo(0, 0);
        this.router.navigate(['']);
    })
  }

  showAccountPage(page: string): void {
    if (page in this.accountPages) {
      this.accountPages[page] = true;
      if (page == "showAppointmentPage") {
        this.userService.getUserAppointmentsByEmail(this.email)
          .subscribe((data: any) => {
            console.log(data);
            this.appointments = data;
            this.tableCopy = data;
            this.showAppointments = true;
            this.renderReviews();
          }
        );
      } else if (page == "showSubscriptionPage") {
        this.userService.getUserSubscriptionByEmail(this.email)
        .subscribe((data: any) => {
            this.endDate = data.endDate.split('T')[0];
            this.showSubscription = true;
          }
        );
      }
    }
    for (const key in this.accountPages) {
      if (key != page) {
        this.accountPages[key] = false;
      }
    } 
  }

  onPageChange(event: any) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.renderReviews();
  }

  renderReviews() {
    const start = this.pageIndex * this.pageSize;
    const end = start + this.pageSize;
    this.appointments = this.tableCopy.slice(start, end);
  }

  logOut() {
    Fingerprint2.get((components: any) => {
      const values = components.map((component: any) => component.value);
      const identifier = Fingerprint2.x64hash128(values.join(''), 31);
      this.authService.logOut(identifier)
        .subscribe((data: any) => {
          this.cookieService.delete(identifier);
          this.localStorage.set("loggedIn", false);
        }
      );
    });
  }

}
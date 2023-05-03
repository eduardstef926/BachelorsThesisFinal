import { Component, OnInit } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';
import { LocalStorageService } from '../services/localstorage.service';
import { UserService } from '../services/user.service';
import { UserDto } from '../model/user.model';
import { AppointmentDto } from '../model/appointment.model';
import { AuthService } from '../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { EmailService } from '../services/email.service';

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
  cookieId!: number;
  confirmationCode!: number;
  phoneNumber!: string;
  pageSize = 2;
  pageIndex = 0;
  paginatorLength = 5;
  accountPages: {
    [key: string]: boolean;
  } = {
    showProfilePage: true,
    showSubscriptionPage: false,
    showAppointmentPage: false,
    showConfirmEmailPage: false,
  };
  showEmailConfirmButton = false;
  showAppointments = false;
  showSubscription = false;
  showEmailConfirmation = false;
  tableCopy: AppointmentDto[] = [];
  appointments: AppointmentDto[] = [];

  constructor(private snackBar: MatSnackBar,
              private router: Router,
              private userService: UserService,
              private authService: AuthService,
              private emailService: EmailService,
              private localStorage: LocalStorageService) {}


  ngOnInit(): void {
    this.cookieId = this.localStorage.get("loggedUserId");
    this.userService.getFullUserDataByCookieId(this.cookieId)
      .subscribe((user: any) => {
        this.user = user;
        this.firstName = user.firstName;
        this.lastName = user.lastName;
        this.email = user.email;
        this.phoneNumber = user.phoneNumber;
        this.showEmailConfirmButton = user.isEmailConfirmed;
      }
    )
  }

  updateProfile() {
    var modifiedUser = {
      userId: this.user.userId,
      email: this.email,
      phoneNumber: this.phoneNumber,
      firstName: this.firstName,
      lastName: this.lastName,
      isEmailConfirmed: this.user.isEmailConfirmed
    } as UserDto;

    this.userService.updateUserData(modifiedUser)
      .subscribe((response: any) => {
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
      switch (page) {
        case "showAppointmentPage":
          this.displayAppointmentPage();
          break;
        case "showSubscriptionPage":
          this.displaySubscriptionPage();
          break;
        case "showConfirmEmailPage":
          this.displayEmailConfirmationPage();
          break;
      }
    }
    for (const key in this.accountPages) {
      if (key != page) {
        this.accountPages[key] = false;
      }
    } 
  }

  displayAppointmentPage() { 
    this.userService.getUserAppointmentsByEmail(this.email).subscribe((data: any) => {
      this.appointments = data;
      this.tableCopy = data;
      this.showAppointments = true;
      this.renderReviews();
    });
  }

  displaySubscriptionPage() {
    this.userService.getUserSubscriptionByEmail(this.email).subscribe((data: any) => {
      this.endDate = data.endDate.split('T')[0];
      this.showSubscription = true;
    });
  }

  displayEmailConfirmationPage() {
    this.emailService.sendEmailConfirmation(this.email)
    .subscribe((data: any) => {});
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

  confirmEmail() {
    this.authService.confirmEmail(this.email, this.confirmationCode)
      .subscribe(
        (data: any) => this.handleConfirmationSucess(),
        (errors) => this.handleConfirmationError()
    );
  }

  handleConfirmationSucess() {
    this.snackBar.open('Email confirmed successfully!', 'X', {
      duration: 5000,
      panelClass: ['my-snackbar']
    });
    window.scrollTo(0, 0);
    this.router.navigate(['']);
  }

  handleConfirmationError() {
    this.snackBar.open('Invalid code!', 'X', {
      duration: 5000,
      panelClass: ['error-snackbar']
    });
  }

  logOut() {
    var id = this.localStorage.get("loggedUserId");
    if (id != 0) {
      this.authService.logOut(id).subscribe((data: any) => {
        this.handleLogOutSuccess();
      });
    }
  }

  handleLogOutSuccess() {
    this.localStorage.set("loggedUserId", 0);
    this.localStorage.set("loggedIn", false);
    this.router.navigate(['']).then(() => {
      setTimeout(() => {
        window.scrollTo(0, 0);
        window.location.reload();
      }, 50);
    });
  }

  cancelSubscription() {
    this.userService.cancelUserSubscription(this.cookieId)
      .subscribe((data: any) => {});
  }

}

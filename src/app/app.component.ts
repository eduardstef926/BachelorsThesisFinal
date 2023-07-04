import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageService } from './services/localstorage.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  loggedIn = false;
  userId!: number;
  emailAddress = 'virtualClinicSupport@gmail.com';
  subject = 'Information Support';

  constructor(
    private localStorage: LocalStorageService,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit() {
    var cookieId = Number(this.localStorage.get("loggedUserId"));
    if (cookieId != -1) {
      this.authService.checkLoginCookie(cookieId).subscribe((response: boolean) => {
        this.localStorage.set("loggedIn", true);
        this.loggedIn = true;
      }, (error: any) => {
        this.localStorage.set("loggedIn", false);
        this.localStorage.set("loggedUserId", -1);
        this.loggedIn = false;
      });
    }
  }

  displayPage(event: Event, page: string) {
    event.preventDefault();
    window.scrollTo(0, 0);
    switch (page) {
      case "locations":
        this.router.navigate(['/locations']);
        break;
      case "employees":
        this.router.navigate(['/employees']);
        break;
      case "subscriptions":
        this.router.navigate(['/subscription']);
        break;
      case "account":
        this.router.navigate(['/account']);
        break;
      case "home":
        this.router.navigate(['']);
        break;
      default:
        this.router.navigate(['']);
        break;
    }
  }
}

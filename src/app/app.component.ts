import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  constructor(private localStorage: LocalStorageService,
              private router: Router,
              private authService: AuthService,
              private route: ActivatedRoute) {}

  ngOnInit() {
    var id = Number(this.localStorage.get("loggedUserId"));
    if (id != 0) {
      this.authService.checkLoginCookie(id).subscribe((response: boolean) => {
        this.localStorage.set("loggedIn", true);
        this.loggedIn = true;
      });
    }
  }

  lookLocationsPage(event : Event) {
    event.preventDefault();
    window.scrollTo(0, 0);
    this.router.navigate(['/locations']);
  }

  lookEmployeePage(event: Event) {
    event.preventDefault();
    window.scrollTo(0, 0);
    this.router.navigate(['/employees']);
  }
  
  lookSubscriptionsPage(event: Event) {
    event.preventDefault();
    window.scrollTo(0, 0);
    this.router.navigate(['/subscription']);
  }

  goHome(event : Event) {
    event.preventDefault();
    this.router.navigate(['']);
  }

  lookAccountPage(event : Event) {
    event.preventDefault();
    this.router.navigate(['/account']);
  }

  login() {
    this.router.navigate(['/login']);
  }
}

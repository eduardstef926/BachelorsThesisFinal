import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalStorageService } from './services/localstorage.service';

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
              private route: ActivatedRoute) {}

  ngOnInit() {
    this.loggedIn = this.localStorage.get("loggedIn");
  }

  lookLocationsPage(event : Event) {
    event.preventDefault();
    this.router.navigate(['/locations']);
  }

  lookEmployeePage(event: Event) {
    event.preventDefault();
    this.router.navigate(['/employees']);
  }
  
  lookSubscriptionsPage(event: Event) {
    event.preventDefault();
    this.router.navigate(['/subscription-page']);
  }

  goHome(event : Event) {
    event.preventDefault();
    this.router.navigate(['']);
  }

  lookAccountPage(event : Event) {
    event.preventDefault();
    this.router.navigate(['/my-account']);
  }

  login() {
    this.router.navigate(['/login']);
  }
}

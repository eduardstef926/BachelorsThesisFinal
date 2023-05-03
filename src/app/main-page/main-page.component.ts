import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalStorageService } from '../services/localstorage.service';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {

  constructor(private router: Router,
              private route: ActivatedRoute,
              private localStorage: LocalStorageService,
              private authService: AuthService) {}

  ngOnInit(): void {
  
  }

  lookEmployers(event : Event) {
    event.preventDefault();
  }

  lookCareers(event : Event) {
    event.preventDefault();
    this.router.navigate(['/careers']);
  }

  lookNews(event : Event) {
    event.preventDefault();
  }

  lookAppointments(event : Event) {
    event.preventDefault();
  }

  lookSubscription(event : Event) {
    event.preventDefault();
  }

  enterMainPage() {
    if (this.localStorage.get("loggedIn")) {
      this.router.navigate(['/symptom']);
    } else {
      this.router.navigate(['/login']);
    }
  }
}

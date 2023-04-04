import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalStorageService } from '../services/localstorage.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
  animations: [
    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(1000, style({ opacity: 1 }))
      ]),
      transition(':leave', [
        style({ opacity: 1 }),
        animate(1000, style({ opacity: 0 }))
      ])
    ])
  ]
})
export class MainPageComponent implements OnInit {

  constructor(private router: Router,
              private route: ActivatedRoute,
              private localStorage: LocalStorageService,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      var number = Number(params.get('userId'));
      if (number != null && number != 0) {
        this.authService.confirmEmail(number).subscribe(() => {});
      }
    });
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
      this.router.navigate(['/chat']);
    } else {
      this.router.navigate(['/login']);
    }
  }
}

import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalStorageService } from '../services/localstorage.service';
import { AuthService } from '../services/auth.service';
import * as Fingerprint2 from 'fingerprintjs2';
import { CookieService } from 'ngx-cookie-service';

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
              private cookieService: CookieService,
              private localStorage: LocalStorageService,
              private authService: AuthService) { }

  ngOnInit(): void {
    Fingerprint2.get((components: any) => {
      const values = components.map((component: any) => component.value);
      const identifier = Fingerprint2.x64hash128(values.join(''), 31);
      if (this.cookieService.check(identifier)) {
        const cookieValue = this.cookieService.get(identifier).split('T')[0].split('-');
        const year = Number(cookieValue[0]);
        const month = Number(cookieValue[1]);
        const day = Number(cookieValue[2]);
        const expirationDate = new Date(year, month - 1, day);
        const today = new Date();
        today.setHours(0, 0, 0, 0);
        this.localStorage.set("loggedIn", today <= expirationDate);
      }
    });
    // this.route.paramMap.subscribe(params => {
    //   var number = Number(params.get('userId'));
    //   if (number != null && number != 0) {
    //     this.authService.confirmEmail(number).subscribe(() => {});
    //   }
    // });
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

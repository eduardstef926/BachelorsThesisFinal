import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from '../services/localstorage.service';
import { UserService } from '../services/user.service';
import { SubscriptionInputDto } from '../model/subscriptionInput.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-subscriptions-page',
  templateUrl: './subscriptions-page.component.html',
  styleUrls: ['./subscriptions-page.component.scss'],
  animations: [
    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(1000, style({ opacity: 1 }))
      ]),
    ])
  ]
})
export class SubscriptionsPageComponent implements OnInit {

  constructor(private userService: UserService,
              private router: Router,
              private localStorage: LocalStorageService) {}

  ngOnInit(): void {
  }

  selectSubscription(length: number) {
    if (!this.localStorage.get("loggedIn")) {
      window.scrollTo(0, 0);
      this.router.navigate(['/login']);
    } else {
      window.scrollTo(0, 0);
      this.router.navigate(['/subscription/payment', length]);
    }
  }
}

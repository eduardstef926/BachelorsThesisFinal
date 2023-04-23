import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from '../services/localstorage.service';
import { UserService } from '../services/user.service';
import { SubscriptionDto } from '../model/subscription.model';

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
              private localStorage: LocalStorageService) {}

  ngOnInit(): void {
  }

  selectSubscription(length: number) {
    var userEmail = this.localStorage.get("loggedUserEmail");
    var subscription = {
      email: userEmail,
      length: length
    } as SubscriptionDto;
    this.userService.addUserSubscription(subscription).subscribe(() => {});
  }
}

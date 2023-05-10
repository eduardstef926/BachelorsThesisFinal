import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from '../services/localstorage.service';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { SubscriptionInputDto } from '../model/subscriptionInput.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';

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
  handler: any = null;
  length!: number;

  constructor(
    private userService: UserService,
    private router: Router,
    private snackBar: MatSnackBar,
    private localStorage: LocalStorageService
  ) {}

  ngOnInit(): void {}

  makePayment(length: any) {
    this.length = length;
    this.loadStripe();
  }

  loadStripe() {
    if (!window.document.getElementById('stripe-script')) {
      const s = window.document.createElement('script');
      s.id = 'stripe-script';
      s.type = 'text/javascript';
      s.src = 'https://checkout.stripe.com/checkout.js';
      s.onload = () => {
        this.configureStripeCheckout();
      };
      window.document.body.appendChild(s);
    } else {
      this.configureStripeCheckout();
    }
  }

  configureStripeCheckout() {
    this.handler = (<any>window).StripeCheckout.configure({
      key: environment.testKey,
      locale: 'auto',
      token: (stripeToken: any) => {
        this.planSubscription();
      }
    });

    this.openStripeCheckout();
  }

  openStripeCheckout() {
    if (this.handler) {
      this.handler.open({
        name: 'Medical Subscription',
        description: 'Subscription confirmed',
      });
    }
  }

  planSubscription() {
    const sessionId = this.localStorage.get('loggedUserId');
    const subscription = {
      cookieId: sessionId,
      length: this.length,
    } as SubscriptionInputDto;

    this.userService.addUserSubscription(subscription).subscribe((data: any) => {
      this.snackBar.open('Successful subscription!', 'X', {
        duration: 5000,
        panelClass: ['my-snackbar']
      });
    });
  }
}

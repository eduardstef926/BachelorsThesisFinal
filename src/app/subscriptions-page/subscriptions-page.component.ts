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
  handler:any = null;
  amount!: number;

  constructor(private userService: UserService,
              private router: Router,
              private snackBar: MatSnackBar,
              private localStorage: LocalStorageService) {}

  ngOnInit(): void {
  }

  makePayment(amount: any) {
    this.amount = amount;
    this.loadStripe();
    const paymentHandler = (<any>window).StripeCheckout.configure({
      key: environment.testKey,
      locale: 'auto',
      token: function (stripeToken: any) {
        alert('Stripe token generated!');
      }
    });
    
    paymentHandler.open({
      name: 'Successful Subscriptio',
      description: 'Subscription confirmed',
      amount: amount,
    });
  }

  loadStripe() {
    if (!window.document.getElementById('stripe-script')) {
      const s = window.document.createElement("script");
      s.id = "stripe-script";
      s.type = "text/javascript";
      s.src = "https://checkout.stripe.com/checkout.js";
      s.onload = () => {
        const localStorageService = this.localStorage; // Store reference to localStorage
        this.handler = (<any>window).StripeCheckout.configure({
          key: environment.testKey,
          locale: 'auto',
          token: (stripeToken: any) => {
            this.planSubscription();
          }
        });
        if (this.handler) {
          this.handler.open({
            name: 'Medical Subscription',
            description: 'Subscription confirmed',
            amount: 1000
          });
        }
      };
      window.document.body.appendChild(s);
    }
  }  

  planSubscription() {
    var sessionId = this.localStorage.get("loggedUserId");
    var subscription = {
      cookieId: sessionId,
      length: this.amount,
    } as SubscriptionInputDto;

    this.userService.addUserSubscription(subscription).subscribe((data: any) => {
      window.scrollTo(0, 0);
      this.snackBar.open('Successful subscription!', 'X', {
        duration: 5000,
        panelClass: ['my-snackbar']
      });
      this.router.navigate(['/main']);
    });
  }

}

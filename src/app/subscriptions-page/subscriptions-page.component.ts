import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from '../services/localstorage.service';
import { UserService } from '../services/user.service';
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
  handler:any = null;

  constructor(private userService: UserService,
              private router: Router,
              private localStorage: LocalStorageService) {}

  ngOnInit(): void {
  }

  makePayment(amount: any) {
    this.loadStripe();
    const paymentHandler = (<any>window).StripeCheckout.configure({
      key: 'pk_test_15RBEwp1zLWhcSqIk2qQRqMm',
      locale: 'auto',
      token: function (stripeToken: any) {
        alert('Stripe token generated!');
      }
    });
    
    paymentHandler.open({
      name: 'Techanical Adda',
      description: '4 Products Added',
      amount: amount * 100,
    });
  }

  loadStripe() {
    if (!window.document.getElementById('stripe-script')) {
      var s = window.document.createElement("script");
      s.id = "stripe-script";
      s.type = "text/javascript";
      s.src = "https://checkout.stripe.com/checkout.js";
      s.onload = () => {
        this.handler = (<any>window).StripeCheckout.configure({
          key: 'pk_test_15RBEwp1zLWhcSqIk2qQRqMm',
          locale: 'auto',
          token: function (token: any) {
            alert('Payment Success!!');
          }
        });
        if (this.handler) {
          this.handler.open({
            name: 'My Store',
            description: 'My product',
            amount: 1000
          });
        }
      }
      window.document.body.appendChild(s);
    }
  }  

}

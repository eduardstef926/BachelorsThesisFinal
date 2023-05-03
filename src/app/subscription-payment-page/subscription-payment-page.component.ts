import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { LocalStorageService } from '../services/localstorage.service';
import * as Fingerprint2 from 'fingerprintjs2';
import { CookieService } from 'ngx-cookie-service';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoggedUserDto } from '../model/loginUser.model';
import { SubscriptionInputDto } from '../model/subscriptionInput.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-subscription-payment-page',
  templateUrl: './subscription-payment-page.component.html',
  styleUrls: ['./subscription-payment-page.component.scss']
})
export class SubscriptionPaymentPageComponent implements OnInit {
  length!: number;
  price!: number;

  formControl = new FormGroup({
    cardNumber: new FormControl(''),
    expirationDate: new FormControl(''),
    ccv: new FormControl('')
  });
  
  getCardNumber() {
    return this.formControl.get('cardNumber')?.value;
  }

  getExpirationDate() {
    return this.formControl.get('email')?.value;
  }

  getCCV() {
    return this.formControl.get('email')?.value;
  }

  constructor(private userService: UserService,
              private router: Router,
              private route: ActivatedRoute,
              private snackBar: MatSnackBar,
              private localStorage: LocalStorageService) {}
  
  ngOnInit(): void {
    this.length = this.route.snapshot.params['length'];
    this.price = (this.length == 1) ? 10 : (this.length == 6) ? 50 : 100;
  }

  confirmPay() {
    var subscription = {
      cookieId: this.localStorage.get("loggedUserId"),
      length: this.length
    } as SubscriptionInputDto;
    
    this.userService.addUserSubscription(subscription).subscribe(() => {
      this.snackBar.open('Successful payment!', 'X', {
        duration: 5000,
        panelClass: ['my-snackbar']
      });
      window.scrollTo(0, 0);
      this.router.navigate(['']);
    });
  }

}

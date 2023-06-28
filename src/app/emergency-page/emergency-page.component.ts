import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { LocalStorageService } from '../services/localstorage.service';

@Component({
  selector: 'app-emergency-page',
  templateUrl: './emergency-page.component.html',
  styleUrls: ['./emergency-page.component.scss'],
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
export class EmergencyPageComponent implements OnInit {
  phoneNumber = '+40 0758922350';
  diseaseName = "";

  constructor(
    private userService: UserService,
    private localStorage: LocalStorageService
  ) {}

  ngOnInit(): void {
    var cookieId = this.localStorage.get("loggedUserId");
    this.userService.getLastDiagnosticByUserEmail(cookieId)
    .subscribe((diagnostic: any) => {
        this.diseaseName = diagnostic.diseaseName.replace(/_/g, ' ').toLowerCase();
      }
    );
  }

  callNumber() {
    window.location.href = 'whatsapp://send?phone=' + this.phoneNumber;
  }

}

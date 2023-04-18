import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
interface Food {
  value: string;
  viewValue: string;
}


@Component({
  selector: 'app-appointment-page',
  templateUrl: './appointment-page.component.html',
  styleUrls: ['./appointment-page.component.scss']
})
export class AppointmentPageComponent implements OnInit {
  selectSortingMethod!: string;
  sortings = ["s1", "s2", "s3"];
  
  constructor() {}

  formControl = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  ngOnInit(): void {
  }

}

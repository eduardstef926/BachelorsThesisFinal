import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { CoreService } from '../services/core.service';
import { LocalStorageService } from '../services/localstorage.service';
import { AuthService } from '../services/auth.service';
import { UserDto } from '../model/user.model';

@Component({
  selector: 'app-symptom-page',
  templateUrl: './symptom-page.component.html',
  styleUrls: ['./symptom-page.component.scss'],
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
export class SymptomPageComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  symptomList!:Map<String, Boolean>;
  listCopy!:Array<String>;
  symptomArray!: any;
  selectedSymptom!: string;
  isChecked = false;
  totalItems: number = 100;
  pageSize: number = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  constructor(private router: Router,
              private authService: AuthService,
              private coreService: CoreService,
              private localStorage: LocalStorageService) { }

  ngOnInit(): void {
    this.symptomList = new Map<String, Boolean>();
    this.symptomList.set("Headache", false);
    this.symptomList.set("Aaurel", false);
    this.symptomList.set("Aaurel2", false);
    this.symptomList.set("Aaure3", false);
    this.symptomList.set("Aaurel43", false);
    this.symptomList.set("Aaurel24", false);
    this.symptomList.set("Aaurel25", false);
    this.symptomArray = Array.from(this.symptomList);
    this.listCopy =  this.symptomArray;
  }

  getValue(symptom: string) {
    return this.symptomList.get(symptom);
  }

  filterSymptoms() {
    if (this.selectedSymptom.length != 0) {
      this.symptomArray = this.listCopy.filter((symptom: any) => {
        return symptom[0].includes(this.selectedSymptom);
      });
    } else {
      this.symptomArray = this.listCopy;
    }
  }

  moveNext() {
    var selectedSymptoms = this.symptomArray.filter((symptom: any) => {
      return symptom[1] == true;
    });
    var symptomNames = selectedSymptoms.map((symptom:any) => symptom[0]);
    var loggedUserEmail = this.localStorage.get('loggedUserEmail');
    this.authService.getSymptomData(symptomNames, loggedUserEmail).subscribe(() => {
    });
  }

}

import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { CoreService } from '../services/core.service';
import { LocalStorageService } from '../services/localstorage.service';
import { AuthService } from '../services/auth.service';
import { SymptomDto } from '../model/symptom.model';

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
  symptomSelection!: Map<String, Boolean>;
  listCopy!: Array<String>;
  symptomList!: any;
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
    this.symptomSelection = new Map<String, Boolean>();
    this.coreService.getAllSymptoms().subscribe((symptoms: SymptomDto[]) => {
      this.symptomList = symptoms.map((symptom) => [symptom.name, false]);
      this.listCopy =  this.symptomList;
    })
  }

  getValue(symptom: string) {
    return this.symptomSelection.get(symptom);
  }

  filterSymptoms() {
    if (this.selectedSymptom.length != 0) {
      this.symptomList = this.listCopy.filter((symptom: any) => 
        symptom[0].includes(this.selectedSymptom)
      );
    } else {
      this.symptomList = this.listCopy;
    }
  }

  moveNext() {
    const selectedSymptoms = this.symptomList
      .filter((symptom: any) => symptom[1])
      .map((symptom: any) => symptom[0]);

    const loggedUserEmail = this.localStorage.get('loggedUserEmail');
    this.authService
      .addUserSymptoms(selectedSymptoms, loggedUserEmail)
      .subscribe(() => {
        this.router.navigate(['/second-phase']);
    });
  }
}

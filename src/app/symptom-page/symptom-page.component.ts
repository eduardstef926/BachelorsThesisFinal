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
  symptomList!: any;
  selectedSymptom!: string;
  filteredTableCopy: string[] = [];
  tableCopy: string[] = [];
  pageSize = 5;
  pageIndex = 0;

  constructor(private router: Router,
              private authService: AuthService,
              private coreService: CoreService,
              private localStorage: LocalStorageService) {}
  
  ngOnInit(): void {
    this.coreService.getAllSymptoms().subscribe((symptoms: SymptomDto[]) => {
      this.symptomList = symptoms.map((symptom) => [symptom.name, false]);
      this.tableCopy =  this.symptomList;
      this.filteredTableCopy = this.symptomList;
      this.renderSymptoms();
    })
  }

  onPageChange(event: any) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.renderSymptoms();
  }

  renderSymptoms() {
    const start = this.pageIndex * this.pageSize;
    const end = start + this.pageSize;
    this.symptomList = this.filteredTableCopy.slice(start, end);
  }

  filterSymptoms() {
    if (this.selectedSymptom.length != 0) {
      this.filteredTableCopy = this.tableCopy.filter((symptom: any) => 
        symptom[0].toLowerCase().includes(this.selectedSymptom.toLowerCase())
      );
      this.symptomList = this.filteredTableCopy;
    } else {
      this.symptomList = this.tableCopy;
      this.filteredTableCopy = this.tableCopy;
    }
    this.renderSymptoms();
  }

  submitSymptoms() {
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
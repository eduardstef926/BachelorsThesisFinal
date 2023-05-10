import { animate, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { LocalStorageService } from '../services/localstorage.service';
import { SymptomDto } from '../model/symptom.model';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  loadingComplete = new EventEmitter<void>(); 
  listSize!: number;
  showEmptyMessage = false;
  selectedSymptoms = [];
  partialSymptomName = "";
  pageSize = 5;
  currentIndex = -1;
  pageDictionary!: Map<number, any[]>;
  isLoading: boolean = false;

  constructor(private router: Router,
              private userService: UserService,
              private localStorage: LocalStorageService,
              private snackBar: MatSnackBar) {}
  
  ngOnInit(): void {
    this.pageDictionary = new Map<number, any[]>();
    this.onPageChange(0);
  }

  onPageChange(pageIndex: number) {
    if (pageIndex >= this.currentIndex) {
      this.userService.getFilterSymptomsPaginated(this.partialSymptomName.toLowerCase(), pageIndex)
        .subscribe((symptoms: SymptomDto) => {
          var symptomList = symptoms.symptoms.map((symptom) => [symptom, false]);
          this.currentIndex += 1;
          if (!this.pageDictionary.has(this.currentIndex)) {
            this.pageDictionary.set(this.currentIndex, symptomList);
            this.listSize = symptoms.number;
          }
        }
      );
    } else {
      this.currentIndex -= 1;
    }
  } 

  filterSymptoms() {
    const pageIndex = 0;
    const partialSymptomName = this.partialSymptomName.toLowerCase();
    this.userService.getFilterSymptomsPaginated(partialSymptomName, pageIndex)
      .subscribe((symptoms: SymptomDto) => {
        if (this.partialSymptomName.length != 0) {
          const symptomList = symptoms.symptoms.map((symptom) => [symptom, false]);
          this.pageDictionary.set(this.currentIndex, symptomList);
          this.listSize = symptoms.number;
          this.showEmptyMessage = (symptoms.symptoms.length === 0);
          this.paginator.pageIndex = 0;
        } else {
          this.showEmptyMessage = false;
          this.pageDictionary.clear();
          this.onPageChange(0);
        }
      }
    );
  }

  submitSymptoms() {
    this.isLoading = true;
    const filteredValues = [];
    const symptomList = Array.from(this.pageDictionary.values())[0];
    for (let i=0; i<symptomList.length; ++i) {
      filteredValues.push(symptomList[i][0]);
    }
    this.snackBar.dismiss();
    this.checkSymptoms(filteredValues);
  }

  checkSymptoms(filteredValues: any) {
    const cookieId = this.localStorage.get('loggedUserId');
    this.userService.addUserSymptoms(cookieId, filteredValues).subscribe(() => {
        this.isLoading = false;  
        this.loadingComplete.emit();
        this.router.navigate(['/severity']);
    });
  }
}
import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewChild } from '@angular/core';
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
  listSize!: number;
  isFiltered = false;
  showEmptyMessage = false;
  symptomList!: any;
  partialSymptomName = "";
  filteredTableCopy: string[] = [];
  tableCopy: string[] = [];
  pageSize = 5;

  constructor(private router: Router,
              private userService: UserService,
              private localStorage: LocalStorageService,
              private snackBar: MatSnackBar) {}
  
  ngOnInit(): void {
    this.onPageChange(0);
  }

  onPageChange(pageIndex: number) {
    this.userService.getFilterSymptomsPaginated(this.partialSymptomName.toLowerCase(), pageIndex)
      .subscribe((symptoms: SymptomDto) => {
        this.symptomList = symptoms.symptoms.map((symptom) => [symptom, false]);
        this.listSize = symptoms.number;
      }
    );
  }

  filterSymptoms() {
    const pageIndex = 0;
    this.userService.getFilterSymptomsPaginated(this.partialSymptomName.toLowerCase(), pageIndex)
      .subscribe((symptoms: SymptomDto) => {
        this.symptomList = symptoms.symptoms.map((symptom) => [symptom, false]);
        this.listSize = symptoms.number;
        symptoms.symptoms.length == 0 ? this.showEmptyMessage = true : this.showEmptyMessage = false;
        this.partialSymptomName.length != 0 ? this.isFiltered = true : this.isFiltered = false;
        this.paginator.pageIndex = 0;
      }
    );
  }

  submitSymptoms() {
    const selectedSymptoms = this.symptomList
      .filter((symptom: any) => symptom[1])
      .map((symptom: any) => symptom[0]);
      
    if (selectedSymptoms.length < 5) {
      this.snackBar.open('Not Enough Symptoms Selected!', 'X', {
        duration: 5000,
        panelClass: ['snackbar']
      }); 
    } else {
      this.snackBar.dismiss();
      const cookieId = this.localStorage.get('loggedUserId');
      this.userService.addUserSymptoms(cookieId, selectedSymptoms)
        .subscribe(() => {
          this.router.navigate(['/severity']);
        }
      );
    }
  }
}
import { Component, OnInit } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-severity-page',
  templateUrl: './severity-page.component.html',
  styleUrls: ['./severity-page.component.scss'],
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
export class SeverityPageComponent implements OnInit {
  severityLevels = ["Very Severe", "Severe", "Mild"];
  selectedSeverity = '';
  selected = false;
  
  constructor(
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
  }

  onCheckboxChange(severity: string) {
    this.selected = true;
    this.selectedSeverity = severity;
  }

  submitSeverity() {
    if (this.selectedSeverity.length == 0) {
      this.snackBar.open('No Level Selected', 'X', {
        duration: 5000,
        panelClass: ['snackbar']
      });  
    } else {
      this.snackBar.dismiss();
      this.selectedSeverity == "Very Severe" 
          ? this.router.navigate(['/emergency']) : this.router.navigate(['/diagnostic']);
    }
  }
}

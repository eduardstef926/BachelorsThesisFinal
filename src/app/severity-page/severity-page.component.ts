import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { animate, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';

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
  selectedSeverity!: string;
  selected = false;
  
  constructor(private router: Router) {}

  ngOnInit(): void {
  }

  onCheckboxChange(severity: string) {
    this.selected = true;
    this.selectedSeverity = severity;
  }

  moveNext() {
    if (this.selectedSeverity == "Very Severe") {
      this.router.navigate(['/emergency-page']);
    } else {
      this.router.navigate(['/diagnostic-page']);
    }
  }
}

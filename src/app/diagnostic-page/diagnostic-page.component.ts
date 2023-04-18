import { Component, OnInit, ViewChild } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { EmployeeService } from '../services/employee.service';
import { LocalStorageService } from '../services/localstorage.service';
import { CoreService } from '../services/core.service';
import { DiagnosticDto } from '../model/diagnostic.model';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'diagnostic-page',
  templateUrl: './diagnostic-page.component.html',
  styleUrls: ['./diagnostic-page.component.scss'],
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
export class DiagnosticPageComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  diseaseName!: string;
  doctorTitle!: string;
  diagnostic!: DiagnosticDto;
  doctorTable!: MatTableDataSource<any>;
  paginatorLength = 5;

  constructor(private employeeService: EmployeeService,
              private coreService: CoreService,
              private localStorage: LocalStorageService) { }

  ngOnInit(): void {
    var userEmail = this.localStorage.get('loggedUserEmail');
    this.coreService.getLastDiagnosticByUserEmail(userEmail).subscribe((diagnostic:any) => {
      this.diagnostic = diagnostic;
      this.doctorTitle = diagnostic.doctorTitle.toLowerCase();
      this.diseaseName = diagnostic.diseaseName.replace(/_/g, ' ').toLowerCase();
      this.loadDoctors(diagnostic.doctorSpecialization);
    });
  }

  loadDoctors(specialization: string) {
    this.employeeService.getDoctorsBySpecialization(specialization).subscribe((doctors:any) => {
      this.doctorTable = new MatTableDataSource(doctors);
      this.doctorTable.paginator = this.paginator;
    })
  }
}

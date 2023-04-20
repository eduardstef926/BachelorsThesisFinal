import { Component, OnInit } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';
import { DoctorService } from '../services/doctor.service';
import { LocalStorageService } from '../services/localstorage.service';
import { CoreService } from '../services/core.service';
import { DiagnosticDto } from '../model/diagnostic.model';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

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
    ])
  ]
})
export class DiagnosticPageComponent implements OnInit {
  selectedLocation!: string;
  diseaseName!: string;
  doctorTitle!: string;
  diagnostic!: DiagnosticDto;
  locations: string[] = [];
  
  dateRange = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
  });

  getStart() {
    return this.dateRange.get('start')?.value;
  }

  getEnd() {
    return this.dateRange.get('end')?.value;
  }

  constructor(private doctorService: DoctorService,
              private coreService: CoreService,
              private localStorage: LocalStorageService,
              private router: Router) { }
              

  ngOnInit(): void {
    var userEmail = this.localStorage.get('loggedUserEmail');
    this.coreService.getLastDiagnosticByUserEmail(userEmail)
      .subscribe((diagnostic:any) => {
        this.diagnostic = diagnostic;
        this.doctorTitle = diagnostic.doctorTitle.toLowerCase();
        this.diseaseName = diagnostic.diseaseName.replace(/_/g, ' ').toLowerCase();
        this.loadAppointmentLocations();
    });
  }

  loadAppointmentLocations() {
    this.doctorService.getDoctorLocationsBySpecialization(this.diagnostic.doctorSpecialization)
      .subscribe((locations:any) => {
        this.locations = locations;
    })
  }

  convertDate(date: Date) {
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var day = date.toString().split(' ')[2];
    return `${year}-${month}-${day}`;
  }

  listAppointmentDates() {
    this.localStorage.set("startDate",this.convertDate(this.getStart()));
    this.localStorage.set("endDate", this.convertDate(this.getEnd()));
    this.localStorage.set("location", this.selectedLocation);
    this.router.navigate(['/appointment-page/list']);
  }
}

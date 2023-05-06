import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DoctorService } from '../services/doctor.service';
import { LocalStorageService } from '../services/localstorage.service';
import { animate, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-appointment-slotlist',
  templateUrl: './appointment-slotlist.component.html',
  styleUrls: ['./appointment-slotlist.component.scss'],
  animations: [
    trigger('fade', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(1000, style({ opacity: 1 }))
      ]),
    ])
  ]
})
export class AppointmentSlotlistComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  appointmentSlots!: MatTableDataSource<any>;
  paginatorLength = 5;
  
  constructor(private doctorService: DoctorService,
              private localStorage: LocalStorageService,
              private router: Router) { }

  ngOnInit(): void {
    var appointmentRange = this.localStorage.get("appointmentRange");
    this.doctorService.getAppointmentDatesByDateSpecializationAndLocation(appointmentRange)
      .subscribe((appointmentSlots) => {
        this.appointmentSlots = new MatTableDataSource(appointmentSlots);
        this.appointmentSlots.paginator = this.paginator;
    });
  }

  moveToFinalPage(element: any) {
    this.localStorage.set("appointment", element);
    this.router.navigate(['/appointment/confirmation']);
  }
}

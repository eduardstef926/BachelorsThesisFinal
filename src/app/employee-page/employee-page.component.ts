import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { DoctorDto } from '../model/doctor.model';
import { EmployeeService } from '../services/employee.service';
import { animate, style, transition, trigger } from '@angular/animations';
import { City } from '../model/city.model';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.scss'],
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
export class EmployeePageComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  doctorTable!: MatTableDataSource<any>;
  cities: City[] = [];
  tableCopy: DoctorDto[] = [];
  lastIndex!: number;
  selectedValue!: string;
  paginatorLength!: number;

  formControl = new FormGroup({
    doctorName: new FormControl(''),
  });

  getDoctorName() {
    return this.formControl.get('doctorName')?.value;
  }

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.lastIndex = 0;
    this.employeeService.getAllDoctors().subscribe((doctors) => {
      this.doctorTable = new MatTableDataSource(doctors);
      this.tableCopy = doctors; 
      doctors.forEach((doctor) => {
        var isValid = this.cities.some(x => x.viewValue == doctor.location);
        if (!isValid) {
          this.cities.push({"value":  String(this.lastIndex), "viewValue" : doctor.location})
          this.lastIndex ++;
        }
      });
    });
  }

   filterValues() {
    var name = this.getDoctorName();
    var city = this.selectedValue != null ? this.cities[Number(this.selectedValue)].viewValue : null;
    this.doctorTable.data = this.tableCopy.filter((doctor) => {
      return (
        (doctor.location === city || city == null) &&
        (doctor.firstName.includes(name) || doctor.lastName.includes(name) || name == null)
      );
    });
  }
}

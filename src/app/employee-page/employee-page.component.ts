import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { DoctorDto } from '../model/doctor.model';
import { DoctorService } from '../services/doctor.service';
import { animate, style, transition, trigger } from '@angular/animations';
import { City } from '../model/city.model';
import { MatPaginator } from '@angular/material/paginator';
import { specialization } from '../model/specialization.model';

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
  specializations: specialization[] = [];
  sortings: string[] = [];
  tableCopy: DoctorDto[] = [];
  cityIndex!: number;
  specializationIndex!: number;
  selectedCity!: string;
  selectedSpecialization!: string;
  selectSortingMethod = "Sort";
  paginatorLength = 10;

  formControl = new FormGroup({
    doctorName: new FormControl('')
  });

  selectedOption = new FormControl('option1');

  getDoctorName() {
    return this.formControl.get('doctorName')?.value;
  }

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    this.cityIndex = 0;
    this.specializationIndex = 0;
    this.sortings = ['by name', 'by rating'];
    this.doctorService.getAllDoctors().subscribe((doctors) => {
      this.doctorTable = new MatTableDataSource(doctors);
      this.tableCopy = doctors; 
      this.doctorTable.paginator = this.paginator;
      this.fillTable();
    });
  }

  fillTable() {
    this.tableCopy.forEach((doctor: DoctorDto) => {
      const isCityPresent = this.cities.some(x => x.viewValue == doctor.location);
      const isSpecializationPresent = this.specializations.some(x => x.viewValue == doctor.specialization);
      if (!isCityPresent) {
        this.cities.push({"value": String(this.cityIndex), "viewValue": doctor.location})
        this.cityIndex ++;
      }
      if (!isSpecializationPresent) {
        this.specializations.push({"value":  String(this.cityIndex), "viewValue": doctor.specialization})
        this.specializationIndex ++;
      }
    });
  }

  applyChanges() {
    var name = this.getDoctorName();
    var city = this.selectedCity != null ? this.cities[Number(this.selectedCity)].viewValue : null;
    var specialization = this.selectedSpecialization != null ? this.specializations[Number(this.selectedSpecialization) - 1].viewValue : null;
    this.doctorTable.data = this.tableCopy.filter((doctor) => {
      return (
        (doctor.location === city || city == null) &&
        (doctor.specialization === specialization || specialization == null) &&
        (doctor.firstName.includes(name) || doctor.lastName.includes(name) || name == null)
      );
    });
    this.applySorting();
  }

  applySorting() {
    if (this.selectSortingMethod == "by name") {
      this.doctorTable.data = this.doctorTable.data.sort((a: DoctorDto, b: DoctorDto) => {
        return a.firstName.localeCompare(b.firstName);
      });
    } else {
      this.doctorTable.data = this.doctorTable.data.sort((a: DoctorDto, b: DoctorDto) => {
        return b.rating - a.rating;
      });
    }
  }
}

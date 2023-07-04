import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { DoctorDto } from '../model/doctor.model';
import { DoctorService } from '../services/doctor.service';
import { animate, style, transition, trigger } from '@angular/animations';
import { City } from '../model/city.model';
import { MatPaginator } from '@angular/material/paginator';
import { Specialty } from '../model/specialization.model';

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
  specializations: Specialty[] = [];
  initialTable: DoctorDto[] = [];
  tableCopy: DoctorDto[] = [];
  sortings: string[] = [];
  cityIndex!: number;
  specialtyIndex!: number;
  selectedCity = "";
  selectedSpecialization = "";
  selectSortingMethod = "Sort";
  paginatorLength = 10;
  pageIndexes: number[] = [];
  currentIndex = 0;
  pageSize = 5;
  isFiltered = false;

  formControl = new FormGroup({
    doctorName: new FormControl('')
  });
  
  getDoctorName() {
    return this.formControl.get('doctorName')?.value;
  }

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    this.cityIndex = 0;
    this.specialtyIndex = 0;
    this.sortings = ['by name', 'by evaluation'];
    this.doctorService.getAllDoctors().subscribe((doctors) => {
      this.tableCopy = doctors;
      this.renderData(doctors);
      this.setFilterParameters();
    });
  }

  setFilterParameters() {
    this.initialTable.forEach((doctor: DoctorDto) => {
      const isCityPresent = this.cities.some(x => x.viewValue == doctor.location);
      const isSpecialtyPresent = this.specializations.some(x => x.viewValue == doctor.specialization);
      if (!isCityPresent) {
        this.cities.push({"value": String(this.cityIndex), "viewValue": doctor.location})
        this.cityIndex ++;
      }
      if (!isSpecialtyPresent) {
        this.specializations.push({"value":  String(this.specialtyIndex), "viewValue": doctor.specialization})
        this.specialtyIndex ++;
      }
    });
  }

  renderData(doctors: any) {
    this.pageIndexes = [];
    const numberLength = doctors.length % this.pageSize == 0 ? doctors.length / this.pageSize : doctors.length / this.pageSize + 1;
    for(let i=1; i<=numberLength; i++) {
      this.pageIndexes.push(i);
    }
    this.doctorTable = new MatTableDataSource(doctors.slice(this.currentIndex, this.currentIndex + 5));
    if (!this.isFiltered) {
      this.initialTable = doctors; 
    }
  }

  applyFiltering() {
    var name = this.getDoctorName();
    var city = this.selectedCity.length != 0 ? this.cities[Number(this.selectedCity)].viewValue : null;
    var specialty = this.selectedSpecialization.length != 0 ? this.specializations[Number(this.selectedSpecialization)].viewValue: null;
    this.isFiltered = true;
    this.doctorTable.data = this.initialTable.filter((doctor) => {
      return (
        (doctor.location === city || city == null) &&
        (doctor.specialization === specialty || specialty == null) &&
        (doctor.firstName.includes(name) || doctor.lastName.includes(name) || name == null)
      );
    });
    this.applySorting();
  }

  applySorting() {
    if (this.selectSortingMethod == "by name") {
      this.doctorTable.data = this.doctorTable.data.sort((a: DoctorDto, b: DoctorDto) => a.firstName.localeCompare(b.firstName));
    } else if (this.selectSortingMethod == "by evaluation") {
      this.doctorTable.data = this.doctorTable.data.sort((a: DoctorDto, b: DoctorDto) => b.rating - a.rating);
    }
    this.tableCopy = this.doctorTable.data;
    this.renderData(this.doctorTable.data);
  }

  choosePage(index: any) {
    this.doctorTable = new MatTableDataSource(this.tableCopy.slice(5*(index-1), 5*index));
    this.currentIndex = index - 1;
  }

  movePage(direction: string) {
    if (direction == "forward") {
      this.currentIndex += 1;
    } else {
      this.currentIndex -= 1;
    }
    this.doctorTable = new MatTableDataSource(this.tableCopy.slice(5*this.currentIndex, 5*(this.currentIndex + 1)));
  }
}

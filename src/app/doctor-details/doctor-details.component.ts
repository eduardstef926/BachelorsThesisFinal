import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { DoctorDto } from '../model/doctor.model';
import { Chart, registerables } from 'chart.js';
import { DegreeDto } from '../model/college.model';
import { StudyProgram } from '../enums/studyProgram.enum';
import { StudyField } from '../enums/studyField.enum';
import { ReviewDto } from '../model/review.model';

@Component({
  selector: 'app-doctor-details',
  templateUrl: './doctor-details.component.html',
  styleUrls: ['./doctor-details.component.scss']
})
export class DoctorDetailsComponent implements OnInit {
  firstName!: string;
  lastName!: string;
  doctor!: DoctorDto;
  reviewsNumber = 10;
  chart: any;
  degreeList: DegreeDto[] = [];
  reviewList: ReviewDto[] = [];
  studyProgram = StudyProgram;
  studyField = StudyField;
  reviewDictionary = new Map<number, number>();
	
  constructor(private route: ActivatedRoute,
              private employeeService: EmployeeService) { }

  ngOnInit(): void {
    Chart.register(...registerables);
    this.route.params.subscribe(params => {
      this.firstName = params['firstName'];
      this.lastName = params['lastName'];
      this.loadDoctorDetails();
    });
  }

  loadDoctorDetails() {
    this.employeeService.getDoctorByFirstNameAndLastName(this.firstName, this.lastName).subscribe((doctor) => {
      this.doctor = doctor;
      this.employeeService.getDoctorEducationByFirstNameAndLastName(this.firstName, this.lastName).subscribe((degrees) => {
        this.degreeList = degrees;
        this.loadDoctorReviews();
      });
    });
  }

  loadDoctorReviews() {
    this.employeeService.getDoctorReviewsByFirstNameAndLastName(this.firstName, this.lastName).subscribe((reviews: any) => {
      this.reviewList = reviews;
      this.reviewsNumber = reviews.length;
      this.reviewList.forEach((element: any) => {
        const appearences = this.reviewDictionary.get(element.number);
        if (appearences) {
          this.reviewDictionary.set(element.number, appearences + 1);
        } else {
          this.reviewDictionary.set(element.number, 1);
        }
        this.createChart();
      });
    });
  }

  getDictionaryValues() {
    const dictionaryValues = [];
    for (let key=10; key>=1; --key) {
      dictionaryValues.push(this.reviewDictionary.get(key));
    }
    return dictionaryValues;
  }

  createArray(length: number) {
    return Array.from({ length: length/2 }, (_, i) => i + 1);
  }

  splitYear(year: Date) {
    return year.toString().split('-')[0];
  }

  createChart(){
    this.chart = new Chart("MyChart", {
      type: 'bar',
      data: {
        labels: ['10', '9', '8','7', '6', '5', '4', '3', '2', '1'], 
	       datasets: [
          {
            label: "Rating",
            data: [this.getDictionaryValues()],
            backgroundColor: 'blue'
          },
        ]
      },
      options: {
        aspectRatio:2.5
      }
    });
  }

}

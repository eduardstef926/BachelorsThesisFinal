import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DoctorService } from '../services/doctor.service';
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
  pageSize = 5;
  currentIndex = 0;
  pageIndex = 0;
  pageIndexes: number[] = [];
  chart: any;
  firstName!: string;
  lastName!: string;
  specialization!: string;
  hospitalName!: string;
  doctorPosition!: string;
  location!: string;
  doctor!: DoctorDto;
  reviewsEvaluationNumber!: number;
  degreeList: DegreeDto[] = [];
  reviewList: ReviewDto[] = [];
  tableCopy: ReviewDto[] = [];
  studyProgram = StudyProgram;
  studyField = StudyField;
  reviewDictionary = new Map<number, number>();
	
  constructor(
    private route: ActivatedRoute,
    private doctorService: DoctorService
  ) {}

  ngOnInit(): void {
    Chart.register(...registerables);
    this.route.params.subscribe(params => {
      this.firstName = params['firstName'];
      this.lastName = params['lastName'];
      this.loadDoctorDetails();
    });
  }
  
  choosePage(index: any) {
    this.currentIndex = index - 1;
    this.loadDoctorReviews();
  }

  nextPage() {
    this.currentIndex += 1;
    this.loadDoctorReviews();
  }

  prevPage() {
    this.currentIndex -= 1;
    this.loadDoctorReviews();
  }

  loadDoctorDetails() {
    this.doctorService.getDoctorWithEmploymentByFirstNameAndLastNameAsync(this.firstName, this.lastName).subscribe((doctor) => {
      this.doctor = doctor;
      this.doctorPosition = doctor.currentPosition;
      this.hospitalName = doctor.hospitalName;
      this.specialization = doctor.specialization;
      this.location = doctor.location;
      this.doctorService.getDoctorEducationByFirstNameAndLastName(this.firstName, this.lastName).subscribe((degrees) => {
        this.degreeList = degrees;
        this.renderData();
      });
    });
  }

  renderData() {
    this.doctorService.getDoctorReviewLengthByFirstNameAndLastName(this.firstName, this.lastName).subscribe((data: any) => {
      this.reviewsEvaluationNumber = data;
      const numberLength =  this.reviewsEvaluationNumber % this.pageSize == 0 
                          ? this.reviewsEvaluationNumber / this.pageSize 
                          : this.reviewsEvaluationNumber / this.pageSize + 1;
      for(let i=1; i<=numberLength; i++) {
        this.pageIndexes.push(i);
      }
      this.loadDoctorReviews();
    });
  }

  loadDoctorReviews() {
    this.doctorService.getDoctorReviewsPaginatedByFirstNameAndLastName(this.firstName, this.lastName, this.currentIndex).subscribe((reviews: any) => {
      this.reviewList = reviews;
      if (reviews != null) {
        this.tableCopy = reviews;
        this.tableCopy.forEach((element: any) => {
          if (this.reviewDictionary.has(element.number)) {
            const appearences = Number(this.reviewDictionary.get(element.number));
            this.reviewDictionary.set(element.number, appearences + 1);
          } else {
            this.reviewDictionary.set(element.number, 1);
          }
        });
        this.createChart();
      }
    });
  }

  getDictionaryValues() {
    const dictionaryValues = [];
    for (let key=10; key>=1; --key) {
      if (this.reviewDictionary.has(key)) {
        dictionaryValues.push(this.reviewDictionary.get(key));
      } else {
        dictionaryValues.push(0);
      }
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
            data: this.getDictionaryValues(),
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

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
  pageIndex = 0;
  chart: any;
  firstName!: string;
  lastName!: string;
  doctor!: DoctorDto;
  reviewsEvaluationNumber!: number;
  degreeList: DegreeDto[] = [];
  reviewList: ReviewDto[] = [];
  tableCopy: ReviewDto[] = [];
  studyProgram = StudyProgram;
  studyField = StudyField;
  reviewDictionary = new Map<number, number>();
	
  constructor(private route: ActivatedRoute,
              private doctorService: DoctorService) { }

  ngOnInit(): void {
    Chart.register(...registerables);
    this.route.params.subscribe(params => {
      this.firstName = params['firstName'];
      this.lastName = params['lastName'];
      this.loadDoctorDetails();
    });
  }
  
  onPageChange(event: any) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.renderReviews();
  }

  renderReviews() {
    const start = this.pageIndex * this.pageSize;
    const end = start + this.pageSize;
    this.reviewList = this.tableCopy.slice(start, end);
  }

  loadDoctorDetails() {
    this.doctorService.getDoctorWithEmploymentByFirstNameAndLastNameAsync(this.firstName, this.lastName).subscribe((doctor) => {
      this.doctor = doctor;
      this.doctorService.getDoctorEducationByFirstNameAndLastName(this.firstName, this.lastName).subscribe((degrees) => {
        this.degreeList = degrees;
        this.loadDoctorReviews();
      });
    });
  }

  loadDoctorReviews() {
    this.doctorService.getDoctorReviewsByFirstNameAndLastName(this.firstName, this.lastName).subscribe((reviews: any) => {
      this.reviewList = reviews;
      this.tableCopy = reviews;
      this.renderReviews();
      this.reviewsEvaluationNumber = reviews.length;
      this.tableCopy.forEach((element: any) => {
        if (this.reviewDictionary.has(element.number)) {
          const appearences = Number(this.reviewDictionary.get(element.number));
          this.reviewDictionary.set(element.number, appearences + 1);
        } else {
          this.reviewDictionary.set(element.number, 1);
        }
      });
      this.createChart();
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

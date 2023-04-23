import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SymptomDto } from "../model/symptom.model";
import { DiagnosticDto } from "../model/diagnostic.model";
import { AppointmentDto } from "../model/appointment.model";
import { ReviewDto } from "../model/review.model";

@Injectable()
export class UserService{
  
  baseUrl = 'https://localhost:7270/api';

  constructor(private httpClient: HttpClient) {}

  private headers = new HttpHeaders({
    'Content-Type' : 'application/json'
  });

  private options = { headers : this.headers, withCredentials: true};

  addUserSymptoms(symptomNames: Array<string>, loggedUserEmail: string): Observable<any> {
    return this.httpClient.post<any>(
      this.baseUrl + "/User/AddUserSymptoms?email=" + loggedUserEmail + "&symptoms=" + symptomNames.toString(),
      this.options
    );
  }

  addAppointmentReview(review: ReviewDto) {
    const body = JSON.stringify(review);
    return this.httpClient.post<any>(
      this.baseUrl + "/User/AddAppointmentReview" ,
      body,
      this.options
    );
  }

  getAllSymptoms(): Observable<SymptomDto[]> {
    return this.httpClient.get<Array<SymptomDto>>(
      this.baseUrl + "/User/GetAllSymptoms",
      this.options
    );
  }

  getLastDiagnosticByUserEmail(email: string): Observable<DiagnosticDto> {
    return this.httpClient.get<DiagnosticDto>(
      this.baseUrl + "/User/GetLastDiagnosticByUserEmail?email=" + email,
      this.options
    );
  }

  getAppointmentById(id: number): Observable<AppointmentDto> {
    return this.httpClient.get<AppointmentDto>(
      this.baseUrl + "/User/GetAppointmentById?id=" + id,
      this.options
    );
  }
}
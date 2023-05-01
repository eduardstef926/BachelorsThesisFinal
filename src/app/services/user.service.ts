import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SymptomDto } from "../model/symptom.model";
import { DiagnosticDto } from "../model/diagnostic.model";
import { AppointmentDto } from "../model/appointment.model";
import { ReviewDto } from "../model/review.model";
import { SubscriptionInputDto } from "../model/subscriptionInput.model";
import { UserDto } from "../model/user.model";
import { SubscriptionDto } from "../model/subscription.model";

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

  scheduleAppointment(appointment: AppointmentDto): Observable<any>  {
    appointment.appointmentDate = new Date(appointment.appointmentDate);
    const body = JSON.stringify(appointment);
    return this.httpClient.post<any>(
      this.baseUrl + "/User/ScheduleAppointment",
      body,
      this.options
    );
  }

  getUserAppointmentsByEmail(email: string): Observable<Array<AppointmentDto>>  {
    return this.httpClient.get<Array<AppointmentDto>>(
      this.baseUrl + "/User/GetUserAppointments?email=" + email,
      this.options
    );
  }

  getUserSubscriptionByEmail(email: string): Observable<SubscriptionDto> {
    return this.httpClient.get<SubscriptionDto>(
      this.baseUrl + "/User/GetUserSubscription?email=" + email,
      this.options
    );
  }

  addUserSubscription(subscription: SubscriptionInputDto) {
    const body = JSON.stringify(subscription);
    return this.httpClient.post<any>(
      this.baseUrl + "/User/AddUserSubscription",
      body,
      this.options
    );
  }

  checkUserSubscription(email: string) {
    return this.httpClient.get<boolean>(
      this.baseUrl + "/User/CheckUserSubscription?email=" + email,
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

  getFullUserDataByEmail(email: string): Observable<UserDto> {
    return this.httpClient.get<UserDto>(
      this.baseUrl + "/User/GetFullUserDataByEmail?email=" + email,
      this.options
    );
  }

  updateUserData(user: UserDto): Observable<any> {
    const body = JSON.stringify(user);
    console.log(user);
    return this.httpClient.put<any>(
      this.baseUrl + "/User/UpdateUserData",
      body,
      this.options
    );
  }
}
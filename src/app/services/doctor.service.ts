import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DoctorDto } from "../model/doctor.model";
import { DegreeDto } from "../model/college.model";
import { AppointmentSlotDto } from "./appointmentSlot.model";
import { AppointmentDto } from "../model/appointment.model";

@Injectable()
export class DoctorService{
  baseUrl = 'https://localhost:7270/api';

  constructor(private httpClient: HttpClient) {}

  private headers = new HttpHeaders({
    'Content-Type' : 'application/json'
  });

  private options = { headers : this.headers, withCredentials: true};

  getAllDoctors(): Observable<Array<DoctorDto>> {
    return this.httpClient.get<Array<DoctorDto>>(
      this.baseUrl + "/Doctor/GetAllDoctors",
      this.options
    );
  }

  getDoctorAppointmentDatesByDateAndLocation(startDate: string, endDate: string, location: string): Observable<Array<AppointmentSlotDto>> {
    return this.httpClient.get<Array<AppointmentSlotDto>>(
      this.baseUrl + "/Doctor/GetDoctorAppointmentDatesByDateAndLocation?startDate=" + startDate + 
      "&endDate=" + endDate + "&location=" + location,
      this.options
    );
  }

  getDoctorsBySpecialization(specialization: string): Observable<Array<DoctorDto>> {
    return this.httpClient.get<Array<DoctorDto>>(
      this.baseUrl + "/Doctor/GetDoctorsBySpecialization?specialization=" + specialization,
      this.options
    );
  }

  getDoctorLocationsBySpecialization(specialization: string): Observable<Array<string>> {
    return this.httpClient.get<Array<string>>(
      this.baseUrl + "/Doctor/GetDoctorLocationsBySpecialization?specialization=" + specialization,
      this.options
    );
  }

  getDoctorByFirstNameAndLastName(firstName: string, lastName: string): Observable<DoctorDto> {
    return this.httpClient.get<DoctorDto>(
      this.baseUrl + "/Doctor/GetDoctorByFirstNameAndLastName?firstName=" + firstName + 
      "&lastName=" + lastName,
      this.options
    );
  }

  getDoctorEducationByFirstNameAndLastName(firstName: string, lastName: string): Observable<Array<DegreeDto>> {
    return this.httpClient.get<Array<DegreeDto>>(
      this.baseUrl + "/Doctor/GetDoctorDegreeByFirstNameAndLastName?firstName=" + firstName + 
      "&lastName=" + lastName,
      this.options
    );
  }

  getDoctorReviewsByFirstNameAndLastName(firstName: string, lastName: string): Observable<Array<DegreeDto>> {
    return this.httpClient.get<Array<DegreeDto>>(
      this.baseUrl + "/Doctor/GetDoctorReviewsByFirstNameAndLastName?firstName=" + firstName + 
      "&lastName=" + lastName,
      this.options
    );
  }

  scheduleAppointment(appointment: AppointmentDto): Observable<any>  {
    appointment.appointmentDate = new Date(appointment.appointmentDate);
    const body = JSON.stringify(appointment);
    return this.httpClient.post<any>(
      this.baseUrl + "/Doctor/ScheduleAppointment",
      body,
      this.options
    );
  }
}
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DoctorDto } from "../model/doctor.model";
import { DegreeDto } from "../model/college.model";
import { AppointmentSlotDto } from "../model/appointmentSlot.model";
import { AppointmentDto } from "../model/appointment.model";
import { AppointmentRangeDto } from "../model/appointmentRange.model";

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

  getAppointmentDatesByDateSpecializationAndLocation(appointmentRange: AppointmentRangeDto): Observable<Array<AppointmentSlotDto>> {
    return this.httpClient.get<Array<AppointmentSlotDto>>(
      this.baseUrl + "/Doctor/GetAppointmentDatesByDateSpecializationAndLocation?startDate=" + appointmentRange.startDate + 
      "&endDate=" + appointmentRange.endDate + "&location=" + appointmentRange.location + "&specialization=" + appointmentRange.specialization,
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

  getDoctorWithEmploymentByFirstNameAndLastNameAsync(firstName: string, lastName: string): Observable<DoctorDto> {
    return this.httpClient.get<DoctorDto>(
      this.baseUrl + "/Doctor/GetDoctorWithEmploymentByFirstNameAndLastNameAsync?firstName=" + firstName + 
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

  getDoctorReviewsPaginatedByFirstNameAndLastName(firstName: string, lastName: string, pageIndex: number): Observable<Array<DegreeDto>> {
    return this.httpClient.get<Array<DegreeDto>>(
      this.baseUrl + "/Doctor/GetDoctorReviewsPaginatedByFirstNameAndLastName?firstName=" + firstName + 
      "&lastName=" + lastName + "&pageIndex=" + pageIndex,
      this.options
    );
  }

  getDoctorReviewLengthByFirstNameAndLastName(firstName: string, lastName: string): Observable<Array<DegreeDto>> {
    return this.httpClient.get<Array<DegreeDto>>(
      this.baseUrl + "/Doctor/GetDoctorReviewLengthByFirstNameAndLastName?firstName=" + firstName + 
      "&lastName=" + lastName,
      this.options
    );
  }

  getDoctorReviewNumbersByFirstNameAndLastName(firstName: string, lastName: string): Observable<Array<DegreeDto>> {
    return this.httpClient.get<Array<DegreeDto>>(
      this.baseUrl + "/Doctor/GetDoctorReviewNumbersByFirstNameAndLastName?firstName=" + firstName + 
      "&lastName=" + lastName,
      this.options
    );
  }
}
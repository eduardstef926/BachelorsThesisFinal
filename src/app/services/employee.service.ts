import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DoctorDto } from "../model/doctor.model";
import { EngineerDto } from "../model/engineer.model";
import { DegreeDto } from "../model/college.model";

@Injectable()
export class EmployeeService{
  baseUrl = 'https://localhost:7270/api';

  constructor(private httpClient: HttpClient) {}

  private headers = new HttpHeaders({
    'Content-Type' : 'application/json'
  });

  private options = { headers : this.headers, withCredentials: true};

  getAllEngineers(): Observable<Array<EngineerDto>> {
    return this.httpClient.get<Array<EngineerDto>>(
      this.baseUrl + "/Engineer/GetAllEngineers",
      this.options
    );
  }

  getAllDoctors(): Observable<Array<DoctorDto>> {
    return this.httpClient.get<Array<DoctorDto>>(
      this.baseUrl + "/Doctor/GetAllDoctors",
      this.options
    );
  }

  getDoctorsBySpecialization(specialization: string): Observable<Array<DoctorDto>> {
    return this.httpClient.get<Array<DoctorDto>>(
      this.baseUrl + "/Doctor/GetDoctorsBySpecialization?specialization=" + specialization,
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
}
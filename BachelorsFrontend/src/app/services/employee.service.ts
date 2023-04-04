import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DoctorDto } from "../model/doctor.model";
import { EngineerDto } from "../model/engineer.model";

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
}
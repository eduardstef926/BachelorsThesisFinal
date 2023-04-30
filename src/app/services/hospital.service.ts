import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HospitalDto } from "../model/hospital.model";

@Injectable()
export class HospitalService{
  baseUrl = 'https://localhost:7270/api';

  constructor(private httpClient: HttpClient) {}

  private headers = new HttpHeaders({
    'Content-Type' : 'application/json'
  });

  private options = { headers : this.headers, withCredentials: true};

  getAllHospitals(): Observable<Array<HospitalDto>> {
    return this.httpClient.get<Array<HospitalDto>>(
      this.baseUrl + "/Hospital/GetAllHospitals",
      this.options
    );
  }
}
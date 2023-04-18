import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SymptomDto } from "../model/symptom.model";
import { DiagnosticDto } from "../model/diagnostic.model";

@Injectable()
export class CoreService{
  baseUrl = 'https://localhost:7270/api';

  constructor(private httpClient: HttpClient) {}

  private headers = new HttpHeaders({
    'Content-Type' : 'application/json'
  });

  private options = { headers : this.headers, withCredentials: true};

  getSymptomData(symptomList: Array<string>, loggedUserEmail: string): Observable<any> {
    const body = JSON.stringify(symptomList);
    return this.httpClient.post<Array<any>>(
      this.baseUrl + "/Core/GetSymptomData",
      body,
      this.options
    );
  }

  getAllSymptoms(): Observable<SymptomDto[]> {
    return this.httpClient.post<Array<SymptomDto>>(
      this.baseUrl + "/Core/GetAllSymptoms",
      this.options
    );
  }

  getLastDiagnosticByUserEmail(email: string): Observable<DiagnosticDto> {
    return this.httpClient.get<DiagnosticDto>(
      this.baseUrl + "/Core/GetLastDiagnosticByUserEmail?email=" + email,
      this.options
    );
  }
}
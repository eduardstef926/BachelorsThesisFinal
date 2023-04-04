import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

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
    console.log(body);
    return this.httpClient.post<Array<any>>(
      this.baseUrl + "/Core/GetSymptomData",
      body,
      this.options
    );
  }
}
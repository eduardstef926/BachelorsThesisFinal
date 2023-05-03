import { HttpClient, HttpHeaderResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoggedUserDto } from "../model/loginUser.model";
import { UserDto } from "../model/user.model";

@Injectable()
export class EmailService{
  baseUrl = 'https://localhost:7270/api';

  private headers = new HttpHeaders({
    'Content-Type' : 'application/json'
  });

  private options = { headers : this.headers, withCredentials: true};

  constructor(private httpClient: HttpClient) {}

  sendForgotPasswordEmail(email: string): Observable<any> {
    return this.httpClient.post<string>(
      this.baseUrl + "/Email/SendForgotPasswordEmail?email=" + email,
      this.options
    );
  }

  sendEmailConfirmation(email: string): Observable<any> {
    return this.httpClient.put<string>(
      this.baseUrl + "/Email/SendEmailConfirmation?email=" + email,
      this.options
    );
  }
}
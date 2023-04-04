import { HttpClient, HttpHeaderResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoggedUserDto } from "../model/loginUser.model";
import { UserDto } from "../model/user.model";

@Injectable()
export class AuthService{
  baseUrl = 'https://localhost:7270/api';

  private headers = new HttpHeaders({
    'Content-Type' : 'application/json'
  });

  private options = { headers : this.headers, withCredentials: true};

  constructor(private httpClient: HttpClient) {}

  register(user: UserDto): Observable<any> {
    const body = JSON.stringify(user);
    return this.httpClient.post<UserDto>(
      this.baseUrl + "/Auth/register",
      body,
      this.options
    );
  }

  getSymptomData(symptomNames: Array<string>, loggedUserEmail: string): Observable<any> {
    return this.httpClient.post<any>(
      this.baseUrl + "/Core/GetSymptomData?email=" + loggedUserEmail + "&symptoms=" + symptomNames.toString(),
      this.options
    );
  }

  loginAsPacient(user: LoggedUserDto): Observable<any> {
    return this.httpClient.post<string>(
      this.baseUrl + "/Auth/login?email=" + user.email + "&password=" + user.password,
      this.options
    );
  }

  forgotPassword(email: string): Observable<any> {
    return this.httpClient.post<string>(
      this.baseUrl + "/Auth/SendForgotPasswordEmail?email=" + email,
      this.options
    );
  }

  confirmEmail(userId: number): Observable<any> {
    return this.httpClient.put<string>(
      this.baseUrl + "/Auth/ConfirmEmail?id=" + userId,
      this.options
    );
  }

  modifyPassword(userId: number, newPassword: string): Observable<any> {
    return this.httpClient.put<string>(
      this.baseUrl + "/Auth/ModifyPassword?id=" + userId + "&newPassword=" + newPassword,
      this.options
    );
  }
}
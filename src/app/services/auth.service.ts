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

  login(user: LoggedUserDto): Observable<any> {
    const body = JSON.stringify(user);
    return this.httpClient.post<string>(
      this.baseUrl + "/Auth/login",
      body,
      this.options
    );
  }
  
  logOut(id: number): Observable<any> {
    return this.httpClient.delete<string>(
      this.baseUrl + "/Auth/LogOut?id=" + id,
      this.options
    );
  }

  modifyPassword(userId: number, newPassword: string): Observable<any> {
    return this.httpClient.put<string>(
      this.baseUrl + "/Auth/ModifyPassword?id=" + userId + "&newPassword=" + newPassword,
      this.options
    );
  }

  confirmEmail(email: string, confirmationCode: number): Observable<any> {
    return this.httpClient.put<string>(
      this.baseUrl + "/Auth/ConfirmEmail?email=" + email + "&code=" + confirmationCode,
      this.options
    );
  }

  checkLoginCookie(id: number): Observable<any> {
    return this.httpClient.get<any>(
      this.baseUrl + "/Auth/CheckLoginCookie?id=" + id,
      this.options
    );
  }
}
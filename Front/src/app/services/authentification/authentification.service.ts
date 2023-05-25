import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {BehaviorSubject, map, Observable} from "rxjs";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {
  protected baseUrl = 'http://localhost:5113/api';
  protected componentUrl = this.baseUrl + '/Authentification';
  protected tokenSubject = new BehaviorSubject<string>('');

  constructor(
    protected http: HttpClient,
    private jwtHelper: JwtHelperService
  ) { }

  login(username?: string, password?: string): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json').set('Authorization', 'Bearer ' + this.getToken());
    const loginData = {
      username: username,
      password: password
    }
    return this.http.post(this.componentUrl, loginData, {headers, observe: 'response'})
      .pipe(
        map((result: any) => {
          if (result.status != 200 || !result.ok) {
            return false;
          }
          const token = result.body.value;
          localStorage.setItem('token', token);
          this.tokenSubject.next(token);
          return true;
        })
      );
  }

  getToken(): string {
    return localStorage.getItem('token') || '';
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem('token');
    this.tokenSubject.next('');
  }
}

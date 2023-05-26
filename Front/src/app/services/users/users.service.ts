import {Injectable, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Users} from "../../models/users";

@Injectable({
  providedIn: 'root'
})
export class UsersService implements OnInit {

  protected baseUrl = 'http://localhost:5113/api';
  protected componentUrl = this.baseUrl + '/Users';

  constructor(
    protected http: HttpClient
  ) { }

  ngOnInit(): void {
  }

  getUser(id: number): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get(this.componentUrl + '/' + id, {headers});
  }

  createUser(users?: Users): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post(this.componentUrl, users, {headers});
  }
}

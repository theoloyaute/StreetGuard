import {Injectable, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpParams} from "@angular/common/http";

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

  getUsers(): Observable<any> {
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl, {params})
  }
}

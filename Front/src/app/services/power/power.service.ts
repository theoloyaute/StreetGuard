import {Injectable, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PowerService implements OnInit {
  protected baseUrl = 'http://localhost:5113/api';
  protected componentUrl = this.baseUrl + '/Power';

  constructor(
    protected http: HttpClient
  ) { }

  ngOnInit(): void {
  }

  getPowers(): any {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get(this.componentUrl, {headers});
  }
}

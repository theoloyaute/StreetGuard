import {Injectable, OnInit} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CityService implements OnInit {
  protected baseUrl = 'http://localhost:5113/api';
  protected componentUrl = this.baseUrl + '/City';

  constructor(
    protected http: HttpClient,
  ) { }

  ngOnInit(): void {
  }

  getCities(): Observable<any> {
    const params: HttpParams = new HttpParams();
    return this.http.get(this.componentUrl, {params});
  }
}

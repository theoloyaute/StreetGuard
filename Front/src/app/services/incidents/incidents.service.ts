import {Injectable, OnInit} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Incidents} from "../../models/incidents";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class IncidentsService implements OnInit {
  protected baseUrl = 'http://localhost:5113/api';
  protected componentUrl = this.baseUrl + '/Incidents';

  constructor(
    protected http: HttpClient
  ) {
  }

  ngOnInit(): void {
  }

  addIncident(incident: Incidents): Observable<any> {
    const params: HttpParams = new HttpParams();
    return this.http.post(this.componentUrl, incident, {params})
  }
}

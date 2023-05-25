import {Injectable, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
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

  getIncidents(powerId?: number, longitude?: number, latitude?: number): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get(this.componentUrl + '/find/' + powerId + '/' + longitude + '/' + latitude , {headers});
  }

  deleteIncident(id: number): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json', 'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete(this.componentUrl + '/' + id, {headers});
  }
}

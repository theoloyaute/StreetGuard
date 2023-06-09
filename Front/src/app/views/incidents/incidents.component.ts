import {Component, OnInit, ViewChild} from '@angular/core';
import {AuthentificationService} from "../../services/authentification/authentification.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {UsersService} from "../../services/users/users.service";
import {Users} from "../../models/users";
import {IncidentsService} from "../../services/incidents/incidents.service";
import {Incidents, IncidentView} from "../../models/incidents";
import {Router} from "@angular/router";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {MapInfoWindow, MapMarker} from "@angular/google-maps";

@Component({
  selector: 'app-incidents',
  templateUrl: './incidents.component.html',
  styleUrls: ['./incidents.component.css'],
  animations: [
    trigger('fadeOut', [
      state('void', style({opacity: 0})),
      transition(':enter', animate('0.5s ease-in-out')),
      transition(':leave', animate('0.5s ease-in-out', style({opacity: 0})))
    ])
  ]
})
export class IncidentsComponent implements OnInit {
  user?: Users;
  incidents: Incidents[] = [];
  incident?: Incidents;
  distance?: number;
  message?: string;
  center?: any;
  markers?: any[] = [];
  messageMarker?: string;

  userId?: number;
  userRole?: string;
  userName?: string;

  constructor(
    private AuthentificationService: AuthentificationService,
    private UsersService: UsersService,
    private jwtHelper: JwtHelperService,
    private IncidentsService: IncidentsService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.decompressToken();
    this.UsersService.getUser(this.userId!).subscribe((user: Users) => {
      this.user = user;
      this.getIncidents();
    });
  }

  decompressToken(): any {
    const decodedToken = this.jwtHelper.decodeToken(this.AuthentificationService.getToken());

    this.userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    this.userName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    var userIdString = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    this.userId = parseInt(userIdString);
  }

  getIncidents(): void {
    this.IncidentsService.getIncidents(this.user?.powerId, this.user?.longitude, this.user?.latitude).subscribe((incidents: Incidents[]) => {
      this.incidents = incidents;
      if (this.incidents.length == 0) {
        this.messageMarker = "Aucun incident à signaler";
      }
      console.log(this.incidents);
      this.incidents.forEach((incident: Incidents) => {
        this.distance = this.calculateDistance(this.user!.longitude!, this.user!.latitude!, incident.longitude!, incident.latitude!);
        this.center = {
          lat: this.user!.latitude,
          lng: this.user!.longitude,
        };
      })
      this.markers = this.incidents.map((incident: Incidents) => {
        return {
          info: {
            id: incident.id,
            date: incident.date,
            description: incident.description,
            cityName: incident.city?.name,
            incidentTypeName: incident.incidentType?.name,
            distance: this.calculateDistance(this.user!.longitude!, this.user!.latitude!, incident.longitude!, incident.latitude!),
          },
          position: {
            lat: incident.latitude,
            lng: incident.longitude,
          },
          label: {
            color: 'red',
          },
          options: {animation: google.maps.Animation.BOUNCE},
        }
      });
    });
  }

  calculateDistance(lon1: number, lat1: number, lon2: number, lat2: number): number {
    const earthRadius = 6371;

    const lon1Radian = lon1 * (Math.PI / 180);
    const lat1Radian = lat1 * (Math.PI / 180);
    const lon2Radian = lon2 * (Math.PI / 180);
    const lat2Radian = lat2 * (Math.PI / 180);

    const dLon = lon2Radian - lon1Radian;
    const dLat = lat2Radian - lat1Radian;

    const a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
      Math.cos(lat1Radian) * Math.cos(lat2Radian) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

    const distance = earthRadius * c;

    return Math.round(distance);
  }

  delete(id: number): void {
    this.IncidentsService.deleteIncident(id).subscribe((response: any) => {
      this.getIncidents();
    });

    this.message = "L'incident a bien été clos !";
  }

  click(event: google.maps.MapMouseEvent): void {
    console.log(event);
  }

  infoContent?: IncidentView;

  openInfo(marker: MapMarker, infoWindow: MapInfoWindow, content: any) {
    infoWindow.open(marker)
    this.infoContent = content
    console.log(this.infoContent);
  }
}

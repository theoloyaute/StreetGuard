import {Component, OnInit} from '@angular/core';
import {IncidentTypeService} from "../../services/incidentType/incident-type.service";
import {IncidentType} from "../../models/incidentType";
import {IncidentsService} from "../../services/incidents/incidents.service";
import {HttpErrorResponse} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {City} from "../../models/city";
import {CityService} from "../../services/city/city.service";
import {AuthentificationService} from "../../services/authentification/authentification.service";
import {JwtHelperService} from "@auth0/angular-jwt";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  animations: [
    trigger('fadeOut', [
      state('void', style({ opacity: 0 })),
      transition(':enter', animate('0.5s ease-in-out')),
      transition(':leave', animate('0.5s ease-in-out', style({ opacity: 0 })))
    ])
  ]
})
export class HomeComponent implements OnInit {
  incidentTypes: IncidentType[] = [];
  cities: City[] = [];

  selectedType: IncidentType | null = null;
  selectedCity: City | null = null;
  description?: string;
  longitude?: number;
  latitude?: number;

  errorMessage!: string;
  succesMessage!: string;

  userId?: string;
  userRole?: string;
  userName?: string;

  constructor(
    private AuthentificationService: AuthentificationService,
    private IncidentTypeService: IncidentTypeService,
    private IncidentsService: IncidentsService,
    private CityService: CityService,
    private jwtHelper: JwtHelperService,
    private route: ActivatedRoute,
  ) {
  }

  ngOnInit(): void {
    this.IncidentTypeService.getIncidentTypes().subscribe((incidentTypes: IncidentType[]) => {
      this.incidentTypes = incidentTypes;
    });
    this.CityService.getCities().subscribe((cities: City[]) => {
      this.cities = cities;
    });
    this.decompressToken();
  }

  add() {
    const incident = {
      description: this.description,
      longitude: this.longitude,
      latitude: this.latitude,
      incidentTypeId: this.selectedType?.id,
      cityId: this.selectedCity?.id
    }

    this.IncidentsService.addIncident(incident).subscribe(result => {
      this.succesMessage = "Incident créé avec succès !";
      setTimeout(() => {
        this.succesMessage = "";
      }, 5000);
    }, (error: HttpErrorResponse) => {
      if (error.status == 404) {
        this.errorMessage = "Erreur de création !";
      }
      if (error.status == 400) {
        this.errorMessage = "Il faut remplir tous les champs !";
      }
      if (error.status == 500) {
        this.errorMessage = "Il faut remplir tous les champs !";
      } setTimeout(() => {
        this.errorMessage = "";
      }
        , 5000);
    });

    this.description = "";
    this.longitude = undefined;
    this.latitude = undefined;
    this.selectedType = null;
    this.selectedCity = null;
  }

  decompressToken(): any {
    const decodedToken = this.jwtHelper.decodeToken(this.AuthentificationService.getToken());

    this.userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    this.userName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    this.userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
  }
}

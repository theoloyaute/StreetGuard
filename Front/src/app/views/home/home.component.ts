import {Component, OnInit} from '@angular/core';
import {IncidentTypeService} from "../../services/incidentType/incident-type.service";
import {IncidentType} from "../../models/incidentType";
import {IncidentsService} from "../../services/incidents/incidents.service";
import {HttpErrorResponse} from "@angular/common/http";
import {Router} from "@angular/router";
import {animate, state, style, transition, trigger} from "@angular/animations";

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

  selectedType: IncidentType | null = null;
  description?: string;
  longitude?: number;
  latitude?: number;

  errorMessage!: string;

  constructor(
    private IncidentTypeService: IncidentTypeService,
    private IncidentsService: IncidentsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.IncidentTypeService.getIncidentTypes().subscribe((incidentTypes: IncidentType[]) => {
      this.incidentTypes = incidentTypes;
    });
  }

  add() {
    const incident = {
      description: this.description,
      longitude: this.longitude,
      latitude: this.latitude,
      incidentTypeId: this.selectedType?.id
    }

    this.IncidentsService.addIncident(incident).subscribe(result => {
      this.router.navigate(['/']);
    }, (error: HttpErrorResponse) => {
      if (error.status == 404) {
        this.errorMessage = "Erreur de création !";
      }
      if (error.status == 400) {
        this.errorMessage = "Il faut remplir tous les champs !";
      } setTimeout(() => {
        this.errorMessage = "";
      }
        , 5000);
    });
  }
}

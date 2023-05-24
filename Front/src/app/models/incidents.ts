import {IncidentType} from "./incidentType";

export interface Incidents {
  id?: number;
  date?: Date;
  description?: string;
  longitude?: number;
  latitude?: number;
  incidentTypeId?: number;
  incidentType?: IncidentType;
}

import {IncidentType} from "./incidentType";
import {City} from "./city";

export interface Incidents {
  id?: number;
  date?: any;
  description?: string;
  longitude?: number;
  latitude?: number;
  incidentTypeId?: number;
  cityId?: number;
  city?: City;
  incidentType?: IncidentType;
}

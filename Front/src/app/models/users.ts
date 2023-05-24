import {Power} from "./power";

export interface Users {
  id?: number;
  username?: string;
  firstname?: string;
  lastname?: string;
  email?: string;
  phone?: string;
  password?: string;
  longitude?: number;
  latitude?: number;
  roleId?: number;
  role?: string;
  powerId?: number;
  power?: Power;
}

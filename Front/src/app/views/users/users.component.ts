import {Component, OnInit} from '@angular/core';
import {Power} from "../../models/power";
import {PowerService} from "../../services/power/power.service";
import {UsersService} from "../../services/users/users.service";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  animations: [
    trigger('fadeOut', [
      state('void', style({opacity: 0})),
      transition(':enter', animate('0.5s ease-in-out')),
      transition(':leave', animate('0.5s ease-in-out', style({opacity: 0})))
    ])
  ]
})
export class UsersComponent implements OnInit {
  powers?: Power[];

  selectedPower: Power | null = null;
  username?: string;
  firstname?: string;
  lastname?: string;
  email?: string;
  phone?: string;
  password?: string;
  passwordConfirm?: string;
  longitude?: number;
  latitude?: number;

  errorMessage!: string;
  succesMessage!: string;

  constructor(
    private PowerService: PowerService,
    private UsersService: UsersService,
  ) {
  }

  ngOnInit(): void {
    this.PowerService.getPowers().subscribe((powers: Power[]) => {
      this.powers = powers;
    });
  }

  create() {
    if (this.username == undefined || this.email == undefined || this.phone == undefined || this.password == undefined || this.passwordConfirm == undefined || this.longitude == undefined || this.latitude == undefined || this.username == '' || this.email == '' || this.phone == '' || this.password == '' || this.passwordConfirm == '' || this.longitude == 0 || this.latitude == 0 || this.selectedPower == null) {
      this.errorMessage = "Il faut remplir tous les champs obligatoires (*) !";
      setTimeout(() => {
        this.errorMessage = "";
      }, 5000);
      return;
    }

    if (this.password != this.passwordConfirm) {
      this.errorMessage = "Les mots de passe doivent être identique !";
      setTimeout(() => {
        this.errorMessage = "";
      }, 5000);
      return;
    }

    const user = {
      username: this.username,
      firstname: this.firstname,
      lastname: this.lastname,
      email: this.email,
      phone: this.phone,
      password: this.password,
      longitude: this.longitude,
      latitude: this.latitude,
      powerId: this.selectedPower?.id
    };
    setTimeout(() => {
      this.errorMessage = "";
    }, 5000);

    this.UsersService.createUser(user).subscribe((response: any) => {
      this.succesMessage = 'Utilisateur créé avec succès !';
      setTimeout(() => {
        this.succesMessage = '';
      }, 5000);
    }, (error: HttpErrorResponse) => {
      if (error.status == 404) {
        this.errorMessage = "Erreur de création !";
      }
      setTimeout(() => {
        this.errorMessage = "";
      }, 5000);
    });

    this.username = '';
    this.firstname = '';
    this.lastname = '';
    this.email = '';
    this.phone = '';
    this.password = '';
    this.passwordConfirm = '';
    this.longitude = 0;
    this.latitude = 0;
    this.selectedPower = null;
  }
}

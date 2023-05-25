import {Component, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {AuthentificationService} from "../../services/authentification/authentification.service";
import {Router} from "@angular/router";
import {HttpErrorResponse} from "@angular/common/http";
import {animate, state, style, transition, trigger} from "@angular/animations";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  animations: [
    trigger('fadeOut', [
      state('void', style({ opacity: 0 })),
      transition(':enter', animate('0.5s ease-in-out')),
      transition(':leave', animate('0.5s ease-in-out', style({ opacity: 0 })))
    ])
  ]
})
export class LoginComponent implements OnInit {

  username?: string;
  password?: string;

  errorMessage!: string;

  constructor(
    private authentificationService: AuthentificationService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    console.log(this.authentificationService.isLoggedIn());
  }

  login() {
    this.authentificationService.login(this.username, this.password).subscribe(result => {
      this.router.navigate(['/']);
    }, (error: HttpErrorResponse) => {
      if (error.status == 400) {
        this.errorMessage = "Username ou mot de passe incorrect !";
      } setTimeout(() => {
          this.errorMessage = "";
        }
        , 5000);
    });
  }

}

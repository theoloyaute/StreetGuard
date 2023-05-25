import {Component, OnInit} from '@angular/core';
import {AuthentificationService} from "../../services/authentification/authentification.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(
    private authentificationService: AuthentificationService,
    private router: Router
  ) {
  }

  ngOnInit() {
  }

  isLoggedIn() {
    return this.authentificationService.isLoggedIn();
  }

  logout() {
    this.authentificationService.logout();
    return this.router.navigate(['/']);
  }

}

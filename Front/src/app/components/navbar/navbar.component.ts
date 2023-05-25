import {Component, OnInit} from '@angular/core';
import {AuthentificationService} from "../../services/authentification/authentification.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(
    private authentificationService: AuthentificationService,
  ) {
  }

  ngOnInit() {
  }

  isLoggedIn() {
    return this.authentificationService.isLoggedIn();
  }

  logout() {
    return this.authentificationService.logout();
  }

}

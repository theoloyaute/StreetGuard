import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HomeComponent} from './views/home/home.component';
import {HttpClientModule} from "@angular/common/http";
import {InputTextModule} from "primeng/inputtext";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {InputTextareaModule} from "primeng/inputtextarea";
import {DropdownModule} from "primeng/dropdown";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {InputNumberModule} from "primeng/inputnumber";
import {ButtonModule} from "primeng/button";
import {NavbarComponent} from './components/navbar/navbar.component';
import {LoginComponent} from './views/login/login.component';
import {GoogleMapsModule} from "@angular/google-maps";
import {MapComponent} from './views/map/map.component';
import { IncidentsComponent } from './views/incidents/incidents.component';
import {JwtModule} from "@auth0/angular-jwt";
import {DividerModule} from "primeng/divider";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    LoginComponent,
    MapComponent,
    IncidentsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    InputTextModule,
    InputTextareaModule,
    DropdownModule,
    FormsModule,
    ReactiveFormsModule,
    InputNumberModule,
    ButtonModule,
    GoogleMapsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token'),
        allowedDomains: ['http://localhost:4200'],
        disallowedRoutes: ['http://localhost:5113/api/Authentification'],
      },
    }),
    DividerModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}

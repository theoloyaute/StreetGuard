import { Component } from '@angular/core';
@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent {
  mapCenter: google.maps.LatLngLiteral = { lat: 37.7749, lng: -122.4194 };
  mapZoom = 12;
  markerPosition: google.maps.LatLngLiteral | undefined;

  // constructor(private gmapsApi: GoogleMapsAPIWrapper) {
  // }
  //
  // geocode(address: string): void {
  //   this.gmapsApi.geocode({ address }).subscribe((results: GeocoderResult[]) => {
  //     if (results.length > 0) {
  //       const location = results[0].geometry.location;
  //       this.markerPosition = { lat: location.lat(), lng: location.lng() };
  //       this.mapCenter = this.markerPosition;
  //     }
  //   });
  // }

}

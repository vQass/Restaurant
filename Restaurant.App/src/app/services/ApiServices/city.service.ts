import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { apiEndpoints } from 'src/apiEndpointsConfig'
import { CityWrapper } from 'src/models/city/CityWrapper';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})

export class CityService {

  baseApiUrl = environment.baseApiUrl;
  cityEndpoints = apiEndpoints.cityEndpoints;

  constructor(private http: HttpClient) {}

  getCities(): Observable<CityWrapper> {
    return this.http.get<CityWrapper>(this.baseApiUrl + this.cityEndpoints.getCities);
  }
}

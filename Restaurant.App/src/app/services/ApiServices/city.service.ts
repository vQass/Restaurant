import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { CityWrapper } from 'src/models/city/CityWrapper';

@Injectable({
  providedIn: 'root'
})

export class CityService {

  baseApiUrl = environment.baseApiUrl;
  cityEndpoints = apiEndpoints.cityEndpoints;

  constructor(private http: HttpClient) { }

  getCities(): Observable<CityWrapper> {
    return this.http.get<CityWrapper>(this.baseApiUrl + this.cityEndpoints.getCities);
  }

  getCitiesWithSpecifiedActivity(cityActivity: boolean): Observable<CityWrapper> {
    let params = new HttpParams().set('cityActivity', cityActivity);

    return this.http.get<CityWrapper>(this.baseApiUrl + this.cityEndpoints.getCities, { params: params });
  }
}

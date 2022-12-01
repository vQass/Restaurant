import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { City } from 'src/models/city/City';
import { CityCreateRequest } from 'src/models/city/CityCreateRequest';
import { CityUpdateRequest } from 'src/models/city/CityUpdateRequest';
import { CityWrapper } from 'src/models/city/CityWrapper';

@Injectable({
  providedIn: 'root'
})

export class CityService {
  baseApiUrl = environment.baseApiUrl;
  cityEndpoints = apiEndpoints.cityEndpoints;

  constructor(private http: HttpClient) { }

  add(city: CityCreateRequest) {
    return this.http.post(this.baseApiUrl + this.cityEndpoints.add, city).pipe(
      catchError(this.handleError)
    );;
  }

  update(city: CityUpdateRequest, id: number) {
    return this.http.put(this.baseApiUrl + this.cityEndpoints.update + '/' + id, city).pipe(
      catchError(this.handleError)
    );;
  }

  getCity(id: number): Observable<City> {
    return this.http.get<City>(this.baseApiUrl + this.cityEndpoints.getCity + '/' + id);
  }

  getCities(pageIndex = 0, pageSize = 0): Observable<CityWrapper> {
    let params = new HttpParams();
    params = params.append("pageIndex", pageIndex);
    params = params.append("pageSize", pageSize);

    return this.http.get<CityWrapper>(this.baseApiUrl + this.cityEndpoints.getCities, { params: params });
  }

  getCitiesWithSpecifiedActivity(cityActivity: boolean): Observable<CityWrapper> {
    let params = new HttpParams().set('cityActivity', cityActivity);

    return this.http.get<CityWrapper>(this.baseApiUrl + this.cityEndpoints.getCities, { params: params });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(this.baseApiUrl + this.cityEndpoints.delete + '/' + id).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.log('An error occurred:', error.error);
    } else {
      console.log(`Backend returned code ${error.status}, body was: `, error.error);
      return throwError(() => new Error(error.error))
    }
    return throwError(() => new Error('Coś poszło nie tak, proszę spróbować później'));
  }
}

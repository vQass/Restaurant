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

  getCity(id: number): Observable<City> {
    return this.http.get<City>(this.baseApiUrl + this.cityEndpoints.get + id);
  }

  getCities(cityActivity: boolean | null): Observable<City[]> {
    let params = new HttpParams();

    if (cityActivity != null) {
      params = params.append("cityActivity", cityActivity);
    }

    return this.http.get<City[]>(this.baseApiUrl + this.cityEndpoints.getList, { params: params });
  }

  getCityPage(pageIndex = 0, pageSize = 0): Observable<CityWrapper> {
    let params = new HttpParams();
    params = params.append("pageIndex", pageIndex);
    params = params.append("pageSize", pageSize);

    return this.http.get<CityWrapper>(this.baseApiUrl + this.cityEndpoints.getPage, { params: params });
  }

  add(city: CityCreateRequest) {
    return this.http.post(this.baseApiUrl + this.cityEndpoints.add, city).pipe(
      catchError(this.handleError)
    );
  }

  update(city: CityUpdateRequest, id: number) {
    return this.http.put(this.baseApiUrl + this.cityEndpoints.update + id, city).pipe(
      catchError(this.handleError)
    );
  }

  delete(id: number): Observable<any> {
    return this.http.delete(this.baseApiUrl + this.cityEndpoints.delete + id).pipe(
      catchError(this.handleError)
    );
  }

  enable(id: number) {
    return this.http.put(this.baseApiUrl + this.cityEndpoints.enable + id, '').pipe(
      catchError(this.handleError)
    );
  }

  disable(id: number) {
    return this.http.put(this.baseApiUrl + this.cityEndpoints.disable + id, '').pipe(
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

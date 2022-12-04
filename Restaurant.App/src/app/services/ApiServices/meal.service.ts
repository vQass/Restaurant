import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';
import { MealAdminPanelWrapper } from 'src/models/meal/MealAdminPanelWrapper';
import { MealCreateRequest } from 'src/models/meal/MealCreateRequest';
import { MealGroupViewModel } from 'src/models/meal/MealGroupViewModel';
import { MealUpdateRequest } from 'src/models/meal/MealUpdateRequest';

@Injectable({
  providedIn: 'root'
})
export class MealService {

  baseApiUrl = environment.baseApiUrl;
  mealEndpoints = apiEndpoints.mealEndpoints;

  constructor(private http: HttpClient) { }

  get(id: number) {
    return this.http.get<MealAdminPanelItem>(this.baseApiUrl + this.mealEndpoints.get + id)
      .pipe(
        catchError(this.handleError)
      );
  }

  getGroups(): Observable<MealGroupViewModel[]> {
    return this.http.get<MealGroupViewModel[]>(this.baseApiUrl + this.mealEndpoints.getGroups);
  }

  getPage(pageIndex: number, pageSize: number): Observable<MealAdminPanelWrapper> {
    let params = new HttpParams();
    params = params.append("pageIndex", pageIndex);
    params = params.append("pageSize", pageSize);
    return this.http.get<MealAdminPanelWrapper>(this.baseApiUrl + this.mealEndpoints.getPage, { params: params });
  }

  add(meal: MealCreateRequest): Observable<any> {
    return this.http.post<MealCreateRequest>(
      this.baseApiUrl + this.mealEndpoints.add, meal)
      .pipe(
        catchError(this.handleError)
      );
  }

  update(meal: MealUpdateRequest, id: number): Observable<any> {
    return this.http.put<MealUpdateRequest>(
      this.baseApiUrl + this.mealEndpoints.update + id, meal)
      .pipe(
        catchError(this.handleError)
      );
  }

  updatePrice(id: number, newPrice: number): Observable<number> {
    return this.http.patch<number>(
      this.baseApiUrl + this.mealEndpoints.updatePrice + id + '/' + newPrice.toString(), null)
      .pipe(
        catchError(this.handleError)
      );
  }

  delete(id: number): Observable<any> {
    return this.http.delete(
      this.baseApiUrl + this.mealEndpoints.delete + id)
      .pipe(
        catchError(this.handleError)
      );
  }

  setAsAvailable(id: number): Observable<any> {
    return this.http.patch(
      this.baseApiUrl + this.mealEndpoints.setAsAvailable, id)
      .pipe(
        catchError(this.handleError)
      );
  }

  setAsUnavailable(id: number): Observable<any> {
    return this.http.patch(
      this.baseApiUrl + this.mealEndpoints.setAsUnavailable, id)
      .pipe(
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

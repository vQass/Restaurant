import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { MealAdminPanelWrapper } from 'src/models/meal/MealAdminPanelWrapper';
import { MealCreateRequest } from 'src/models/meal/MealCreateRequest';
import { MealGroupViewModel } from 'src/models/meal/MealGroupViewModel';

@Injectable({
  providedIn: 'root'
})
export class MealService {

  baseApiUrl = environment.baseApiUrl;
  mealEndpoints = apiEndpoints.mealEndpoints;

  constructor(private http: HttpClient) { }

  getGroupedMeals(): Observable<MealGroupViewModel[]> {
    return this.http.get<MealGroupViewModel[]>(this.baseApiUrl + this.mealEndpoints.getMealsGroups);
  }

  getMealsForAdminPanel(pageIndex: number, pageSize: number): Observable<MealAdminPanelWrapper> {
    return this.http.get<MealAdminPanelWrapper>(this.baseApiUrl + this.mealEndpoints.getMealsForAdminPanel);
  }

  addMeal(meal: MealCreateRequest): Observable<any> {
    return this.http.post<MealCreateRequest>(
      this.baseApiUrl + this.mealEndpoints.addMeal, meal)
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

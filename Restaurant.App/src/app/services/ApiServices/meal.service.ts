import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { MealAdminPanelWrapper } from 'src/models/meal/MealAdminPanelWrapper';
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
}

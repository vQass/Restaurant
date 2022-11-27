import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { MealCategory } from 'src/models/mealCategory/MealCategory';

@Injectable({
  providedIn: 'root'
})
export class MealCategoryService {
  baseApiUrl = environment.baseApiUrl;
  mealCategoryEndpoints = apiEndpoints.mealCategoryEndpoints;
  constructor(private http: HttpClient) { }

  getMealCategories(): Observable<MealCategory[]> {
    return this.http.get<MealCategory[]>(this.baseApiUrl + this.mealCategoryEndpoints.getMealCategories);
  }

}

import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { MealCategory } from 'src/models/mealCategory/MealCategory';
import { MealCategoryCreateRequest } from 'src/models/mealCategory/MealCategoryCreateRequest';
import { MealCategoryWrapper } from 'src/models/mealCategory/MealCategoryWrapper';

@Injectable({
  providedIn: 'root'
})
export class MealCategoryService {
  baseApiUrl = environment.baseApiUrl;
  mealCategoryEndpoints = apiEndpoints.mealCategoryEndpoints;
  constructor(private http: HttpClient) { }

  getMealCategory(id: number): Observable<MealCategory> {
    return this.http.get<MealCategory>(this.baseApiUrl + this.mealCategoryEndpoints.get + id);
  }

  getMealCategories(): Observable<MealCategory[]> {
    return this.http.get<MealCategory[]>(this.baseApiUrl + this.mealCategoryEndpoints.getList);
  }

  getMealCategoriesPage(pageIndex = 0, pageSize = 0): Observable<MealCategoryWrapper> {
    let params = new HttpParams();
    params = params.append("pageIndex", pageIndex);
    params = params.append("pageSize", pageSize);

    return this.http.get<MealCategoryWrapper>(this.baseApiUrl + this.mealCategoryEndpoints.getPage, { params: params });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(this.baseApiUrl + this.mealCategoryEndpoints.delete + id).pipe(
      catchError(this.handleError)
    );
  }

  add(mealCategory: MealCategoryCreateRequest) {
    return this.http.post(this.baseApiUrl + this.mealCategoryEndpoints.add, mealCategory).pipe(
      catchError(this.handleError)
    );
  }

  update(mealCategory: MealCategoryCreateRequest, id: number) {
    return this.http.put(this.baseApiUrl + this.mealCategoryEndpoints.update + id, mealCategory).pipe(
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

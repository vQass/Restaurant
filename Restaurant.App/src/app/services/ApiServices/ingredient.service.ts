import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { IngredientAdminPanelWrapper } from 'src/models/ingredient/IngredientAdminPanelWrapper';
import { IngredientCreateRequest } from 'src/models/ingredient/IngredientCreateRequest';

@Injectable({
  providedIn: 'root'
})
export class IngredientService {
  baseApiUrl = environment.baseApiUrl;
  ingredientsEndpoints = apiEndpoints.ingredientsEndpoints;
  constructor(private http: HttpClient) { }

  getIngredientsForAdminPanel(pageIndex: number, pageSize: number): Observable<IngredientAdminPanelWrapper> {
    let params = new HttpParams();
    params = params.append('pageIndex', pageIndex);
    params = params.append('pageSize', pageSize);

    return this.http.get<IngredientAdminPanelWrapper>(
      this.baseApiUrl + this.ingredientsEndpoints.getIngredientsForAdminPanel, { params: params });
  }

  addIngredient(ingredient: IngredientCreateRequest): Observable<any> {

    return this.http.post<IngredientAdminPanelWrapper>(
      this.baseApiUrl + this.ingredientsEndpoints.addIngredient, ingredient)
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

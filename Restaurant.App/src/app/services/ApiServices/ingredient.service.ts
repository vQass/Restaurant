import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { Ingredient } from 'src/models/ingredient/Ingredient';
import { IngredientCreateRequest } from 'src/models/ingredient/IngredientCreateRequest';
import { IngredientUpdateRequest } from 'src/models/ingredient/IngredientUpdateRequest';
import { IngredientWrapper } from 'src/models/ingredient/IngredientWrapper';

@Injectable({
  providedIn: 'root'
})
export class IngredientService {
  baseApiUrl = environment.baseApiUrl;
  ingredientsEndpoints = apiEndpoints.ingredientsEndpoints;
  constructor(private http: HttpClient) { }

  get(id: number): Observable<Ingredient> {
    return this.http.get<Ingredient>(
      this.baseApiUrl + this.ingredientsEndpoints.get + id);
  }

  getPage(pageIndex: number, pageSize: number): Observable<IngredientWrapper> {
    let params = new HttpParams();
    params = params.append('pageIndex', pageIndex);
    params = params.append('pageSize', pageSize);

    return this.http.get<IngredientWrapper>(
      this.baseApiUrl + this.ingredientsEndpoints.getPage, { params: params });
  }

  add(ingredient: IngredientCreateRequest): Observable<any> {
    return this.http.post<IngredientCreateRequest>(
      this.baseApiUrl + this.ingredientsEndpoints.add, ingredient)
      .pipe(
        catchError(this.handleError)
      );
  }

  edit(id: number, ingredient: IngredientUpdateRequest): Observable<any> {
    return this.http.put<IngredientUpdateRequest>(
      this.baseApiUrl + this.ingredientsEndpoints.udpate + id, ingredient)
      .pipe(
        catchError(this.handleError)
      );
  }

  delete(id: number): Observable<any> {
    return this.http.delete(
      this.baseApiUrl + this.ingredientsEndpoints.delete + id)
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

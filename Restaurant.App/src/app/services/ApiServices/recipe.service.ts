import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { RecipeEditViewModel } from 'src/models/recipe/RecipeEditViewModel';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseApiUrl = environment.baseApiUrl;
  recipeEndpoints = apiEndpoints.recipeEndpoints;

  constructor(private http: HttpClient) { }

  getRecipeEditViewModel(id: number): Observable<RecipeEditViewModel> {
    return this.http.get<RecipeEditViewModel>(this.baseApiUrl + this.recipeEndpoints.getEditModel + id)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateMealRecipe(mealId: number, ingredientsIds: number[]): Observable<any> {
    return this.http.put<any>(this.baseApiUrl + this.recipeEndpoints.update + mealId, ingredientsIds)
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

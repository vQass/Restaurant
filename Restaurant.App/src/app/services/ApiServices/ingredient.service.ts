import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { IngredientAdminPanelWrapper } from 'src/models/ingredient/IngredientAdminPanelWrapper';

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
}

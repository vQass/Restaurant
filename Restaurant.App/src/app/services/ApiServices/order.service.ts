import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { OrderAddRequest } from 'src/models/order/AddOrderRequest';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseApiUrl = environment.baseApiUrl;
  orderEndpoints = apiEndpoints.orderEndpoints;

  constructor(private http: HttpClient) { }

  addOrder(order: OrderAddRequest): Observable<any> {
    return this.http.post<OrderAddRequest>(this.baseApiUrl + this.orderEndpoints.addOrder, order)
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

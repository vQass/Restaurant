import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { OrderAddRequest } from 'src/models/order/AddOrderRequest';
import { OrderAdminPanelWrapper } from 'src/models/order/OrderAdminPanelWrapper';
import { OrderHistoryWrapper } from 'src/models/order/OrderHistoryWrapper';
import { OrderStatus } from 'src/models/order/OrderStatus';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseApiUrl = environment.baseApiUrl;
  orderEndpoints = apiEndpoints.orderEndpoints;

  constructor(private http: HttpClient) { }

  changeOrderStatus(orderId: number, statusId: number) {
    let params = new HttpParams();
    params = params.append('orderStatus', statusId);

    console.log(statusId);

    return this.http.patch<OrderAddRequest>(
      this.baseApiUrl + this.orderEndpoints.changeStatus + orderId, statusId)
      .pipe(
        catchError(this.handleError)
      );
  }

  addOrder(order: OrderAddRequest): Observable<any> {
    return this.http.post<OrderAddRequest>(this.baseApiUrl + this.orderEndpoints.add, order)
      .pipe(
        catchError(this.handleError)
      );
  }

  getOrderStatuses(): Observable<OrderStatus[]> {
    return this.http.get<OrderStatus[]>(this.baseApiUrl + this.orderEndpoints.getStatuses)
      .pipe(
        catchError(this.handleError)
      );
  }

  getOrderHistory(pageIndex: number, pageSize: number, userId: number = 0): Observable<OrderHistoryWrapper> {
    let params = new HttpParams();
    params = params.append('userId', userId);
    params = params.append("pageIndex", pageIndex);
    params = params.append("pageSize", pageSize);
    params = params.append('orderByParams', 'OrderDate desc, Status');

    return this.http.get<OrderHistoryWrapper>(this.baseApiUrl + this.orderEndpoints.getHistoryPage, { params: params })
      .pipe(
        catchError(this.handleError)
      );
  }

  getOrdersForAdminPanel(pageIndex: number, pageSize: number): Observable<OrderAdminPanelWrapper> {
    let params = new HttpParams();
    params = params.append("pageIndex", pageIndex);
    params = params.append("pageSize", pageSize);
    params = params.append("orderByParams", 'id desc');

    return this.http.get<OrderAdminPanelWrapper>(this.baseApiUrl + this.orderEndpoints.getPage, { params: params })
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

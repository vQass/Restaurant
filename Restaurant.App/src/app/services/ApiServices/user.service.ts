import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { UserCreateRequest } from 'src/models/user/UserCreateRequest';
import { UserListElement } from 'src/models/user/UserListElement';
import { UserLoginRequest } from 'src/models/user/UserLoginRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseApiUrl = environment.baseApiUrl;
  userEndpoints = apiEndpoints.userEndpoints;
  isLoggedIn: boolean = true;

  constructor(private http: HttpClient) { }

  getUsers(): Observable<UserListElement[]> {
    let url = this.baseApiUrl + this.userEndpoints.getUsers;
    return this.http.get<UserListElement[]>(url);
  }

  addUser(user: UserCreateRequest): Observable<any> {
    let url = this.baseApiUrl + this.userEndpoints.addUser;

    return this.http.post<UserCreateRequest>(url, user)
      .pipe(
        catchError(this.handleError)
      );
  }

  login(login: UserLoginRequest): Observable<any> {
    let url = this.baseApiUrl + this.userEndpoints.addUser;

    return this.http.post<UserCreateRequest>(url, login)
      .pipe(
        catchError(this.handleError)
      );
  }


  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.log('An error occurred:', error.error);
    } else {
      console.log(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}

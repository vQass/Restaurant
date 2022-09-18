import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { LoginResponse } from 'src/models/user/LoginResponse';
import { UserCreateRequest } from 'src/models/user/UserCreateRequest';
import { UserListElement } from 'src/models/user/UserListElement';
import { UserLoginRequest } from 'src/models/user/UserLoginRequest';

import { ToastService } from '../OtherServices/toast.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseApiUrl = environment.baseApiUrl;
  userEndpoints = apiEndpoints.userEndpoints;

  private isLoggedIn: BehaviorSubject<boolean>;
  private role: BehaviorSubject<string>;

  constructor(private http: HttpClient, private toastService: ToastService) {
    this.isLoggedIn = new BehaviorSubject<boolean>(false);
    this.role = new BehaviorSubject<string>("");
  }

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

  login(login: UserLoginRequest): Observable<LoginResponse> {
    let url = this.baseApiUrl + this.userEndpoints.singIn;

    return this.http.post<LoginResponse>(url, login)
      .pipe(
        catchError(this.handleError)
      );
  }

  logout() {
    sessionStorage.removeItem("token");
    this.setIsLoggedIn(false);
    this.setRole("");
    this.toastService.showSuccess("Wylogowano!", 2500);
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

  getIsLoggedIn(): Observable<boolean> {
    return this.isLoggedIn.asObservable();
  }

  setIsLoggedIn(value: boolean): void {
    this.isLoggedIn.next(value);
  }

  getRole(): Observable<string> {
    return this.role.asObservable();
  }
  setRole(role: string): void {
    this.role.next(role);
  }
}

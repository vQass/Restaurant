import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { apiEndpoints } from 'src/apiEndpointsConfig';
import { environment } from 'src/environments/environment';
import { LoginResponse } from 'src/models/user/LoginResponse';
import { UserCreateRequest } from 'src/models/user/UserCreateRequest';
import { UserLoginRequest } from 'src/models/user/UserLoginRequest';

import { ToastService } from '../OtherServices/toast.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseApiUrl = environment.baseApiUrl;
  userEndpoints = apiEndpoints.userEndpoints;

  private isLoggedIn: BehaviorSubject<boolean>;
  private authToken: string | null;
  private role: string;
  private id: number;

  constructor(private http: HttpClient, private toastService: ToastService, private router: Router) {
    let userStringified = sessionStorage.getItem("user");

    if (userStringified == null) {
      this.isLoggedIn = new BehaviorSubject<boolean>(false);
      this.role = "";
      this.id = 0;
      this.authToken = "";
    }
    else {
      let user = JSON.parse(userStringified);
      this.isLoggedIn = new BehaviorSubject<boolean>(true);
      this.authToken = user.authToken;
      this.role = user.role;
      this.id = user.id;
    }
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
    sessionStorage.removeItem("user");
    this.setIsLoggedIn(false);
    this.setRole("");
    this.setId(0);
    this.setAuthToken(null);
    this.toastService.showSuccess("Wylogowano!", 2000);
    this.router.navigate(['home']);
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

  getIsLoggedInValue(): boolean {
    return this.isLoggedIn.getValue();
  }

  getAuthToken(): string | null {
    return this.authToken;
  }

  setAuthToken(authToken: string | null): void {
    this.authToken = authToken;
  }

  getRole(): string {
    return this.role;
  }

  setRole(role: string): void {
    this.role = role;
  }

  setId(id: number): void {
    this.id = id;
  }

  getId(): number {
    return this.id;
  }
}

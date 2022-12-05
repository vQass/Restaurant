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
  private role: BehaviorSubject<string>;
  private authToken: string | null;
  private id: number;

  constructor(private http: HttpClient, private toastService: ToastService, private router: Router) {
    let userStringified = sessionStorage.getItem("user");

    if (userStringified == null) {
      this.isLoggedIn = new BehaviorSubject<boolean>(false);
      this.role = new BehaviorSubject<string>("");
      this.id = 0;
      this.authToken = "";
    }
    else {
      let user = JSON.parse(userStringified);
      this.isLoggedIn = new BehaviorSubject<boolean>(true);
      this.role = new BehaviorSubject<string>(user.role);
      this.authToken = user.authToken;
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

  loginRequest(login: UserLoginRequest): Observable<LoginResponse> {
    let url = this.baseApiUrl + this.userEndpoints.singIn;

    return this.http.post<LoginResponse>(url, login)
      .pipe(
        catchError(this.handleError)
      );
  }

  login(resp: LoginResponse) {
    this.setIsLoggedIn(true);
    this.setAuthToken(resp.jwtToken);
    this.setRole(resp.role);
    this.setId(resp.id);

    var user = JSON.stringify(resp);
    sessionStorage.setItem('user', user);
    sessionStorage.setItem('token', resp.jwtToken);

    this.toastService.showSuccess("Pomyślnie zalogowano!", 2000)
    this.router.navigate(['home']);
  }

  logout() {
    sessionStorage.removeItem("user");
    sessionStorage.removeItem("token");
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

  getRoleValue(): string {
    return this.role.getValue();
  }

  getRole(): Observable<string> {
    return this.role.asObservable();
  }

  setRole(role: string): void {
    this.role.next(role);
  }

  setId(id: number): void {
    this.id = id;
  }

  getId(): number {
    return this.id;
  }
}

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
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
  private authToken: BehaviorSubject<string>;
  private role: BehaviorSubject<string>;
  private id: BehaviorSubject<number>;

  constructor(private http: HttpClient, private toastService: ToastService, private router: Router) {
    let userStringified = sessionStorage.getItem("user");

    if (userStringified == null) {
      this.isLoggedIn = new BehaviorSubject<boolean>(false);
      this.authToken = new BehaviorSubject<string>("");
      this.role = new BehaviorSubject<string>("");
      this.id = new BehaviorSubject<number>(0);
    }
    else {
      let user = JSON.parse(userStringified);

      this.isLoggedIn = new BehaviorSubject<boolean>(true);
      this.authToken = new BehaviorSubject<string>(user.authToken);
      this.role = new BehaviorSubject<string>(user.role);
      this.id = new BehaviorSubject<number>(user.id);
      console.log(this.role);
    }
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
    sessionStorage.removeItem("user");
    this.setIsLoggedIn(false);
    this.setRole("");
    this.setId(0);
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

  getIsLoggedInValue(): boolean {
    return this.isLoggedIn.getValue();
  }

  setIsLoggedIn(value: boolean): void {
    this.isLoggedIn.next(value);
  }

  getAuthToken(): Observable<string> {
    return this.authToken.asObservable();
  }

  setAuthToken(authToken: string): void {
    this.authToken.next(authToken);
  }

  getRole(): Observable<string> {
    return this.role.asObservable();
  }

  getRoleValue(): string {
    return this.role.getValue();
  }

  setRole(role: string): void {
    this.role.next(role);
  }

  setId(id: number): void {
    this.id.next(id);
  }

  getId(): number {
    return this.id.value;
  }
}

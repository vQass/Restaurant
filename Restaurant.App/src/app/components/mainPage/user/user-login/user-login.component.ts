import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/ApiServices/user.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { ValidationConsts } from 'src/app/Validation/ValidationConsts';
import { UserLoginRequest } from 'src/models/user/UserLoginRequest';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {
  minPassLength = ValidationConsts.MIN_PASSWORD_LENGTH;
  maxPassLength = ValidationConsts.MAX_PASSWORD_LENGTH;

  singleControlMatcher = new SingleControlErrorStateMatcher();

  loginForm: FormGroup;

  constructor(fb: FormBuilder, private userService: UserService, private toastService: ToastService, private router: Router) {
    this.loginForm = fb.group({
      email: fb.control('', [Validators.required, Validators.email]),
      password: fb.control('', [Validators.required, Validators.minLength(this.minPassLength), Validators.maxLength(this.maxPassLength)])
    })
  }

  ngOnInit(): void {
  };

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onSubmit() {
    let user = { email: this.loginForm.value.email, password: this.loginForm.value.password } as UserLoginRequest;
    this.userService.login(user).subscribe({
      next: (resp) => {
        console.log(resp)
        this.userService.setIsLoggedIn(true);
        this.userService.setRole(resp.role);
        sessionStorage.setItem('authToken', resp.token);
        this.toastService.showSuccess("Pomyślnie zalogowano!")
        this.router.navigate(['home']);
      },
      error: (e) => {
        this.toastService.showDanger("Błąd logowania: " + e.message);
      }
    });
  }
}

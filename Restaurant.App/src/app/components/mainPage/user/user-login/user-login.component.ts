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
  disableSubmitButton = false;

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
    this.disableSubmitButton = true;

    let user = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    } as UserLoginRequest;

    this.userService.loginRequest(user).subscribe({
      next: (resp) => {
        this.userService.login(resp);
      },
      error: (e) => {
        this.disableSubmitButton = false;
        this.toastService.showDanger("Błąd logowania: " + e.message);
      }
    });
  }
}

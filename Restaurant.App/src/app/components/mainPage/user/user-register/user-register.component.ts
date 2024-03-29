import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/ApiServices/user.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { FormGroupErrorStateMatcher, SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { ValidationConsts } from 'src/app/Validation/ValidationConsts';
import { UserCreateRequest } from 'src/models/user/UserCreateRequest';

@Component
  ({
    selector: 'app-user-register',
    templateUrl: './user-register.component.html',
    styleUrls: ['./user-register.component.scss']
  })

export class UserRegisterComponent implements OnInit {
  minPassLength = ValidationConsts.MIN_PASSWORD_LENGTH;
  maxPassLength = ValidationConsts.MAX_PASSWORD_LENGTH;

  singleControlMatcher = new SingleControlErrorStateMatcher();
  groupMatcher = new FormGroupErrorStateMatcher();

  registerForm: FormGroup;

  disableSubmitButton = false;

  constructor(fb: FormBuilder, private userService: UserService, private toastService: ToastService, private router: Router) {
    this.registerForm = fb.group({
      email: fb.control('', [Validators.required, Validators.email]),
      passwordGroup: fb.group(
        {
          password: fb.control('', [Validators.required, Validators.minLength(this.minPassLength), Validators.maxLength(this.maxPassLength)]),
          confirmPassword: fb.control('', [Validators.required])
        }, { validators: this.checkPasswords }
      )
    }
    )
  };

  ngOnInit(): void {

  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('passwordGroup.password');
  }

  get confirmPassword() {
    return this.registerForm.get('passwordGroup.confirmPassword');
  }

  get PasswordGroupForm() {
    return this.registerForm.get('passwordGroup');
  }

  checkPasswords: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    let pass = group.get('password')?.value;
    let confirmPass = group.get('confirmPassword')?.value
    return pass === confirmPass ? null : { notSame: true }
  }

  onSubmit() {
    this.disableSubmitButton = true;
    let user = { email: this.registerForm.value.email, ...this.registerForm.value.passwordGroup } as UserCreateRequest;
    this.userService.addUser(user).subscribe({
      next: (resp) => {
        this.toastService.showSuccess("Pomyślnie zarejestrowano! \n Można przystąpić do logowania!")
        this.disableSubmitButton = false;
        this.router.navigate(['home']);
      },
      error: (e) => {
        this.disableSubmitButton = false;
        this.toastService.showDanger("Błąd podczas rejestracji: " + e.message);
      }
    })
  }
}


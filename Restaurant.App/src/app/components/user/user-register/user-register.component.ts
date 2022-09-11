import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { FormGroupErrorStateMatcher, SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { ValidationConsts } from 'src/app/Validation/ValidationConsts';

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

  registerForm;

  constructor(private fb: FormBuilder) {
    this.registerForm = fb.group({
      email: fb.control(null, [Validators.required, Validators.email]),
      password: fb.control(null, [Validators.required, Validators.minLength(this.minPassLength), Validators.maxLength(this.maxPassLength)]),
      confirmPassword: fb.control(null)
    }
      , { validators: this.checkPasswords }
    )
  };

  ngOnInit(): void {

  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }

  checkPasswords: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    let pass = group.get('password')?.value;
    let confirmPass = group.get('confirmPassword')?.value
    return pass === confirmPass ? null : { notSame: true }
  }
}

import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
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

  passwordFormGroup: FormGroup;

  singleControlMatcher = new SingleControlErrorStateMatcher();
  groupMatcher = new FormGroupErrorStateMatcher();

  mainForm = new FormGroup({

  });

  emailFormControl = new FormControl('', [Validators.required, Validators.email]);

  constructor(private fb: FormBuilder) {
    this.passwordFormGroup = this.fb.group({
      password: ['', [Validators.required, Validators.minLength(this.minPassLength), Validators.maxLength(this.maxPassLength)]],
      confirmPassword: ['']
    },
      { validators: this.checkPasswords })
  }

  ngOnInit(): void {
  }

  checkPasswords: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    let pass = group.get('password')?.value;
    let confirmPass = group.get('confirmPassword')?.value
    return pass === confirmPass ? null : { notSame: true }
  }
}

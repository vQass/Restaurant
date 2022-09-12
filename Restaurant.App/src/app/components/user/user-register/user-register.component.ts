import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/ApiServices/user.service';
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

  constructor(private fb: FormBuilder, private userService: UserService) {
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
    let user = { email: this.registerForm.value.email, ...this.registerForm.value.passwordGroup } as UserCreateRequest;
    this.userService.addUser(user).subscribe().unsubscribe();

  }
}

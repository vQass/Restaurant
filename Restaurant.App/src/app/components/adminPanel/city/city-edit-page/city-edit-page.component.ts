import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CityService } from 'src/app/services/ApiServices/city.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { CityUpdateRequest } from 'src/models/city/CityUpdateRequest';

@Component({
  selector: 'app-city-edit-page',
  templateUrl: './city-edit-page.component.html',
  styleUrls: ['./city-edit-page.component.scss']
})
export class CityEditPageComponent {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;

  constructor(
    fb: FormBuilder,
    private cityService: CityService,
    private toastService: ToastService,
    private router: Router) {
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  get name() {
    return this.mainForm.get('name');
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let city =
      {
        name: this.mainForm.value.name,
      } as CityUpdateRequest;

    this.cityService.update(city, id).subscribe({ // get data for id passed in router
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie dodano miasto!", 2000)
        this.router.navigate(['city-admin-main-page']);
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas dodawania miasta: " + e.message);
      }
    });
  }
}

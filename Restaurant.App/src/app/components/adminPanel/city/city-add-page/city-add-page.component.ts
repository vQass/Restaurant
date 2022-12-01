import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CityService } from 'src/app/services/ApiServices/city.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { CityCreateRequest } from 'src/models/city/CityCreateRequest';

@Component({
  selector: 'app-city-add-page',
  templateUrl: './city-add-page.component.html',
  styleUrls: ['./city-add-page.component.scss']
})
export class CityAddPageComponent {
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
      } as CityCreateRequest;

    this.cityService.add(city).subscribe({
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

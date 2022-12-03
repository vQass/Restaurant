import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { CityService } from 'src/app/services/ApiServices/city.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { CityCreateRequest } from 'src/models/city/CityCreateRequest';

@Component({
  selector: 'app-city-add-page',
  templateUrl: './city-add-page.component.html',
  styleUrls: ['./city-add-page.component.scss']
})
export class CityAddPageComponent extends PagingHelper {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;

  constructor(
    fb: FormBuilder,
    private cityService: CityService,
    private toastService: ToastService,
    route: ActivatedRoute,
    router: Router) {
    super(route, router)
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
        this.goToMainPage();
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas dodawania miasta: " + e.message);
      }
    });
  }

  goToMainPage() {
    this.goToPage(
      this.getPageIndex(),
      this.getPageSize(),
      'city-admin-main-page');
  }
}

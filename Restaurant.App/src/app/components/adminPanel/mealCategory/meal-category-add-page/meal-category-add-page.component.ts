import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { MealCategoryService } from 'src/app/services/ApiServices/meal-category.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { MealCategoryCreateRequest } from 'src/models/mealCategory/MealCategoryCreateRequest';

@Component({
  selector: 'app-meal-category-add-page',
  templateUrl: './meal-category-add-page.component.html',
  styleUrls: ['./meal-category-add-page.component.scss']
})
export class MealCategoryAddPageComponent extends PagingHelper {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;

  constructor(
    fb: FormBuilder,
    private mealCategoryService: MealCategoryService,
    private toastService: ToastService,
    router: Router,
    route: ActivatedRoute) {
    super(route, router, 'meal-category-main-page')
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  get name() {
    return this.mainForm.get('name');
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let mealCategory =
      {
        name: this.mainForm.value.name,
      } as MealCategoryCreateRequest;

    this.mealCategoryService.add(mealCategory).subscribe({
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie dodano kategorię!", 2000)
        this.goToMainPage();
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas dodawania kategorii: " + e.message);
      }
    });
  }
}

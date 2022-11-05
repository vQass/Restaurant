import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { MealCreateRequest } from 'src/models/meal/MealCreateRequest';

@Component({
  selector: 'app-add-meal-page',
  templateUrl: './add-meal-page.component.html',
  styleUrls: ['./add-meal-page.component.scss']
})
export class AddMealPageComponent {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  addMealForm: FormGroup;
  disableSubmitButton = false;

  constructor(
    fb: FormBuilder,
    private mealService: MealService,
    private toastService: ToastService,
    private router: Router) {
    this.addMealForm = fb.group({
      name: fb.control('', [Validators.required]),
      price: fb.control('', [Validators.required]),
      categoryId: fb.control('', [Validators.required]),
    })
  }

  get name() {
    return this.addMealForm.get('name');
  }

  get price() {
    return this.addMealForm.get('price');
  }

  get categoryId() {
    return this.addMealForm.get('categoryId');
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let meal =
      {
        name: this.addMealForm.value.name,
        price: this.addMealForm.value.price,
        categoryId: this.addMealForm.value.categoryId
      } as MealCreateRequest;

    this.mealService.addMeal(meal).subscribe({
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie dodano danie!", 2000)
        this.router.navigate(['meal-admin-main-page']);
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas dodawania dania: " + e.message);
      }
    });
  }
}
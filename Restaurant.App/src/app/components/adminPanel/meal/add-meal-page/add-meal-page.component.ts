import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { MealCategoryService } from 'src/app/services/ApiServices/meal-category.service';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { MealCreateRequest } from 'src/models/meal/MealCreateRequest';
import { MealCategory } from 'src/models/mealCategory/MealCategory';

@Component({
  selector: 'app-add-meal-page',
  templateUrl: './add-meal-page.component.html',
  styleUrls: ['./add-meal-page.component.scss']
})
export class AddMealPageComponent extends PagingHelper implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  addMealForm: FormGroup;
  disableSubmitButton = false;
  selectedValue?: string;
  categories?: MealCategory[];

  constructor(
    fb: FormBuilder,
    private mealService: MealService,
    private toastService: ToastService,
    private mealCategoryService: MealCategoryService,
    route: ActivatedRoute,
    router: Router) {
    super(route, router, 'meal-admin-main-page')
    this.addMealForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
      price: fb.control('', [Validators.required, Validators.min(0.01), Validators.max(500), Validators.pattern('')]),
      mealCategoryId: fb.control('', [Validators.required]),
    })
  }

  ngOnInit(): void {
    this.mealCategoryService
      .getMealCategories()
      .subscribe((data) => this.categories = data);
  }

  get name() {
    return this.addMealForm.get('name');
  }

  get price() {
    return this.addMealForm.get('price');
  }

  get mealCategoryId() {
    return this.addMealForm.get('mealCategoryId');
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let meal =
      {
        name: this.addMealForm.value.name,
        price: this.addMealForm.value.price,
        mealCategoryId: this.addMealForm.value.mealCategoryId
      } as MealCreateRequest;

    this.mealService.add(meal).subscribe({
      next: () => {
        this.disableSubmitButton = false;
        this.toastService.showSuccess("Pomyślnie dodano danie!", 2000)
        this.goToMainPage();
      },
      error: (e) => {
        this.disableSubmitButton = false;
        this.toastService.showDanger("Błąd podczas dodawania dania: " + e.message);
      }
    });
  }
}

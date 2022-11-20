import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';
import { MealUpdateRequest } from 'src/models/meal/MealUpdateRequest';

@Component({
  selector: 'app-edit-meal-page',
  templateUrl: './edit-meal-page.component.html',
  styleUrls: ['./edit-meal-page.component.scss']
})
export class EditMealPageComponent implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;

  mealId: number = 0;

  meal!: MealAdminPanelItem;

  ngOnInit(): void {

  }

  constructor(
    fb: FormBuilder,
    private route: ActivatedRoute,
    private mealService: MealService,
    private toastService: ToastService,
    private router: Router) {
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required]),
      price: fb.control('', [Validators.required]),
      mealCategoryId: fb.control('', [Validators.required]),
    })

    let id = this.route.snapshot.paramMap.get('id');

    if (id != null) {
      let parsedId = parseInt(id);
      if (!isNaN(parsedId)) {
        this.mealId = parsedId;
        this.mealService.getMealAdminPanelItem(this.mealId).subscribe((data) => { this.meal = data }
        );
      }
    }
  }

  get name() {
    return this.mainForm.get('name');
  }

  get price() {
    return this.mainForm.get('price');
  }

  get mealCategoryId() {
    return this.mainForm.get('mealCategoryId');
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let meal =
      {
        name: this.mainForm.value.name,
        price: this.mainForm.value.price,
        mealCategoryId: this.mainForm.value.mealCategoryId
      } as MealUpdateRequest;

    this.mealService.updateMeal(this.mealId, meal).subscribe({
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie zaktualizowano danie!", 2000)

        this.router.navigate(['/edit-meal-options-admin-page',
          {
            id: this.mealId
          }]);

      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas aktualizacji dania: " + e.message);
      }
    });
  }

}

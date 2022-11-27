import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MealCategoryService } from 'src/app/services/ApiServices/meal-category.service';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';
import { MealUpdateRequest } from 'src/models/meal/MealUpdateRequest';
import { MealCategory } from 'src/models/mealCategory/MealCategory';

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

  meal?: MealAdminPanelItem;
  categories?: MealCategory[];


  ngOnInit(): void {

  }

  constructor(
    fb: FormBuilder,
    private route: ActivatedRoute,
    private mealService: MealService,
    private mealCategoryService: MealCategoryService,
    private toastService: ToastService,
    private router: Router) {
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
      price: fb.control('', [Validators.required, Validators.min(0.01), Validators.max(500), Validators.pattern('')]),
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

    mealCategoryService.getMealCategories().subscribe((data) => this.categories = data);
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

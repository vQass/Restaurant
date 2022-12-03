import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { MealCategoryService } from 'src/app/services/ApiServices/meal-category.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { MealCategory } from 'src/models/mealCategory/MealCategory';

@Component({
  selector: 'app-meal-category-edit-page',
  templateUrl: './meal-category-edit-page.component.html',
  styleUrls: ['./meal-category-edit-page.component.scss']
})
export class MealCategoryEditPageComponent extends PagingHelper {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;
  id?: number;
  mealCategory?: MealCategory;

  constructor(
    fb: FormBuilder,
    private mealCategoryService: MealCategoryService,
    private toastService: ToastService,
    route: ActivatedRoute,
    router: Router) {
    super(route, router)
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  ngOnInit(): void {

    let tempId = this.route.snapshot.paramMap.get('id');
    if (tempId == null) {
      this.toastService.showDanger('Błąd podczas pobierania identyfikatora ketegorii dań!');
      this.goToMainPage();
      return;
    }

    let parsedId = parseInt(tempId);
    if (isNaN(parsedId)) {
      this.toastService.showDanger('Błąd podczas przetwarzania identyfikatora kategorii dań!');
      this.goToMainPage();
      return;
    }

    this.id = parsedId;
    this.mealCategoryService
      .getMealCategory(this.id)
      .subscribe((data) => this.mealCategory = data);
  }

  get name() {
    return this.mainForm.get('name');
  }

  onSubmit() {
    if (this.mealCategory == null) {
      this.toastService.showDanger("Błąd z pobraną kategorią!", 4000)
      return;
    }

    this.disableSubmitButton = true;

    let mealCategory =
      {
        name: this.mainForm.value.name,
      } as MealCategory;

    this.mealCategoryService.update(mealCategory, this.mealCategory.id).subscribe({
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie zaktualizowano kategorię!", 2000)
        this.goToMainPage();
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas aktualizacji kategorii: " + e.message);
      }
    });
  }

  goToMainPage() {
    this.goToPage(
      this.getPageIndex(),
      this.getPageSize(),
      'meal-category-main-page');
  }
}

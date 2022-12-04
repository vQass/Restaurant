import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { MealCategoryService } from 'src/app/services/ApiServices/meal-category.service';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { IdentifierParserService } from 'src/app/services/OtherServices/identifier-parser.service';
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
export class EditMealPageComponent extends PagingHelper implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;

  mealId: number = 0;

  meal?: MealAdminPanelItem;
  categories?: MealCategory[];

  constructor(
    fb: FormBuilder,
    private mealService: MealService,
    private mealCategoryService: MealCategoryService,
    private idParser: IdentifierParserService,
    private toastService: ToastService,
    route: ActivatedRoute,
    router: Router) {
    super(route, router, '/edit-meal-options-admin-page')
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
      price: fb.control('', [Validators.required, Validators.min(0.01), Validators.max(500), Validators.pattern('')]),
      mealCategoryId: fb.control('', [Validators.required]),
    })
  }

  ngOnInit(): void {
    let id = this.idParser.parseId(this.route);

    if (id == null) {
      this.goToMainPage();
      return;
    }

    this.mealService.get(id).subscribe((data) => {
      this.meal = data;
    });
    this.mealCategoryService.getMealCategories().subscribe((data) => { this.categories = data });
    ;
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
    if (this.meal == null) {
      return;
    }
    const id = this.meal.id;

    this.disableSubmitButton = true;

    let meal =
      {
        name: this.mainForm.value.name,
        price: this.mainForm.value.price,
        mealCategoryId: this.mainForm.value.mealCategoryId
      } as MealUpdateRequest;

    this.mealService.update(meal, id).subscribe({
      next: () => {
        this.disableSubmitButton = false;
        this.toastService.showSuccess("Pomyślnie zaktualizowano danie!", 2000)
        this.goToOptionsPage(id);
      },
      error: (e) => {
        this.disableSubmitButton = false;
        this.toastService.showDanger("Błąd podczas aktualizacji dania: " + e.message);
      }
    });
  }

  goBack() {
    if (this.meal == null) {
      return;
    }
    this.goToOptionsPage(this.meal.id);
  }
}

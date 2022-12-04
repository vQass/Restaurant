import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { IdentifierParserService } from 'src/app/services/OtherServices/identifier-parser.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';

@Component({
  selector: 'app-edit-meal-price-page',
  templateUrl: './edit-meal-price-page.component.html',
  styleUrls: ['./edit-meal-price-page.component.scss']
})
export class EditMealPricePageComponent extends PagingHelper implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;

  meal?: MealAdminPanelItem;

  constructor(
    fb: FormBuilder,
    private mealService: MealService,
    private toastService: ToastService,
    private idParser: IdentifierParserService,
    route: ActivatedRoute,
    router: Router) {
    super(route, router, '/edit-meal-options-admin-page')
    this.mainForm = fb.group({
      price: fb.control('', [Validators.required, Validators.min(0.01), Validators.max(500)]),
    })

  }

  ngOnInit(): void {
    const id = this.idParser.parseId(this.route);

    if (id == null) {
      this.goToMainPage();
      return;
    }

    this.mealService.get(id).subscribe((data) => { this.meal = data });
  }

  get price() {
    return this.mainForm.get('price');
  }

  onSubmit() {
    if (this.meal == null) {
      return;
    }
    const id = this.meal.id;

    this.disableSubmitButton = true;

    let price = this.mainForm.value.price;

    this.mealService.updatePrice(id, price).subscribe({
      next: () => {
        this.disableSubmitButton = false;
        this.toastService.showSuccess("Pomyślnie zaktualizowano cenę!", 2000)
        this.goToOptionsPage(id)
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas aktualizacji ceny: " + e.message);
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

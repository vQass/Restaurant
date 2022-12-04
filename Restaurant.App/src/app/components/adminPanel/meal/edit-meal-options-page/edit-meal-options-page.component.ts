import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { IdentifierParserService } from 'src/app/services/OtherServices/identifier-parser.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';

@Component({
  selector: 'app-edit-meal-options-page',
  templateUrl: './edit-meal-options-page.component.html',
  styleUrls: ['./edit-meal-options-page.component.scss']
})
export class EditMealOptionsPageComponent extends PagingHelper implements OnInit {
  buttonActive = true;
  meal?: MealAdminPanelItem;

  constructor(
    private mealService: MealService,
    private toastService: ToastService,
    private idParser: IdentifierParserService,
    route: ActivatedRoute,
    router: Router) {
    super(route, router, '/meal-admin-main-page')
  }

  ngOnInit() {
    const id = this.idParser.parseId(this.route);

    if (id == null) {
      this.goToMainPage();
      return;
    }

    this.mealService
      .get(id)
      .subscribe((data) => this.meal = data);
  }

  goToMealEditPage() {
    this.goToPage(
      this.getPageIndex(),
      this.getPageSize(),
      '/edit-meal-page/' + this.meal?.id);
  }

  goToMealEditPricePage() {
    this.goToPage(
      this.getPageIndex(),
      this.getPageSize(),
      '/edit-meal-price-page/' + this.meal?.id);
  }

  goToMealEditRecipePage() {
    this.goToPage(
      this.getPageIndex(),
      this.getPageSize(),
      '/edit-meal-recipe-page/' + this.meal?.id);
  }

  setAsAvailable() {
    if (this.meal?.id == null) {
      this.toastService.showDanger("Błąd podczas odczytu identyfikatora dania");
      return;
    }

    this.buttonActive = false;

    this.mealService
      .setAsAvailable(this.meal.id)
      .subscribe({
        next: () => {
          this.buttonActive = true;

          if (this.meal != null) {
            this.meal.available = true;
          }

          this.toastService.showSuccess("Pomyślnie zmieniono aktywność dania!", 1000)
        },
        error: (e) => {
          this.buttonActive = true;
          this.toastService.showDanger("Błąd podczas zmieniania aktywności dania: \n" + e.message, 3000);
        }
      });
  }

  setAsUnavailable() {
    if (this.meal?.id == null) {
      this.toastService.showDanger("Błąd podczas odczytu identyfikatora dania");
      return;
    }

    this.buttonActive = false;

    this.mealService
      .setAsUnavailable(this.meal?.id)
      .subscribe({
        next: () => {
          this.buttonActive = true;

          if (this.meal != null) {
            this.meal.available = false;
          }

          this.toastService.showSuccess("Pomyślnie zmieniono aktywność dania!", 1000)
        },
        error: (e) => {
          this.buttonActive = true;
          this.toastService.showDanger("Błąd podczas zmieniania aktywności dania: \n" + e.message, 3000);
        }
      });
  }

  goBack() {
    this.goToMainPage();
  }
}

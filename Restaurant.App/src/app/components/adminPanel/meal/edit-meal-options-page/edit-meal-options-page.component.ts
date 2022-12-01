import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';

@Component({
  selector: 'app-edit-meal-options-page',
  templateUrl: './edit-meal-options-page.component.html',
  styleUrls: ['./edit-meal-options-page.component.scss']
})
export class EditMealOptionsPageComponent implements OnInit {
  buttonActive;
  id?: number;
  meal?: MealAdminPanelItem;

  constructor(
    private route: ActivatedRoute,
    private mealService: MealService,
    private toastService: ToastService,
    private router: Router) {
    this.buttonActive = true;
  }

  ngOnInit() {
    let tempId = this.route.snapshot.paramMap.get('id');
    if (tempId != null) {
      let parsedId = parseInt(tempId);
      if (!isNaN(parsedId)) {
        this.id = parsedId;
        this.mealService
          .getMealAdminPanelItem(this.id)
          .subscribe((data) => this.meal = data);
      }
    }
  }

  goToMealEditPage() {
    this.goToPage('/edit-meal-page');
  }

  goToMealEditPricePage() {
    this.goToPage('/edit-meal-price-page');
  }

  goToMealEditRecipePage() {
    this.goToPage('/edit-meal-recipe-page');
  }

  goToPage(link: string) {
    this.router.navigate([link,
      {
        id: this.id
      }]);
  }

  setAsAvailable() {

    if (this.id == null) {
      return;
    }

    this.buttonActive = false;

    this.mealService
      .setAsAvailable(this.id)
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

    if (this.id == null) {
      return;
    }

    this.buttonActive = false;

    this.mealService
      .setAsUnavailable(this.id)
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
}

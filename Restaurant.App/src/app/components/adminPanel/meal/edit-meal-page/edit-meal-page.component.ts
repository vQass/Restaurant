import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';

@Component({
  selector: 'app-edit-meal-page',
  templateUrl: './edit-meal-page.component.html',
  styleUrls: ['./edit-meal-page.component.scss']
})
export class EditMealPageComponent implements OnInit {
  buttonActive;
  id?: string | null;
  name?: string | null;
  price?: string | null;
  available?: boolean | null;
  categoryId?: string | null;

  constructor(
    private route: ActivatedRoute,
    private mealService: MealService,
    private toastService: ToastService) {
    this.buttonActive = true;
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.name = this.route.snapshot.paramMap.get('name');
    this.price = this.route.snapshot.paramMap.get('price');
    this.available = this.route.snapshot.paramMap.get('available') === "true";
    this.categoryId = this.route.snapshot.paramMap.get('categoryId');
  }

  setAsAvailable() {

    if (this.id == null) {
      return;
    }

    this.buttonActive = false;

    this.mealService
      .setAsAvailable(parseInt(this.id)
      ).subscribe({
        next: () => {
          this.buttonActive = true;
          this.available = true;
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
      .setAsUnavailable(parseInt(this.id))
      .subscribe({
        next: () => {
          this.buttonActive = true;
          this.available = false;
          this.toastService.showSuccess("Pomyślnie zmieniono aktywność dania!", 1000)
        },
        error: (e) => {
          this.buttonActive = true;
          this.toastService.showDanger("Błąd podczas zmieniania aktywności dania: \n" + e.message, 3000);
        }
      });

  }
}

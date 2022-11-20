import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';

@Component({
  selector: 'app-edit-meal-price-page',
  templateUrl: './edit-meal-price-page.component.html',
  styleUrls: ['./edit-meal-price-page.component.scss']
})
export class EditMealPricePageComponent implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;

  mealId: number = 0;

  meal!: MealAdminPanelItem;

  constructor(
    fb: FormBuilder,
    private route: ActivatedRoute,
    private mealService: MealService,
    private toastService: ToastService,
    private router: Router) {
    this.mainForm = fb.group({
      price: fb.control('', [Validators.required]),
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

  ngOnInit(): void {
  }

  get price() {
    return this.mainForm.get('price');
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let price = this.mainForm.value.price;

    console.log(price);


    this.mealService.updateMealPrice(this.mealId, price).subscribe({
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie zaktualizowano cenę!", 2000)

        this.router.navigate(['/edit-meal-options-admin-page',
          {
            id: this.mealId
          }]);

      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas aktualizacji ceny: " + e.message);
      }
    });
  }


}

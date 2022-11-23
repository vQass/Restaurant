import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeService } from 'src/app/services/ApiServices/recipe.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { RecipeEditViewModel } from 'src/models/recipe/RecipeEditViewModel';
import { RecipeIngredient } from 'src/models/recipe/RecipeIngredient';

@Component({
  selector: 'app-edit-meal-recipe-page',
  templateUrl: './edit-meal-recipe-page.component.html',
  styleUrls: ['./edit-meal-recipe-page.component.scss']
})
export class EditMealRecipePageComponent implements OnInit {

  disableSubmitButton = false;
  recipeViewModel?: RecipeEditViewModel;
  firstHalfOfRecipe?: RecipeIngredient[];
  secondHalfOfRecipe?: RecipeIngredient[];
  mealId: number = 0;

  constructor(private recipeService: RecipeService,
    private route: ActivatedRoute,
    private toastService: ToastService,
    private router: Router) {

    let id = this.route.snapshot.paramMap.get('id');

    if (id != null) {
      let parsedId = parseInt(id);
      if (!isNaN(parsedId)) {
        this.mealId = parsedId;
        this.recipeService.getRecipeEditViewModel(this.mealId).subscribe((data) => {
          this.recipeViewModel = data;
          let half = Math.ceil(this.recipeViewModel.ingredients.length / 2);
          this.firstHalfOfRecipe = this.recipeViewModel.ingredients.slice(0, half)
          this.secondHalfOfRecipe = this.recipeViewModel.ingredients.slice(-half)
        });
      }
    }

  }

  ngOnInit(): void {
  }

  onSubmit() {
    let newRecipe = this.recipeViewModel?.ingredients.filter(x => x.isInRecipe)

    if (newRecipe == null) {
      return;
    }

    this.recipeService.updateMealRecipe(this.mealId, newRecipe.map(x => x.ingredientId)).subscribe({
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie zaktualizowano przepis!", 2000)

        this.router.navigate(['/edit-meal-options-admin-page',
          {
            id: this.mealId
          }]);

      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas aktualizacji przepisu: " + e.message);
      }
    });
  }

}

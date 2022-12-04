import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, Observable, shareReplay } from 'rxjs';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { RecipeService } from 'src/app/services/ApiServices/recipe.service';
import { IdentifierParserService } from 'src/app/services/OtherServices/identifier-parser.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { RecipeEditViewModel } from 'src/models/recipe/RecipeEditViewModel';
import { RecipeIngredient } from 'src/models/recipe/RecipeIngredient';

@Component({
  selector: 'app-edit-meal-recipe-page',
  templateUrl: './edit-meal-recipe-page.component.html',
  styleUrls: ['./edit-meal-recipe-page.component.scss']
})
export class EditMealRecipePageComponent extends PagingHelper implements OnInit {
  isMobileView$: Observable<boolean> = this.breakpointObserver.observe('(max-width: 450px)')
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  disableSubmitButton = false;
  recipeViewModel?: RecipeEditViewModel;
  firstHalfOfRecipe?: RecipeIngredient[];
  secondHalfOfRecipe?: RecipeIngredient[];

  constructor(private recipeService: RecipeService,
    private toastService: ToastService,
    private breakpointObserver: BreakpointObserver,
    private idParser: IdentifierParserService,
    route: ActivatedRoute,
    router: Router) {
    super(route, router, '/edit-meal-options-admin-page')
  }

  ngOnInit(): void {
    const id = this.idParser.parseId(this.route);

    if (id == null) {
      this.goToMainPage();
      return;
    }

    this.recipeService.getRecipeEditViewModel(id).subscribe((data) => {
      this.recipeViewModel = data;
      let half = Math.ceil(this.recipeViewModel.ingredients.length / 2);
      this.firstHalfOfRecipe = this.recipeViewModel.ingredients.slice(0, half)
      this.secondHalfOfRecipe = this.recipeViewModel.ingredients.slice(-half)
    })
  }

  onSubmit() {
    if (this.recipeViewModel?.mealId == null) {
      return;
    }
    const id = this.recipeViewModel?.mealId;

    let newRecipe = this.recipeViewModel?.ingredients.filter(x => x.isInRecipe)
    if (newRecipe == null) {
      return;
    }

    this.recipeService.updateMealRecipe(id, newRecipe.map(x => x.ingredientId)).subscribe({
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie zaktualizowano przepis!", 2000)
        this.goToOptionsPage(id);
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas aktualizacji przepisu: " + e.message);
      }
    });
  }

  goBack() {
    if (this.recipeViewModel == null) {
      return;
    }
    this.goToOptionsPage(this.recipeViewModel.mealId);
  }
}

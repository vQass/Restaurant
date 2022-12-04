import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { IngredientService } from 'src/app/services/ApiServices/ingredient.service';
import { IdentifierParserService } from 'src/app/services/OtherServices/identifier-parser.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { Ingredient } from 'src/models/ingredient/Ingredient';
import { IngredientUpdateRequest } from 'src/models/ingredient/IngredientUpdateRequest';

@Component({
  selector: 'app-edit-ingredient-page',
  templateUrl: './edit-ingredient-page.component.html',
  styleUrls: ['./edit-ingredient-page.component.scss']
})
export class EditIngredientPageComponent extends PagingHelper implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;
  ingredient?: Ingredient;

  constructor(
    fb: FormBuilder,
    private ingredientService: IngredientService,
    private toastService: ToastService,
    private idParser: IdentifierParserService,
    router: Router,
    route: ActivatedRoute) {
    super(route, router, 'ingredient-admin-main-page')
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  ngOnInit(): void {

    const id = this.idParser.parseId(this.route);

    if (id == undefined) {
      this.goToMainPage();
      return;
    }

    this.ingredientService
      .get(id)
      .subscribe((data) => this.ingredient = data);
  }

  get name() {
    return this.mainForm.get('name');
  }

  onSubmit() {
    if (this.ingredient == null) {
      this.toastService.showDanger("Błąd z pobranym składnikiem!", 4000)
      return;
    }

    this.disableSubmitButton = true;

    let ingredient =
      {
        name: this.mainForm.value.name
      } as IngredientUpdateRequest;

    this.ingredientService.update(ingredient, this.ingredient.id).subscribe({
      next: () => {
        this.toastService.showSuccess("Pomyślnie dodano składnik!", 2000)
        this.goToMainPage();
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas dodawania składnika: " + e.message);
      }
    });
  }
}

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IngredientService } from 'src/app/services/ApiServices/ingredient.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { IngredientCreateRequest } from 'src/models/ingredient/IngredientCreateRequest';

@Component({
  selector: 'app-add-ingredient-page',
  templateUrl: './add-ingredient-page.component.html',
  styleUrls: ['./add-ingredient-page.component.scss']
})
export class AddIngredientPageComponent {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  addIngredientForm: FormGroup;
  disableSubmitButton = false;

  constructor(
    fb: FormBuilder,
    private ingredientService: IngredientService,
    private toastService: ToastService,
    private router: Router) {
    this.addIngredientForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  get name() {
    return this.addIngredientForm.get('name');
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let ingredient =
      {
        name: this.addIngredientForm.value.name
      } as IngredientCreateRequest;

    this.ingredientService.addIngredient(ingredient).subscribe({
      next: () => {
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie dodano składnik!", 2000)
        this.router.navigate(['ingredient-admin-main-page']);
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas dodawania składnika: " + e.message);
      }
    });
  }
}

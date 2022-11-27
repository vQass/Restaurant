import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IngredientService } from 'src/app/services/ApiServices/ingredient.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { IngredientAdminPanelItem } from 'src/models/ingredient/IngredientAdminPanelItem';
import { IngredientUpdateRequest } from 'src/models/ingredient/IngredientUpdateRequest';

@Component({
  selector: 'app-edit-ingredient-page',
  templateUrl: './edit-ingredient-page.component.html',
  styleUrls: ['./edit-ingredient-page.component.scss']
})
export class EditIngredientPageComponent implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  editIngredientForm: FormGroup;
  disableSubmitButton = false;
  ingredient?: IngredientAdminPanelItem;
  ingredientId: number = 0;

  constructor(
    fb: FormBuilder,
    private ingredientService: IngredientService,
    private toastService: ToastService,
    private router: Router) {
    this.editIngredientForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  ngOnInit(): void {
    this.ingredient = history.state.data;
    this.ingredientId = history.state.data.id;
  }


  get name() {
    return this.editIngredientForm.get('name');
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let ingredient =
      {
        name: this.editIngredientForm.value.name
      } as IngredientUpdateRequest;

    this.ingredientService.editIngredient(this.ingredientId, ingredient).subscribe({
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

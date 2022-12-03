import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IngredientService } from 'src/app/services/ApiServices/ingredient.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { Ingredient } from 'src/models/ingredient/Ingredient';
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
  ingredient?: Ingredient;
  id: number = 0;

  constructor(
    fb: FormBuilder,
    private ingredientService: IngredientService,
    private toastService: ToastService,
    private router: Router,
    private route: ActivatedRoute) {
    this.editIngredientForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  ngOnInit(): void {

    let tempId = this.route.snapshot.paramMap.get('id');
    if (tempId == null) {
      this.toastService.showDanger('Błąd podczas pobierania danych o składniku!');
      this.router.navigate(['ingredient-admin-main-page']);
      return;
    }

    let parsedId = parseInt(tempId);
    if (isNaN(parsedId)) {
      this.toastService.showDanger('Błąd podczas przetwarzania identyfikatora składnika!');
      this.router.navigate(['ingredient-admin-main-page']);
      return;
    }

    this.id = parsedId;
    this.ingredientService
      .get(this.id)
      .subscribe((data) => this.ingredient = data);
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

    this.ingredientService.edit(this.id, ingredient).subscribe({
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

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { CityService } from 'src/app/services/ApiServices/city.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { City } from 'src/models/city/City';
import { CityUpdateRequest } from 'src/models/city/CityUpdateRequest';

@Component({
  selector: 'app-city-edit-page',
  templateUrl: './city-edit-page.component.html',
  styleUrls: ['./city-edit-page.component.scss']
})
export class CityEditPageComponent extends PagingHelper implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;
  id?: number;
  city?: City;

  constructor(
    fb: FormBuilder,
    private cityService: CityService,
    private toastService: ToastService,
    router: Router,
    route: ActivatedRoute) {
    super(route, router);
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  ngOnInit(): void {
    let tempId = this.route.snapshot.paramMap.get('id');
    if (tempId == null) {
      this.toastService.showDanger('Błąd podczas pobierania identyfikatora składnika!');
      this.goToMainPage();
      return;
    }

    let parsedId = parseInt(tempId);
    if (isNaN(parsedId)) {
      this.toastService.showDanger('Błąd podczas przetwarzania identyfikatora składnika!');
      this.goToMainPage();
      return;
    }

    this.id = parsedId;
    this.cityService
      .getCity(this.id)
      .subscribe((data) => this.city = data);
  }

  get name() {
    return this.mainForm.get('name');
  }

  onSubmit() {
    if (this.city == null) {
      this.toastService.showDanger("Błąd z pobranym miastem!", 4000)
      return;
    }

    this.disableSubmitButton = true;

    let city =
      {
        name: this.mainForm.value.name,
      } as CityUpdateRequest;

    this.cityService.update(city, this.city.id).subscribe({
      next: () => {
        this.toastService.showSuccess("Pomyślnie zaktualizowano miasto!", 2000);
        this.goToMainPage();
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas aktualizacji miasta: " + e.message);
      }
    });
  }

  goToMainPage() {
    this.goToPage(
      this.getPageIndex(),
      this.getPageSize(),
      'city-admin-main-page');
  }
}

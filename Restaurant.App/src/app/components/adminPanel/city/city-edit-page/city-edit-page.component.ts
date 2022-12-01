import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
export class CityEditPageComponent implements OnInit {
  singleControlMatcher = new SingleControlErrorStateMatcher();
  mainForm: FormGroup;
  disableSubmitButton = false;
  id?: number;
  city?: City;

  constructor(
    fb: FormBuilder,
    private cityService: CityService,
    private toastService: ToastService,
    private route: ActivatedRoute,
    private router: Router) {
    this.mainForm = fb.group({
      name: fb.control('', [Validators.required, Validators.maxLength(127)]),
    })
  }

  ngOnInit(): void {
    let tempId = this.route.snapshot.paramMap.get('id');
    if (tempId != null) {
      let parsedId = parseInt(tempId);
      if (!isNaN(parsedId)) {
        this.id = parsedId;
        this.cityService
          .getCity(this.id)
          .subscribe((data) => this.city = data);
      }
    }
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
        this.disableSubmitButton = false;

        this.toastService.showSuccess("Pomyślnie zaktualizowano miasto!", 2000)
        this.router.navigate(['city-admin-main-page']);
      },
      error: (e) => {
        this.disableSubmitButton = false;

        this.toastService.showDanger("Błąd podczas aktualizacji miasta: " + e.message);
      }
    });
  }
}

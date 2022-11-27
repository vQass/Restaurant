import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { catchError, map, merge, of as observableOf, startWith, switchMap } from 'rxjs';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';

@Component({
  selector: 'app-meal-main-page',
  templateUrl: './meal-main-page.component.html',
  styleUrls: ['./meal-main-page.component.scss']
})
export class MealMainPageComponent {
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  meals: MealAdminPanelItem[] = [];

  disableDeleteButton = false;
  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'price', 'mealCategoryName', 'available', 'actions'];

  constructor(
    private mealService: MealService,
    private toastService: ToastService,
    private router: Router) {
  }

  ngAfterViewInit(): void {

    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.mealService.getMealsForAdminPanel(
            this.paginator.pageIndex,
            this.paginator.pageSize
          ).pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          this.isLoadingResults = false;

          if (data === null) {
            return [];
          }

          this.resultsLength = data.itemCount;

          return data.items;
        }),
      )
      .subscribe(data => (this.meals = data
      ));
  }

  delete(id: number) {
    // TODO
  }

  gotoItems(meal: MealAdminPanelItem) {
    this.router.navigate(['/edit-meal-options-admin-page',
      {
        id: meal.id,
      }]);
  }
}

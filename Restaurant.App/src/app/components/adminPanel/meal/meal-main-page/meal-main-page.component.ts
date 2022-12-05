import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, merge, of as observableOf, startWith, switchMap } from 'rxjs';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { MealAdminPanelItem } from 'src/models/meal/MealAdminPanelItem';

@Component({
  selector: 'app-meal-main-page',
  templateUrl: './meal-main-page.component.html',
  styleUrls: ['./meal-main-page.component.scss']
})
export class MealMainPageComponent extends PagingHelper {
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  meals: MealAdminPanelItem[] = [];

  disableActivityButton = false;
  disableDeleteButton = false;
  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'price', 'mealCategoryName', 'available', 'actions'];

  constructor(
    private mealService: MealService,
    private toastService: ToastService,
    router: Router,
    route: ActivatedRoute) {
    super(route, router, 'meal-admin-main-page')
  }

  ngAfterViewInit(): void {
    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.mealService.getPage(
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
    this.disableDeleteButton = true;

    this.mealService.delete(id).subscribe({
      next: () => {
        this.disableDeleteButton = false;

        this.toastService.showSuccess("Pomyślnie usunięto danie!", 2000);
        this.refreshData();
      },
      error: (e) => {
        this.disableDeleteButton = false;

        this.toastService.showDanger("Błąd podczas usuwania dania: " + e.message);
      }
    });
  }

  setAsAvailable(meal: MealAdminPanelItem) {
    this.disableActivityButton = true;

    this.mealService
      .setAsAvailable(meal.id)
      .subscribe({
        next: () => {
          this.disableActivityButton = false;

          if (meal != null) {
            meal.available = true;
          }

          this.toastService.showSuccess("Pomyślnie zmieniono aktywność dania!", 1000)
        },
        error: (e) => {
          this.disableActivityButton = false;
          this.toastService.showDanger("Błąd podczas zmieniania aktywności dania: \n" + e.message, 3000);
        }
      });
  }

  setAsUnavailable(meal: MealAdminPanelItem) {
    this.disableActivityButton = true;

    this.mealService
      .setAsUnavailable(meal.id)
      .subscribe({
        next: () => {
          this.disableActivityButton = false;

          if (meal != null) {
            meal.available = false;
          }

          this.toastService.showSuccess("Pomyślnie zmieniono aktywność dania!", 1000)
        },
        error: (e) => {
          this.disableActivityButton = false;
          this.toastService.showDanger("Błąd podczas zmieniania aktywności dania: \n" + e.message, 3000);
        }
      });
  }

  goToAddPage() {
    this.goToPage(
      this.paginator.pageIndex,
      this.paginator.pageSize,
      'add-meal-page');
  }

  goToMealOptionsPage(id: number) {
    this.goToPage(
      this.paginator.pageIndex,
      this.paginator.pageSize,
      '/edit-meal-options-admin-page/' + id);
  }

  refreshData() {
    return this.mealService.getPage(
      this.paginator.pageIndex,
      this.paginator.pageSize
    ).subscribe((data) => {
      if (data.items.length == 0) {
        const pageIndex = this.paginator.pageIndex;
        if (pageIndex == 0) {
          return;
        }
        this.paginator.pageIndex = pageIndex - 1;
        this.refreshData();
      }
      this.resultsLength = data.itemCount;
      this.meals = data.items;
    });
  }


}

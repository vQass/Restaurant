import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { catchError, map, merge, of as observableOf, startWith, switchMap } from 'rxjs';
import { MealCategoryService } from 'src/app/services/ApiServices/meal-category.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { MealCategory } from 'src/models/mealCategory/MealCategory';

@Component({
  selector: 'app-meal-category-main-page',
  templateUrl: './meal-category-main-page.component.html',
  styleUrls: ['./meal-category-main-page.component.scss']
})
export class MealCategoryMainPageComponent {
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  categories: MealCategory[] = [];
  disableDeleteButton = false;
  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'actions'];

  constructor(private mealCategoryService: MealCategoryService, private router: Router, private toastService: ToastService) {
  }

  ngAfterViewInit(): void {
    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.mealCategoryService!.getMealCategoriesPage(
            this.paginator.pageIndex,
            this.paginator.pageSize,
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
      .subscribe(data => (this.categories = data));
  }


  delete(id: number) {
    this.disableDeleteButton = true;

    this.mealCategoryService.delete(id).subscribe({
      next: () => {
        this.disableDeleteButton = false;

        this.toastService.showSuccess("Pomyślnie usunięto kategorię dań!", 2000);
        this.refreshData();
      },
      error: (e) => {
        this.disableDeleteButton = false;

        this.toastService.showDanger("Błąd podczas kategorii dań: " + e.message);
      }
    });
  }

  gotoItems(mealCategoryId: number) {
    this.router.navigate(['/edit-meal-category-page',
      {
        id: mealCategoryId,
      }]);
  }

  refreshData() {
    return this.mealCategoryService.getMealCategoriesPage(
      this.paginator.pageIndex,
      this.paginator.pageSize
    ).subscribe((data) => {
      this.resultsLength = data.itemCount;
      this.categories = data.items;
    });
  }
}

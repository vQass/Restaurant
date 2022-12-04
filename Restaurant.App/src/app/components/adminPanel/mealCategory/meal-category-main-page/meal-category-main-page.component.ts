import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, merge, of as observableOf, startWith, switchMap } from 'rxjs';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { MealCategoryService } from 'src/app/services/ApiServices/meal-category.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { MealCategory } from 'src/models/mealCategory/MealCategory';

@Component({
  selector: 'app-meal-category-main-page',
  templateUrl: './meal-category-main-page.component.html',
  styleUrls: ['./meal-category-main-page.component.scss']
})
export class MealCategoryMainPageComponent extends PagingHelper {
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  categories: MealCategory[] = [];
  disableDeleteButton = false;
  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'actions'];

  constructor(
    private mealCategoryService: MealCategoryService,
    private toastService: ToastService,
    router: Router,
    route: ActivatedRoute,) {
    super(route, router, 'meal-category-main-page')
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

  goToEditPage(id: number) {
    this.goToPage(
      this.paginator.pageIndex,
      this.paginator.pageSize,
      '/edit-meal-category-page/' + id)
  }

  goToAddPage() {
    this.goToPage(
      this.paginator.pageIndex,
      this.paginator.pageSize,
      '/add-meal-category-page')
  }

  refreshData() {
    return this.mealCategoryService.getMealCategoriesPage(
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
      else {
        this.resultsLength = data.itemCount;
        this.categories = data.items;
      }
    });
  }
}

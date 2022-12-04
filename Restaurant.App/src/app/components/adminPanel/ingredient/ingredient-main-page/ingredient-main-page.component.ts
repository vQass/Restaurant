import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, merge, of as observableOf, startWith, switchMap } from 'rxjs';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { IngredientService } from 'src/app/services/ApiServices/ingredient.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { Ingredient } from 'src/models/ingredient/Ingredient';

@Component({
  selector: 'app-ingredient-main-page',
  templateUrl: './ingredient-main-page.component.html',
  styleUrls: ['./ingredient-main-page.component.scss']
})
export class IngredientMainPageComponent extends PagingHelper {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<Ingredient>;

  ingredients: Ingredient[] = [];

  disableDeleteButton = false;
  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'actions'];

  constructor(
    private ingredientService: IngredientService,
    private toastService: ToastService,
    router: Router,
    route: ActivatedRoute) {
    super(route, router, 'ingredient-admin-main-page')
  }

  ngAfterViewInit(): void {
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.ingredientService.getPage(
            this.paginator.pageIndex,
            this.paginator.pageSize
          ).pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          this.isLoadingResults = false;

          if (data === null) {
            return [];
          }

          this.resultsLength = data.itemsCount;
          return data.items;
        }),
      )
      .subscribe(data => (this.ingredients = data
      ));
  }

  delete(id: number) {
    this.disableDeleteButton = true;

    this.ingredientService.delete(id).subscribe({
      next: () => {
        this.disableDeleteButton = false;
        this.refreshData();
        this.toastService.showSuccess("Pomyślnie usunięto składnik!", 2000)
      },
      error: (e) => {
        this.disableDeleteButton = false;

        this.toastService.showDanger("Błąd podczas usuwania składnika: " + e.message);
      }
    });
  }

  refreshData() {
    return this.ingredientService.getPage(
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
      this.resultsLength = data.itemsCount;
      this.ingredients = data.items;
    });
  }

  goToEditPage(id: number) {
    this.goToPage(
      this.paginator.pageIndex,
      this.paginator.pageSize,
      'edit-ingredient-page/' + id
    );
  }

  goToAddPage() {
    this.goToPage(
      this.paginator.pageIndex,
      this.paginator.pageSize,
      'add-ingredient-page'
    );
  }
}

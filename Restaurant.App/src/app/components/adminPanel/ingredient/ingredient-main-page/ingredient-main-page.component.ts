import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { catchError, map, merge, of as observableOf, startWith, switchMap } from 'rxjs';
import { IngredientService } from 'src/app/services/ApiServices/ingredient.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { IngredientAdminPanelItem } from 'src/models/ingredient/IngredientAdminPanelItem';

@Component({
  selector: 'app-ingredient-main-page',
  templateUrl: './ingredient-main-page.component.html',
  styleUrls: ['./ingredient-main-page.component.scss']
})
export class IngredientMainPageComponent {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<IngredientAdminPanelItem>;

  ingredients: IngredientAdminPanelItem[] = [];

  disableDeleteButton = false;
  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'actions'];

  constructor(private ingredientService: IngredientService, private toastService: ToastService) {
  }

  ngAfterViewInit(): void {

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.ingredientService.getIngredientsForAdminPanel(
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

    this.ingredientService.deleteIngredient(id).subscribe({
      next: () => {
        this.disableDeleteButton = false;

        this.toastService.showSuccess("Pomyślnie usunięto składnik!", 2000)


      },
      error: (e) => {
        this.disableDeleteButton = false;

        this.toastService.showDanger("Błąd podczas usuwania składnika: " + e.message);
      }
    });
  }

  refreshData() {
    return this.ingredientService.getIngredientsForAdminPanel(
      this.paginator.pageIndex,
      this.paginator.pageSize
    ).subscribe((data) => {
      this.resultsLength = data.itemsCount;
      this.ingredients = data.items;
    });
  }
}

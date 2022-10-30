import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
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
  @ViewChild(MatSort) sort!: MatSort;

  meals: MealAdminPanelItem[] = [];

  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'price', 'mealCategoryName', 'available'];

  constructor(private mealService: MealService, private toastService: ToastService) {
  }

  ngAfterViewInit(): void {

    // this.orderService
    //   .getOrderStatuses()
    //   .subscribe((data) => { this.orderStatuses = data, console.log(this.orderStatuses) });

    merge(this.sort.sortChange, this.paginator.page)
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
          console.log(data);

          this.resultsLength = data.itemsCount;
          return data.items;
        }),
      )
      .subscribe(data => (this.meals = data
      ));
  }


  // changeOrderStatus(orderId: number, statusId: number) {
  //   this.orderService.changeOrderStatus(orderId, statusId).subscribe({
  //     next: (resp) => {

  //       if (this.orders?.some(x => x.id == orderId)) {
  //         var item = this.orders.filter(x => x.id == orderId)[0];

  //         item.status = this.orderStatuses.filter(x => x.id == statusId)[0].description;
  //       }

  //       this.toastService.showSuccess("Pomyślnie zmieniono status!", 1000)
  //     },
  //     error: (e) => {
  //       this.toastService.showDanger("Błąd zmiany statusu: " + e.message, 3000);
  //     }
  //   });
  // }

}

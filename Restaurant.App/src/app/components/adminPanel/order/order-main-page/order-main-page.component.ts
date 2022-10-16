import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { catchError, map, merge, of as observableOf, startWith, switchMap } from 'rxjs';
import { OrderService } from 'src/app/services/ApiServices/order.service';
import { OrderAdminPanelItem } from 'src/models/order/OrderAdminPanelItem';

@Component({
  selector: 'app-order-main-page',
  templateUrl: './order-main-page.component.html',
  styleUrls: ['./order-main-page.component.scss']
})
export class OrderMainPageComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  orders: OrderAdminPanelItem[] = [];

  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'surname'];

  constructor(private orderService: OrderService) {
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.orderService!.getOrdersForAdminPanel(
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
      .subscribe(data => (this.orders = data));
  }
}

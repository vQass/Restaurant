import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { catchError, map, merge, of as observableOf, startWith, switchMap } from 'rxjs';
import { OrderService } from 'src/app/services/ApiServices/order.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { OrderAdminPanelElement } from 'src/models/order/OrderAdminPanelElement';
import { OrderAdminPanelItem } from 'src/models/order/OrderAdminPanelItem';
import { OrderStatus } from 'src/models/order/OrderStatus';

@Component({
  selector: 'app-order-main-page',
  templateUrl: './order-main-page.component.html',
  styleUrls: ['./order-main-page.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class OrderMainPageComponent {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  orders: OrderAdminPanelItem[] = [];
  orderStatuses: OrderStatus[] = [];

  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'email', 'orderDate', 'status']; //,
  displayedColumnsWithExpand = [...this.displayedColumns, 'expand'];
  expandedElement?: OrderAdminPanelItem | null;

  constructor(private orderService: OrderService, private toastService: ToastService) {
  }

  ngAfterViewInit(): void {

    this.orderService
      .getOrderStatuses()
      .subscribe((data) => { this.orderStatuses = data });

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
      .subscribe(data => (this.orders = data
      ));
  }

  orderSum(order: OrderAdminPanelElement[]): number {
    return order.reduce((partialSum, orderElement) => partialSum + orderElement.amount * orderElement.price, 0);
  }

  changeOrderStatus(orderId: number, statusId: number) {
    this.orderService.changeOrderStatus(orderId, statusId).subscribe({
      next: (resp) => {

        if (this.orders?.some(x => x.id == orderId)) {
          var item = this.orders.filter(x => x.id == orderId)[0];

          item.status = this.orderStatuses.filter(x => x.id == statusId)[0].description;
        }

        this.toastService.showSuccess("Pomyślnie zmieniono status!", 1000)
      },
      error: (e) => {
        this.toastService.showDanger("Błąd zmiany statusu: " + e.message, 3000);
      }
    });
  }

}

import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { catchError, map, merge, Observable, of as observableOf, shareReplay, startWith, switchMap } from 'rxjs';
import { OrderService } from 'src/app/services/ApiServices/order.service';
import { UserService } from 'src/app/services/ApiServices/user.service';
import { OrderHistoryItem } from 'src/models/order/OrderHistoryItem';

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.scss']
})
export class OrderHistoryComponent {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  isMobileView$: Observable<boolean> = this.breakpointObserver.observe([Breakpoints.XSmall])
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  ordersHistory?: OrderHistoryItem[];
  resultsLength = 0;
  constructor(private orderService: OrderService, private userService: UserService, private breakpointObserver: BreakpointObserver) {
  }

  ngAfterViewInit(): void {

    let userId = this.userService.getId()
    if (userId == 0) { return; }

    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          //this.isLoadingResults = true;
          return this.orderService.getOrderHistory
            (this.paginator.pageIndex,
              this.paginator.pageSize,
              userId)
            .pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          //this.isLoadingResults = false;

          if (data === null) {
            return [];
          }
          this.resultsLength = data.itemCount;

          return data.items;
        }),
      )
      .subscribe(data => (this.ordersHistory = data
      ));
  }

  cartSum(order: OrderHistoryItem): number {
    return order.orderElements.reduce((partialSum, orderElement) => partialSum + orderElement.amount * orderElement.price, 0);
  }
}

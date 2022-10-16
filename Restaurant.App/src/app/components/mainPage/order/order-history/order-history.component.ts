import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { map, Observable, shareReplay } from 'rxjs';
import { OrderService } from 'src/app/services/ApiServices/order.service';
import { UserService } from 'src/app/services/ApiServices/user.service';
import { OrderHistoryItem } from 'src/models/order/OrderHistoryItem';

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.scss']
})
export class OrderHistoryComponent implements OnInit {
  isMobileView$: Observable<boolean> = this.breakpointObserver.observe([Breakpoints.XSmall])
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  ordersHistory!: OrderHistoryItem[];

  constructor(private orderService: OrderService, private userService: UserService, private breakpointObserver: BreakpointObserver) {
    this.orderService.getOrderHistory(this.userService.getId()).subscribe(data => { this.ordersHistory = data });
  }

  ngOnInit(): void {
  }

  cartSum(order: OrderHistoryItem): number {
    return order.orderElements.reduce((partialSum, orderElement) => partialSum + orderElement.amount * orderElement.price, 0);
  }
}

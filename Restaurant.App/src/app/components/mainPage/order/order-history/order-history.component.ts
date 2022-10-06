import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/ApiServices/order.service';
import { UserService } from 'src/app/services/ApiServices/user.service';
import { OrderHistoryItem } from 'src/models/order/OrderHistoryItem';

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.scss']
})
export class OrderHistoryComponent implements OnInit {

  ordersHistory!: OrderHistoryItem[];

  constructor(private orderService: OrderService, private userService: UserService) {
    this.orderService.getOrderHistory(this.userService.getId()).subscribe(data => { this.ordersHistory = data; console.log(this.ordersHistory); });

  }

  ngOnInit(): void {

  }

}

import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/OtherServices/cart.service';
import { CartItem } from 'src/models/cart/CartItem';

@Component({
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.scss']
})
export class OrderSummaryComponent implements OnInit {

  cart: CartItem[];

  constructor(private cartService: CartService) {
    this.cart = this.cartService.cartItems;
  }


  ngOnInit(): void {
  }

}

import { Component, Input } from '@angular/core';
import { CartService } from 'src/app/services/OtherServices/cart.service';
import { CartItem } from 'src/models/cart/CartItem';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.scss']
})
export class CartItemComponent {
  @Input() cartItem!: CartItem;

  constructor(private cartService: CartService) { }

  decrementCartItem() {
    this.cartService.decrementCartItem(this.cartItem);
  }

  removeCartItem() {
    this.cartService.removeCartItem(this.cartItem);
  }
}

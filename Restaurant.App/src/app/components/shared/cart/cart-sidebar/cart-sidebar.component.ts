import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component } from '@angular/core';
import { map, shareReplay } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { CartService } from 'src/app/services/OtherServices/cart.service';
import { CartItem } from 'src/models/cart/CartItem';

@Component({
  selector: 'app-cart-sidebar',
  templateUrl: './cart-sidebar.component.html',
  styleUrls: ['./cart-sidebar.component.scss']
})
export class CartSidebarComponent {
  isMobileView$: Observable<boolean> = this.breakpointObserver.observe([Breakpoints.XSmall, Breakpoints.Small])
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  cart: CartItem[] = [];

  constructor(private cartService: CartService, private breakpointObserver: BreakpointObserver) {
    this.cart = this.cartService.cartItems;
  }

  cartSum(): number {
    return this.cart.reduce((partialSum, cartItem) => partialSum + cartItem.amount * cartItem.singleMealPrice, 0);
  }

}

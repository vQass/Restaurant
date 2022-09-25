import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ValidationConsts } from 'src/app/Validation/ValidationConsts';
import { CartItem } from 'src/models/cart/CartItem';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  public cartItems: CartItem[];
  public observableCartItems: Observable<CartItem[]> = new Observable<CartItem[]>;
  maxMealCount = ValidationConsts.MAX_MEAL_COUNT_IN_ORDER;

  constructor() {
    this.cartItems = [];
  }
  // todo how to use next like in user service to an array
  // nvm, try to use observer pattern to notify cart component about changes
  public addToCart(cartItem: CartItem) {
    if (this.cartItems?.some(x => x.mealId == cartItem.mealId)) {
      var item = this.cartItems.filter(x => x.mealId == cartItem.mealId)[0];
      item.amount += cartItem.amount;
      if (item.amount > this.maxMealCount) {
        item.amount = this.maxMealCount;
      }
    }
    else {
      this.cartItems?.push(cartItem);
    }

    this.observableCartItems = of(this.cartItems)

    console.log(this.cartItems);
  }

  public getCart(): Observable<CartItem[]> {
    return this.observableCartItems;
  }

  public decrementCartItem(cartItem: CartItem) {
    var item = this.cartItems.filter(x => x.mealId == cartItem.mealId)[0];
    item.amount--;
    if (item.amount == 0) {
      this.removeCartItem(cartItem)
    }
  }

  public removeCartItem(cartItem: CartItem) {
    const index: number = this.cartItems.indexOf(cartItem);
    if (index !== -1) {
      this.cartItems.splice(index, 1);
    }
  }
}

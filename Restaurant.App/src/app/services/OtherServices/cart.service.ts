import { Injectable } from '@angular/core';
import { ValidationConsts } from 'src/app/Validation/ValidationConsts';
import { CartItem } from 'src/models/cart/CartItem';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  cartItems: CartItem[];
  maxMealCount = ValidationConsts.MAX_MEAL_COUNT_IN_ORDER;

  constructor() {
    this.cartItems = [];
  }

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
    console.log(this.cartItems);
  }
}

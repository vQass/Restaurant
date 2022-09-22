import { Component, Input, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/OtherServices/cart.service';
import { ValidationConsts } from 'src/app/Validation/ValidationConsts';
import { CartItem } from 'src/models/cart/CartItem';
import { MealViewModel } from 'src/models/meal/MealViewModel';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.scss']
})
export class OrderItemComponent implements OnInit {
  @Input() meal!: MealViewModel;

  maxMealCount = ValidationConsts.MAX_MEAL_COUNT_IN_ORDER;
  amount = 0;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
  }

  add() {
    this.amount++;
    console.log(this.amount, this.meal);
  }

  remove() {
    this.amount--;
    console.log(this.amount, this.meal);
  }

  addToCart() {
    if (this.amount <= 0) {
      return;
    }

    const cartItem: CartItem = {
      amount: this.amount,
      mealId: this.meal.id,
      mealName: this.meal.name,
      singleMealPrice: this.meal.price
    };
    this.cartService.addToCart(cartItem);
    this.amount = 0;
  }
}

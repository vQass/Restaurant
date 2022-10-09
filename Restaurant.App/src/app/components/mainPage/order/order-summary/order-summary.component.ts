import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CityService } from 'src/app/services/ApiServices/city.service';
import { OrderService } from 'src/app/services/ApiServices/order.service';
import { UserService } from 'src/app/services/ApiServices/user.service';
import { CartService } from 'src/app/services/OtherServices/cart.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { SingleControlErrorStateMatcher } from 'src/app/Validation/ErrorStateMatchers';
import { CartItem } from 'src/models/cart/CartItem';
import { City } from 'src/models/city/City';
import { OrderAddRequest } from 'src/models/order/AddOrderRequest';
import { OrderElement } from 'src/models/order/OrderElement';

@Component({
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.scss']
})
export class OrderSummaryComponent implements OnInit {

  disableSubmitButton = false;

  cities!: City[];

  cart: CartItem[];

  singleControlMatcher = new SingleControlErrorStateMatcher();

  orderForm: FormGroup;

  constructor(
    private cartService: CartService,
    fb: FormBuilder,
    private userService: UserService,
    private cityService: CityService,
    private orderService: OrderService,
    private toastService: ToastService,
    private router: Router) {
    this.cart = this.cartService.cartItems;

    if (this.cart.length == 0) {
      this.router.navigate(['order']);
    }

    this.orderForm = fb.group({
      name: fb.control('', [Validators.required]),
      surname: fb.control('', [Validators.required]),
      city: fb.control('', [Validators.required]),
      address: fb.control('', [Validators.required]),
      phoneNumber: fb.control('', [Validators.required])
    })
  }

  ngOnInit(): void {
    this.cityService.getCitiesWithSpecifiedActivity(true).subscribe((val) => this.cities = val.items);
  };

  get name() {
    return this.orderForm.get('name');
  }

  get surname() {
    return this.orderForm.get('surname');
  }

  get address() {
    return this.orderForm.get('address');
  }

  get phoneNumber() {
    return this.orderForm.get('phoneNumber');
  }

  get city() {
    return this.orderForm.get('city');
  }

  cartSum(): number {
    return this.cart.reduce((partialSum, cartItem) => partialSum + cartItem.amount * cartItem.singleMealPrice, 0);
  }

  onSubmit() {
    this.disableSubmitButton = true;

    let order = {
      userId: this.userService.getId(),
      cityId: this.orderForm.value.city,
      name: this.orderForm.value.name,
      surname: this.orderForm.value.surname,
      address: this.orderForm.value.address,
      phoneNumber: this.orderForm.value.phoneNumber,
      orderElements: this.cartService.cartItems.map(item => this.cartToOrder(item))
    } as OrderAddRequest;
    this.orderService.addOrder(order).subscribe({
      next: (resp) => {
        console.log(resp)
        this.toastService.showSuccess("Pomyślnie złożono zamówienie!")
        this.disableSubmitButton = false;
        this.cartService.cartItems = [];
        this.router.navigate(['home']);
      },
      error: (e) => {
        this.toastService.showDanger("Błąd podczas składania zamówienia!");
        this.disableSubmitButton = false;
      }
    })
  }

  cartToOrder(cartItem: CartItem): OrderElement {
    let orderElement = {
      amount: cartItem.amount,
      mealId: cartItem.mealId
    } as OrderElement

    return orderElement;
  }
}

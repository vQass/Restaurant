import { Component, Input, OnInit } from '@angular/core';
import { MealViewModel } from 'src/models/meal/MealViewModel';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.scss']
})
export class OrderItemComponent implements OnInit {
  @Input() meal!: MealViewModel;

  panelOpenState = false;
  constructor() { }

  ngOnInit(): void {
  }
}

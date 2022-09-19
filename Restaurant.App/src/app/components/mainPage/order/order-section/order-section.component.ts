import { Component, Input, OnInit } from '@angular/core';
import { MealGroupViewModel } from 'src/models/meal/MealGroupViewModel';

@Component({
  selector: 'app-order-section',
  templateUrl: './order-section.component.html',
  styleUrls: ['./order-section.component.scss']
})
export class OrderSectionComponent implements OnInit {
  @Input() mealGroup!: MealGroupViewModel;

  constructor() { }

  ngOnInit(): void {
  }
}

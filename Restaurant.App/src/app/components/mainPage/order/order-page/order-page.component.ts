import { Component, OnInit } from '@angular/core';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { MealGroupViewModel } from 'src/models/meal/MealGroupViewModel';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css']
})
export class OrderPageComponent implements OnInit {
  mealGroups!: MealGroupViewModel[];

  constructor(private mealService: MealService) { }

  ngOnInit(): void {
    this.mealService.getGroupedMeals().subscribe(data => this.mealGroups = data);
  }
}

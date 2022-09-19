import { Component, OnInit } from '@angular/core';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { MealGroupViewModel } from 'src/models/meal/MealGroupViewModel';

@Component({
  selector: 'app-menu-page',
  templateUrl: './menu-page.component.html',
  styleUrls: ['./menu-page.component.scss']
})
export class MenuPageComponent implements OnInit {
  mealGroups!: MealGroupViewModel[];

  constructor(private mealService: MealService) { }

  ngOnInit(): void {
    this.mealService.getGroupedMeals().subscribe(data => this.mealGroups = data);
  }
}


import { Component, Input, OnInit } from '@angular/core';
import { MealGroupViewModel } from 'src/models/meal/MealGroupViewModel';

@Component({
  selector: 'app-menu-section',
  templateUrl: './menu-section.component.html',
  styleUrls: ['./menu-section.component.scss']
})
export class MenuSectionComponent implements OnInit {
  @Input() mealGroup!: MealGroupViewModel;

  constructor() { }

  ngOnInit(): void {
  }

}

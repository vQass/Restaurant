import { Component, Input, OnInit } from '@angular/core';
import { MealViewModel } from 'src/models/meal/MealViewModel';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.scss']
})
export class MenuItemComponent implements OnInit {
  @Input() meal!: MealViewModel;

  panelOpenState = false;
  constructor() { }

  ngOnInit(): void {
  }

}

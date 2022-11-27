import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { map, Observable, shareReplay } from 'rxjs';
import { MealService } from 'src/app/services/ApiServices/meal.service';
import { MealGroupViewModel } from 'src/models/meal/MealGroupViewModel';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.scss']
})
export class OrderPageComponent implements OnInit {
  mealGroups?: MealGroupViewModel[];

  isMobileView$: Observable<boolean> = this.breakpointObserver.observe([Breakpoints.XSmall, Breakpoints.Small])
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private mealService: MealService, private breakpointObserver: BreakpointObserver) { }

  ngOnInit(): void {
    this.mealService.getGroupedMeals().subscribe(data => this.mealGroups = data);
  }
}

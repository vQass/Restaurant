import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { CityService } from 'src/app/services/ApiServices/city.service';
import { City } from 'src/models/city/City';

@Component({
  selector: 'app-city-main-page',
  templateUrl: './city-main-page.component.html',
  styleUrls: ['./city-main-page.component.scss']
})
export class CityMainPageComponent {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  cities: City[] = [];

  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'isActive', 'action'];

  constructor(private cityService: CityService) {
  }

  ngAfterViewInit(): void {
    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.cityService!.getCities(
            this.paginator.pageIndex,
            this.paginator.pageSize,
          ).pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;

          if (data === null) {
            return [];
          }

          this.resultsLength = data.itemsCount;
          return data.items;
        }),
      )
      .subscribe(data => (this.cities = data));
  }
}

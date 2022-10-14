import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { catchError, map, merge, startWith, switchMap } from 'rxjs';
import { OrderService } from 'src/app/services/ApiServices/order.service';
import { OrderAdminPanelWrapper } from 'src/models/order/OrderAdminPanelWrapper';

@Component({
  selector: 'app-order-main-page',
  templateUrl: './order-main-page.component.html',
  styleUrls: ['./order-main-page.component.scss']
})
export class OrderMainPageComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  cities: OrderAdminPanelWrapper[] = [];

  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'isActive', 'action'];

  constructor(private orderService: OrderService) {
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  ngAfterViewInit(): void {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.cityService!.getCities(
            // TODO paging
            // this.sort.active,
            // this.sort.direction,
            // this.paginator.pageIndex,
          ).pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          console.log(data);

          if (data === null) {
            return [];
          }

          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.
          this.resultsLength = data.itemsCount;
          return data.items;
        }),
      )
      .subscribe(data => (this.cities = data));
  }
}
function observableOf(arg0: null): any {
  throw new Error('Function not implemented.');
}


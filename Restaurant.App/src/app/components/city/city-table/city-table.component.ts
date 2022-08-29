import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, SortDirection } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import {merge, Observable, of as observableOf} from 'rxjs';
import {catchError, map, startWith, switchMap} from 'rxjs/operators';
import { CityService } from 'src/app/services/ApiServices/city.service';
import { City } from 'src/models/city/City';
import { CityWrapper } from 'src/models/city/CityWrapper';
import { CityTableDataSource, CityTableItem } from './city-table-datasource';

@Component({
  selector: 'app-city-table',
  templateUrl: './city-table.component.html',
  styleUrls: ['./city-table.component.scss']
})

export class CityTableComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  
  @ViewChild(MatTable) table!: MatTable<CityTableItem>;
  dataSource!: CityTableDataSource;
  
  cities: City[] = [];
  
  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name'];

  constructor(private cityService : CityService) {
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
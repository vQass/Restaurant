import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { merge, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { PagingHelper } from 'src/app/abstractClasses/pagingHelper';
import { CityService } from 'src/app/services/ApiServices/city.service';
import { ToastService } from 'src/app/services/OtherServices/toast.service';
import { City } from 'src/models/city/City';

@Component({
  selector: 'app-city-main-page',
  templateUrl: './city-main-page.component.html',
  styleUrls: ['./city-main-page.component.scss']
})
export class CityMainPageComponent extends PagingHelper {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  resultsLength = 0;
  isLoadingResults = true;
  displayedColumns = ['id', 'name', 'isActive', 'actions'];

  disableDeleteButton = false;
  disableDisableCityButton = false;
  disableEnableCityButton = false;

  cities: City[] = [];

  constructor(private cityService: CityService,
    router: Router,
    route: ActivatedRoute,
    private toastService: ToastService) {
    super(route, router, 'city-admin-main-page');
  }

  ngAfterViewInit(): void {
    this.paginator._intl.itemsPerPageLabel = "Elementów na stronę: ";

    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.cityService!.getCityPage(
            this.paginator.pageIndex,
            this.paginator.pageSize,
          ).pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          this.isLoadingResults = false;

          if (data === null) {
            return [];
          }

          this.resultsLength = data.itemCount;
          return data.items;
        }),
      )
      .subscribe(data => (this.cities = data));
  }

  delete(id: number) {
    this.disableDeleteButton = true;

    this.cityService.delete(id).subscribe({
      next: () => {
        this.disableDeleteButton = false;

        this.toastService.showSuccess("Pomyślnie usunięto miasto!", 2000);
        this.refreshData();
      },
      error: (e) => {
        this.disableDeleteButton = false;

        this.toastService.showDanger("Błąd podczas usuwania miasta: " + e.message);
      }
    });
  }

  enableCity(id: number) {
    this.disableEnableCityButton = true;

    this.cityService.enable(id).subscribe({
      next: () => {
        this.disableEnableCityButton = false;

        this.toastService.showSuccess("Pomyślnie aktywowano miasto!", 2000);
        this.refreshData();
      },
      error: (e) => {
        this.disableEnableCityButton = false;

        this.toastService.showDanger("Błąd podczas aktywacji miasta: " + e.message);
      }
    });
  }

  disableCity(id: number) {
    this.disableDisableCityButton = true;

    this.cityService.disable(id).subscribe({
      next: () => {
        this.disableDisableCityButton = false;

        this.toastService.showSuccess("Pomyślnie dezaktywacji miasto!", 2000);
        this.refreshData();
      },
      error: (e) => {
        this.disableDisableCityButton = false;

        this.toastService.showDanger("Błąd podczas dezaktywacji miasta: " + e.message);
      }
    });
  }

  refreshData() {
    return this.cityService.getCityPage(
      this.paginator.pageIndex,
      this.paginator.pageSize
    ).subscribe((data) => {
      if (data.items.length == 0) {
        const pageIndex = this.paginator.pageIndex;
        if (pageIndex == 0) {
          return;
        }
        this.paginator.pageIndex = pageIndex - 1;
        this.refreshData();
      }
      this.resultsLength = data.itemCount;
      this.cities = data.items;
    });
  }

  goToEditPage(cityId: number) {
    this.goToPage(
      this.paginator.pageIndex,
      this.paginator.pageSize,
      '/edit-city-page/' + cityId);
  }

  goToAddPage() {
    this.goToPage(
      this.paginator.pageIndex,
      this.paginator.pageSize,
      '/add-city-page/');
  }

}

import { ActivatedRoute, Router } from '@angular/router';

export abstract class PagingHelper {
  private pageIndex!: number;
  private pageSize!: number;

  defaultPageIndex = 0;
  defaultPageSize = 5;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router) {
    this.route.queryParams
      .subscribe(params => {
        this.pageIndex = params['pageIndex'] ?? this.defaultPageIndex;
        this.pageSize = params['pageSize'] ?? this.defaultPageSize;
      });
  }

  getPageIndex() {
    return this.pageIndex;
  }

  getPageSize() {
    return this.pageSize;
  }

  goToPage(pageIndex: number, pageSize: number, path: string) {
    this.router.navigate([path],
      {
        queryParams: {
          pageIndex: pageIndex,
          pageSize: pageSize
        }
      });
  }

}

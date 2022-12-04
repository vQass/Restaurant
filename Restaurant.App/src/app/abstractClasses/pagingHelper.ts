import { ActivatedRoute, Router } from '@angular/router';

export abstract class PagingHelper {
  private pageIndex!: number;
  private pageSize!: number;

  defaultPageIndex = 0;
  defaultPageSize = 5;
  pagePath: string;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router, pagePath: string) {
    this.route.queryParams
      .subscribe(params => {
        this.pageIndex = params['pageIndex'] ?? this.defaultPageIndex;
        this.pageSize = params['pageSize'] ?? this.defaultPageSize;
      });
    this.pagePath = pagePath;
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

  goToMainPage() {
    this.goToPage(this.pageIndex, this.pageSize, this.pagePath)
  }

  goToOptionsPage(id: number) {
    this.goToPage(this.pageIndex, this.pageSize, this.pagePath + '/' + id)
  }

}

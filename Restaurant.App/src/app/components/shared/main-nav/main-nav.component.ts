import { BreakpointObserver } from '@angular/cdk/layout';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { UserService } from 'src/app/services/ApiServices/user.service';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.scss']
})
export class MainNavComponent {

  shouldChangeToBurger$: Observable<boolean> = this.breakpointObserver.observe('(max-width: 700px)').pipe(
    map(result => result.matches),
    shareReplay()
  );
  isLoggedIn: boolean = false;
  userRole: string = "";

  constructor(private userService: UserService, private breakpointObserver: BreakpointObserver) {
    userService.getIsLoggedIn().subscribe((value) => this.isLoggedIn = value);
  }

  logout() {
    this.userService.logout();
  }
}

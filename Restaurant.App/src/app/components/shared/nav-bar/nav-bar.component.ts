import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserService } from 'src/app/services/ApiServices/user.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  isLoggedIn: boolean = false;
  userRole: string = "";

  constructor(private userService: UserService, public dialog: MatDialog) {
    userService.getIsLoggedIn().subscribe((value) => this.isLoggedIn = value);
  }

  ngOnInit(): void {
  }

  logout() {
    this.userService.logout();

  }
}

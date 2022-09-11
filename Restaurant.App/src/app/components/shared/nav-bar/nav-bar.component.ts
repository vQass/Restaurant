import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserService } from 'src/app/services/ApiServices/user.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  isLoggedIn: boolean;

  constructor(private userService: UserService, public dialog: MatDialog) 
  {
    this.isLoggedIn = userService.isLoggedIn;
  }

  ngOnInit(): void {
  }

  clickEvent()
  {
    this.isLoggedIn = !this.isLoggedIn;
  }

}

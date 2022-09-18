import { Component, OnInit } from '@angular/core';
import { ToastService } from 'src/app/services/OtherServices/toast.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  constructor(public toastService: ToastService) { }

  showInfo() {
    this.toastService.showInfo('I am a info toast', 10000);
  }

  showSuccess() {
    this.toastService.showSuccess('I am a success toast', 10000);
  }

  showDanger() {
    this.toastService.showDanger('I am a danger toastaaa', 10000);
  }

  ngOnInit(): void {
  }

}

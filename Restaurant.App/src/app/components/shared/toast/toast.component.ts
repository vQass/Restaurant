import { Component } from '@angular/core';
import { ToastService } from 'src/app/services/OtherServices/toast.service';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.scss'],
  host: { 'class': 'toast-container position-fixed p-3', 'style': 'z-index: 1200; top: 60px; right: 60px' }
})
export class ToastComponent {
  constructor(public toastService: ToastService) { }
}

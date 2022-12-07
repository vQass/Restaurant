import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class ToastService {
  constructor(private _snackBar: MatSnackBar) {
  }

  show(text: string, duration: number = 5000, panelClass = 'info-toast') {
    this._snackBar.open(text, 'Zamknij',
      {
        duration: duration,
        horizontalPosition: 'center',
        verticalPosition: 'top',
        panelClass: panelClass
      });
  }

  showInfo(text: string, duration: number = 5000) {
    this.show(text, duration, 'info-toast');
  }

  showSuccess(text: string, duration: number = 10000) {
    this.show(text, duration, 'success-toast');
  }

  showDanger(text: string, duration: number = 10000) {
    this.show(text, duration, 'danger-toast');
  }
}

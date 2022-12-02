import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class ToastService {
  constructor(private _snackBar: MatSnackBar) {
  }

  show(text: string, duration: number = 5000) {
    this._snackBar.open(text, 'Zamknij',
      {
        duration: duration,
        horizontalPosition: 'center',
        verticalPosition: 'top',
      });
  }

  showInfo(text: string, duration: number = 5000) {
    this.show(text, duration);
  }

  showSuccess(text: string, duration: number = 10000) {
    this.show(text, duration);
  }

  showDanger(text: string, duration: number = 10000) {
    this.show(text, duration);
  }
}

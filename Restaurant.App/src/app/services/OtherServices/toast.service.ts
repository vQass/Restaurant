import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ToastService {
  toasts: any[] = [];

  show(text: string, options: any = {}) {
    this.toasts.push({ text, ...options });
  }

  remove(toast: any) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }

  showInfo(text: string, delay: number = 5000) {

    this.show(text, { classname: 'bg-primary text-light', delay: delay });
  }

  showSuccess(text: string, delay: number = 10000) {
    this.show(text, { classname: 'bg-success text-light', delay: delay });
  }

  showDanger(text: string, delay: number = 15000) {
    console.log(text, delay);

    this.show(text, { classname: 'bg-danger text-light', delay: delay });
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }
}

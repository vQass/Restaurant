import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ToastService } from './toast.service';

@Injectable({
  providedIn: 'root'
})
export class IdentifierParserService {

  constructor(private toastService: ToastService) { }

  parseId(route: ActivatedRoute): number | null {
    let tempId = route.snapshot.paramMap.get('id');
    if (tempId == null) {
      this.toastService.showDanger('Błąd podczas pobierania identyfikatora!');
      return null;
    }

    let parsedId = parseInt(tempId);
    if (isNaN(parsedId)) {
      this.toastService.showDanger('Błąd podczas przetwarzania identyfikatora!');
      return null;
    }

    return parsedId;
  }
}

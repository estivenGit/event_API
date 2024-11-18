import { inject, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private readonly _snackBar= inject(MatSnackBar);

  showError(message: string): void {
    this._snackBar.open(message, 'Cerrar', {
      duration: 5000, // 5 segundos
      panelClass: ['snackbar-error'],
    });
  }

  showMessage(message: string): void {
    this._snackBar.open(message, 'Cerrar', {
      duration: 5000, // 5 segundos
      panelClass: ['snackbar-error'],
    });
  }
}

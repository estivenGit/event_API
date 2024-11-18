import { ComponentType } from '@angular/cdk/portal';
import { Injectable,inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Evento} from '../models/evento.model'
import {  Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
private readonly _modal= inject(MatDialog)

private eventoActualizadoSubject = new Subject<void>();
public eventoActualizado$ = this.eventoActualizadoSubject.asObservable();


  openModal<CT,T=Evento>(CompoentRef: ComponentType<CT>, data?: T, edit=false): void {
    const config={data, edit};
    this._modal.open(CompoentRef, {
      width: '80%',
      height: '85%',
      panelClass: 'modal-dialog',
      disableClose: true,
      autoFocus: false,
      data: config
    });

  }

  closeModal():void {
    this._modal.closeAll();
  }

  emitirEventoActualizado(): void {
    this.eventoActualizadoSubject.next();  // Emite el evento
  }
}

import { Component, inject, OnInit,ViewChild } from '@angular/core';
import { HomeService } from '../../services/home.service'; 
import { ModalService } from '../../services/modal.service';
import { ReactiveFormsModule } from '@angular/forms';  
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';  // Para usar ngModel
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder,FormGroup,Validators  } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotificationService } from '../../services/notification.service';
import { EventEmitter, Output } from '@angular/core';
import { ListItemComponent } from '../list-item/list-item.component';

@Component({
  selector: 'app-create-item',
  standalone: true,
  // Necesita los módulos necesarios para las animaciones de Material y el uso de ngModels
  imports: [
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './create-item.component.html',
  styleUrls: ['./create-item.component.sass']
})

export class CreateItemComponent implements OnInit {
  eventForm!: FormGroup;
@Output() eventoActualizado = new EventEmitter<void>();
@ViewChild(ListItemComponent) _listItemComponent!: ListItemComponent;
  private readonly _formBuilder=inject(FormBuilder);
  private readonly _homeService=inject(HomeService);
  private readonly _modalService=inject(ModalService);
  private readonly _matDialog=inject(MAT_DIALOG_DATA);

  evento = {
    Nombre: '',
    Descripcion: '',
    FechaHora: '',
    Ubicacion: '',
    Capacidad: 0
  };

  onSubmit(): void {
    //editar    
    if(this._matDialog.data) {
      this._homeService.EditarEvento(this._matDialog.data.IdEvento, this.eventForm.value).subscribe({
        next: (res) => {
            this._modalService.emitirEventoActualizado();
        },
        error: (error) => {      
          console.log(error);
        }      
      });;
    }else {//crear
      this._homeService.crearEvento(this.eventForm.value).subscribe({
      next: (res) => {
        this._modalService.emitirEventoActualizado();
      },
      error: (error) => {      
        console.log(error);
      }      
    });
    }
    this.cerrarModal();
  }

  cerrarModal():void{
    this._modalService.closeModal();
  }

ngOnInit(): void {
  this.buildForm();
  if (this._matDialog.edit) {
    this.eventForm.patchValue({
      Nombre: this._matDialog.data.Nombre,
      Descripcion: this._matDialog.data.Descripcion,
      FechaHora: this._matDialog.data.FechaHora,
      Ubicacion: this._matDialog.data.Ubicacion,
      Capacidad: this._matDialog.data.Capacidad,
    });

    this.eventForm.get('Nombre')?.disable();
    this.eventForm.get('Descripcion')?.disable();
  } 
}

  private buildForm(): void {
    this.eventForm=this._formBuilder.nonNullable.group({
      Nombre: ['',Validators.required],
      Descripcion: ['',Validators.required],
      FechaHora: ['',Validators.required],
      Ubicacion: ['',Validators.required],
      Capacidad: [0,Validators.required],
    });
  }
  
  actualizarLista(): void {
    this._listItemComponent.actualizarLista(); 
  }
}

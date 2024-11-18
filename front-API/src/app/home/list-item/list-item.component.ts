import { Component, inject } from '@angular/core';
import { HomeService } from '../../services/home.service'; 
import { CommonModule } from '@angular/common';
import { ModalService } from '../../services/modal.service';
import { CreateItemComponent } from '../create-item/create-item.component';

import { MatTableDataSource } from '@angular/material/table'; 
import { MatTableModule } from '@angular/material/table'; 
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-list-item',
  standalone: true,
  imports: [MatTableModule,CommonModule,MatIconModule,MatButtonModule],
  templateUrl: './list-item.component.html',
  styleUrls: ['./list-item.component.sass']
})
export class ListItemComponent {
 private readonly _homeService= inject(HomeService);
 private readonly _modalService =inject(ModalService);

   items: any[] = []; 
   displayedColumns: string[] = ['nombre', 'descripcion', 'fechaHora', 'ubicacion', 'capacidad', 'asistentes','acciones'];
   dataSource: MatTableDataSource<any> = new MatTableDataSource();

  ngOnInit(): void {
     this.obtenerEvents();    
  }

  obtenerEvents():void {
  this._homeService.obtenerEvents().subscribe({
    next: (data) => {
      this.dataSource.data = data; 
    },
    error: (error) => {
      console.error('Error al obtener los elementos', error);
    }
  });
}

   toggleSubscription(element: any): void {
    
    console.log('toggleSubscription'+ element.userAsistente);
    const eventId = element.IdEvento; 
    if (element.userAsistente) {
      // Si está suscrito, realiza un DELETE
      this._homeService.unsubscribeFromEvent(eventId).subscribe({
        next: (response) => {          
          if(response.success){
            element.userAsistente = false;
            element.CantidadAsistentes--;
          }         
        },
        error: (error) => {
          console.error('Error al desuscribirse:', error);
        }
      });
    } else {
      // Si no está suscrito, realiza un POST
      this._homeService.subscribeToEvent(eventId).subscribe({
        next: (response) =>  {
          if(response.success) {
            element.userAsistente = true;
            element.CantidadAsistentes++;
          } 
        },
        error: (error) => {
          console.error('Error al suscribirse:', error);
        }
    });
    }
   }

   editarElemento(elemento: any): void {
      this._modalService.openModal<CreateItemComponent>(CreateItemComponent, elemento, true);
   }

   eliminarElemento(elemento: any): void {
    if (confirm('¿Estás seguro de que quieres eliminar este evento?')) {
      this._homeService.eliminarElemento(elemento.IdEvento).subscribe({
        next: (response) =>  {
          if(response.success) {
            console.log('Elemento eliminado', response);
            this.dataSource.data = this.dataSource.data.filter(item => item.IdEvento !== elemento.IdEvento);
          }
        },
        error: (error) => {
          console.error('no fue posible eliminar el evento:', error);
        }
    });    
   }
  }

    actualizarLista(): void {
     this.obtenerEvents(); // Recarga la lista de eventos
   }
}
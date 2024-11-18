import { Component,OnInit,inject,ViewChild, ElementRef  } from '@angular/core';
import { ListItemComponent } from '../home/list-item/list-item.component';
import { MatButtonModule } from '@angular/material/button'; 
import { CreateItemComponent } from '../home/create-item/create-item.component';
import { ModalService } from '../services/modal.service';
import { BehaviorSubject, delay, take } from 'rxjs';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatButtonModule,ListItemComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.sass'
})
export class HomeComponent implements OnInit { 
  @ViewChild(ListItemComponent) _listItemComponent!: ListItemComponent;

  private readonly _modalService = inject(ModalService);
  onClickNewEvent(): void {
    this._modalService.openModal<CreateItemComponent>(CreateItemComponent);
  }

  ngOnInit(): void {
      // Suscribirse al evento de actualización del modal
      this._modalService.eventoActualizado$.subscribe(() => {
        this.actualizarLista(); // Llamar al método cuando se emita el evento
      });
  }

  actualizarLista(): void {
    this._listItemComponent.actualizarLista();
  }
    
}

import { Injectable ,inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,of  } from 'rxjs';
import { catchError, map } from 'rxjs/operators'; 
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  
  private readonly _http = inject(HttpClient);

  private apiUrl =  `${environment.apiUrl}`;


  obtenerEvents(): Observable<any> {
      return this._http.get<any>(`${this.apiUrl}/Evento`) 
      .pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          return of({ mensaje: 'Error al obtener datos' }); 
        })
      );
    }
  

    subscribeToEvent(idEvento: number): Observable<any> {
      return this._http.post(`${this.apiUrl}/EventoAsistentes/${idEvento}`, { })
      .pipe(
        map(() => {
          return { success: true, mensaje: 'Suscrito al Evento' };
        }),
        catchError(error => {
          console.error('Error Suscribiento al evento:', error);
          return of({success: false, mensaje: 'Error Suscribiento al evento' }); 
        })
      );
    }
  
    unsubscribeFromEvent(idEvento: number): Observable<any> {
      return this._http.delete(`${this.apiUrl}/EventoAsistentes/${idEvento}`).pipe(
        map(() => {
          return { success: true, mensaje: 'Desuscrito al Evento' };
        }),
        catchError(error => {
          console.error('Error desuscribiendo al evento:', error);
          return of({success: false, mensaje: 'Error desuscribiendo al evento' }); 
        })
      );
    }
  
    eliminarElemento(id: number): Observable<any> {
      return this._http.delete(`${this.apiUrl}/Evento/${id}`).pipe(
        map(() => {
          return { success: true, mensaje: 'Evento eliminado correctamente' };
        }),
        catchError(error => {
          console.error('Error eliminando el evento:', error);
          return of({ success: false, mensaje: 'Error eliminando el evento' }); 
        })
      );
    }
    

    crearEvento(evento: any): Observable<any> {
      return this._http.post(`${this.apiUrl}/Evento`, evento).pipe(
        catchError(error => {
          console.error('Error creando el evento:', error);
          return of({ mensaje: 'Error creando el evento' }); 
        })
      );
    }

    EditarEvento(id: number,evento: any): Observable<any> {
      
      const url = `${this.apiUrl}/Evento/${id}`;
      return this._http.put(url, evento)
      .pipe(
        catchError(error => {
          console.error('Error al editar el evento:', error);
          return of({ mensaje: 'Error al editar el evento' }); 
        })
      );
    }

    
    
}

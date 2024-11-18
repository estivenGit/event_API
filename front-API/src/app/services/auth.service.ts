// auth.service.ts
import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

import { IAuthenticateStatus } from "../core/models/authenticate-status.model";
import { jwtDecode } from 'jwt-decode';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly _http = inject(HttpClient);
  
  private apiUrl = `${environment.apiUrl}/login/renovarRefreshToken`; 
  private apiUrl1 = `${environment.apiUrl}/login/authenticate`;  
  private accessToken: string | null = null;
  private refreshToken: string | null = null;
  private tokenAut: string | null = null;
  
  login(username: string, password: string): Observable<any> {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});

    const body = {
      username: username,
      password: password
    };

    return this._http.post<any>(this.apiUrl1, body, { headers }).pipe(
      tap(value => {
        this.tokenAut= value.access_Token;
        const result: IAuthenticateStatus = jwtDecode(value.access_Token);
        this.setToken(value.access_Token);
        this.setRefreshToken(value.refresh_Token);
        //this.authenticationStatus.next(result);
        return result;

        // El refresh token se maneja con cookies automáticamente, si está configurado en el servidor
      })
    );
  }
  
  // Método para solicitar la renovación del token
  renovarToken(): Observable<any> {
    const token = this.getToken("jwt"); 
    const refreshToken = this.getToken("refreshToken");  
    console.log('inicio renovación de token '+refreshToken);
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
      'refresh_Token':  refreshToken
    });
    return this._http.post<any>(this.apiUrl, { refreshToken: this.refreshToken }, { headers }).pipe(
      tap(value => {
        console.log('Renovación exitosa');
        this.tokenAut= value.access_Token;
        const result: IAuthenticateStatus = jwtDecode(value.access_Token);
        this.setToken(value.access_Token);        
        this.setRefreshToken(value.refresh_Token);
        //this.authenticationStatus.next(result);
        return value.refresh_Token;
      })
    );
  }

  setTokens(accessToken: string, refreshToken: string) {
    this.accessToken = accessToken;
    this.refreshToken = refreshToken;
  }

  getAccessToken(): string | null {
    return this.accessToken;
  }

  /**
   * Método que permite establecer el token y almacenarlo en la cache.
   * @param {string} jwt Token
   */
  private setToken(jwt: string) {
    this.setItem('jwt', jwt);
  }

  private setRefreshToken(refreshToken: string) {
    this.setItem('refreshToken', refreshToken);
  }


  protected setItem(key: string, data: object | string) {
    if (typeof data === 'string') {
      localStorage.setItem(key, data);
    } else {
      localStorage.setItem(key, JSON.stringify({data}));
    }
  }

  getToken(ls :string): string {
    return localStorage.getItem(ls) || '';
  }

}

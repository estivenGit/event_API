import { HttpInterceptorFn,HttpHeaders  } from '@angular/common/http';
import { inject,PLATFORM_ID  } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { NotificationService } from '../services/notification.service';
import { Router } from '@angular/router';
import {  throwError } from 'rxjs';
import { switchMap, catchError } from 'rxjs/operators'; 

export const authenticationInterceptor: HttpInterceptorFn = (req, next) => {
  
  const _notificationService = inject(NotificationService);
  const _authService = inject(AuthService);
  const _router = inject(Router);
  const _platformId = inject(PLATFORM_ID);
  const excludedUrls = [/api\/login/]; 
  let authReq = req;  
  let token="";

  if(!excludedUrls.some(url => req.url.match(url))){
    
    token = _authService.getToken("jwt");

    if (token) {
      authReq = req.clone({
          headers: req.headers
          .set('Authorization', 'Bearer ' + token)
          .set('Content-Type', 'application/json')
      });
    }
  }else{
    authReq = req.clone(
        { headers: req.headers, withCredentials: true }
      );  
  }
  return next(authReq).pipe(
    catchError((error) => {
      console.error('error:', error);
      if (error.status === 409) {
        _notificationService.showError(error.error);
      }
      else if (error.status === 401 && error.error === 'Token expired') {
        console.log('renovando token...');
        return _authService.renovarToken().pipe(
          switchMap((rta: string) => {
            const newToken=_authService.getToken("jwt");
            const clonedRequest = req.clone({
              headers: 
                    req.headers.append('Content-Type', 'application/json')
                              .append('Access-Control-Allow-Origin', '*')
                              .append('Authorization', 'Bearer ' + newToken)
            });
            return next(clonedRequest);
          }),
          catchError((err) => {
            _router.navigate(['']);
            return throwError(err);
          })
        );
      }else if(error.status === 401){
        _router.navigate(['']);
        return throwError(error);
      }
      console.log('catchError peticion...');
      console.log(error);
      return throwError(error);
    })
  );
};
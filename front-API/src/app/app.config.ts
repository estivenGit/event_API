import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors,withFetch } from '@angular/common/http';
import { authenticationInterceptor } from './interceptors/authentication.interceptor'

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes), provideClientHydration(),
    provideHttpClient(
      withFetch()
      ,withInterceptors([authenticationInterceptor])
    ), provideAnimationsAsync()
  ]
};
 
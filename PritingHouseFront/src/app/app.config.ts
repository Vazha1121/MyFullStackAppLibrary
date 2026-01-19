import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withHashLocation } from '@angular/router';

import { routes } from './app.routes';
import {
  provideClientHydration,
  withEventReplay,
} from '@angular/platform-browser';
import {
  provideHttpClient,
  withFetch,
  withInterceptors,
} from '@angular/common/http';
import { loaderInterceptor } from './Interceptors/loader.interceptor';
import { deleteConfirmInterceptor } from './Interceptors/delete-confirm.interceptor';
import { errorInterceptor } from './Interceptors/error.interceptor';
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withHashLocation()),
    provideClientHydration(withEventReplay()),
    provideHttpClient(
      withFetch(),
      withInterceptors([
        deleteConfirmInterceptor,
        loaderInterceptor,
        errorInterceptor,
      ]),
    ),
  ],
};

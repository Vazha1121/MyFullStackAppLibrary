import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';
import { ApiService } from '../Services/api.service';

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
  const api = inject(ApiService);
  api.startLoader();

  return next(req).pipe(finalize(() => api.stopLoader()));
};

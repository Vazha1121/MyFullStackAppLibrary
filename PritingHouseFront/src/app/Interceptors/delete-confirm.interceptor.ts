import { HttpInterceptorFn } from '@angular/common/http';
import { EMPTY } from 'rxjs';

export const deleteConfirmInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.method === 'DELETE') {
    const confirmed = window.confirm('ნამდვილდ გსურს წაშლა');

    if (!confirmed) {
      return EMPTY;
    }
  }

  return next(req);
};

import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackBar = inject(MatSnackBar);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let message = 'უცნობი შეცდომა';

      if (error.error instanceof ErrorEvent) {
        message = 'სერვერთან კავშირი ვერ დამყარდა';
      } else {
        switch (error.status) {
          case 0:
            message = 'სერვერთ პასუხსა არ აბრუნებს';
            break;
          case 400:
            message = error.error?.message || 'არასწორი მონაცემები';
            break;
          case 401:
            message = 'საჭიროა ავტორიზაცია';
            router.navigate(['/login']);
            break;
          case 403:
            message = 'წვდომა აკრძალულია';

            break;
          case 404:
            message = 'რესურსი არ იძებნება';
            break;
          case 500:
            message = 'სერვერის შეცდომა';
            break;
        }
      }
      snackBar.open(message, 'დახურვა', {
        duration: 4000,
        horizontalPosition: 'right',
        verticalPosition: 'top',
      });

      return throwError(() => error);
    }),
  );
};

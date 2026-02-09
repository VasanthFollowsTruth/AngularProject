import {
  HttpInterceptorFn,
  HttpErrorResponse
} from '@angular/common/http';

import { inject } from '@angular/core';
import { Router } from '@angular/router';

import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router);

  return next(req).pipe(

    catchError((error: HttpErrorResponse) => {

      let message = 'Something went wrong';

      if (error.status === 401) {

        localStorage.removeItem('token');
        localStorage.removeItem('expiresAt');

        router.navigate(['/login']);

        alert('Session expired. Please login again.');

        return throwError(() => error);
      }

      if (error.error?.message) {
        message = error.error.message;
      }

      else if (error.error?.errors) {

        const firstKey =
          Object.keys(error.error.errors)[0];

        message =
          error.error.errors[firstKey][0];
      }

      else if (error.error?.title) {
        message = error.error.title;
      }

      else if (typeof error.error === 'string') {
        message = error.error;
      }

      alert(message);

      return throwError(() => error);
    })

  );
};

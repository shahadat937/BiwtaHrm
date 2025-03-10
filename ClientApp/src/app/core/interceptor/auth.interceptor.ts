import { Injectable } from '@angular/core';
import {Router} from '@angular/router'
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import {AuthService} from '../service/auth.service'

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private authService: AuthService,
    private route: Router
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error)=> {
        if(error instanceof HttpErrorResponse && error.status == 401 && !request.url.includes('/login')) {
          this.authService.logout();
          this.route.navigate(['/login']);
        }

        throw error;
        
      })
    )
  }
}

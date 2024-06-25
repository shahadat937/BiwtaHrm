import { AuthService } from '../service/auth.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private authenticationService: AuthService,
    private snackBar: MatSnackBar,
    private toastr: ToastrService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        // if (err.status === 401) {
        //   // auto logout if 401 response returned from api
        //   this.authenticationService.logout();
        //   location.reload(true);
        // }
        if (err) {
          switch (err.status) {
            case 400:
              if (err.error.errors) {
            
                const modalStateErrors = [];
                for (const key in err.error.errors) {
                  if (err.error.errors[key]) {
                    modalStateErrors.push(err.error.errors[key])
                  }
                }
                throw modalStateErrors;
              } else if (typeof(err.error) === 'object') {
                // this.snackBar.open(err.error.ErrorMessage, err.error.ErrorType, {
                //   duration: 2000,
                //   verticalPosition: 'bottom',
                //   horizontalPosition: 'right',
                //   panelClass: 'snackbar-danger'
                // });
                this.toastr.warning(`${err.error.ErrorMessage}`, `${err.error.ErrorType}`, {
                  positionClass: 'toast-top-right',
                });
              
              } else {
               
                // this.snackBar.open(err.error.ErrorMessage, err.error.ErrorType, {
                //   duration: 2000,
                //   verticalPosition: 'bottom',
                //   horizontalPosition: 'right',
                //   panelClass: 'snackbar-danger'
                // });
                this.toastr.warning(`${err.error.ErrorMessage}`, `${err.error.ErrorType}`, {
                  positionClass: 'toast-top-right',
                });
              }
              break;
            case 401:
              // this.snackBar.open(err.error.ErrorMessage, err.error.ErrorType, {
              //   duration: 2000,
              //   verticalPosition: 'bottom',
              //   horizontalPosition: 'right',
              //   panelClass: 'snackbar-danger'
              // });
              this.toastr.warning(`${err.error.ErrorMessage}`, `${err.error.ErrorType}`, {
                positionClass: 'toast-top-right',
              });
              break;
            case 404:
              // this.snackBar.open(err.error.ErrorMessage, err.error.ErrorType, {
              //   duration: 2000,
              //   verticalPosition: 'bottom',
              //   horizontalPosition: 'right',
              //   panelClass: 'snackbar-danger'
              // });
              this.toastr.warning(`${err.error.ErrorMessage}`, `${err.error.ErrorType}`, {
                positionClass: 'toast-top-right',
              });
              //this.router.navigateByUrl('/authentication/page404');
              break;
            case 500:
              // const navigationExtras: NavigationExtras = {state: {error: err.error}}
              // this.router.navigateByUrl('/authentication/page500', navigationExtras);
 		// this.snackBar.open('Something unexpected went wrong','', {
    //             duration: 2000,
    //             verticalPosition: 'bottom',
    //             horizontalPosition: 'right',
    //             panelClass: 'snackbar-danger'
    //           });
    this.toastr.warning(`${err.error.ErrorMessage}`, `${err.error.ErrorType}`, {
      positionClass: 'toast-top-right',
    });
              break;
            default:
              // this.snackBar.open('Something unexpected went wrong','', {
              //   duration: 2000,
              //   verticalPosition: 'bottom',
              //   horizontalPosition: 'right',
              //   panelClass: 'snackbar-danger'
              // });
              this.toastr.warning('', `Something unexpected went wrong`, {
                positionClass: 'toast-top-right',
              });
              break;
          }
        }
        const error = err.error.message || err.statusText;
        return throwError(error);
      })
    );
  }
}

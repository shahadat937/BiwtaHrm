import { Injectable } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class AuthGuard   {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authService.currentUserValue) {
      const userRole = this.authService.currentUserValue.role; // Access role using bracket notation
      if (route.data['role'] && route.data['role'].indexOf(userRole) === -1) {
        console.log(route.data['role'])
        console.log(route.data['role'].indexOf(userRole) )
        this.router.navigate(['/login']);
        return false;
      }
      return true;
    } 
    this.router.navigate(['/login']);
    return false;
  }
}

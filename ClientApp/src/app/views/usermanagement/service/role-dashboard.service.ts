import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { RoleDashboard } from '../model/role-dashboard';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleDashboardService {
  
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient,
    private toastr: ToastrService,) {
  }

  
  saveRoleDashboard(model: FormData){
    return this.http.post(this.baseUrl + '/roleDashboard/save-roleDashboard', model);
  }
  
  getRoleDashboardList(): Observable<RoleDashboard[]>{
    return this.http.get<RoleDashboard[]>(this.baseUrl + '/roleDashboard/get-allRoleDashboard');
  }

  getRoleDashboardPermission(roleName: string): Observable<RoleDashboard>{
    return this.http.get<RoleDashboard>(this.baseUrl + '/roleDashboard/get-roleDashboardPermission/' + roleName);
  }
}

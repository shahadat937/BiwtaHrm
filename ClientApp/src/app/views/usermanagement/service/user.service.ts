import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModule } from '../model/user.module';
import { UserRoles } from '../model/user-roles';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';
import { SelectedStringModel } from 'src/app/core/models/selectedStringModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  users: UserModule;
  userRole: UserRoles;
  constructor(private http: HttpClient) {
    this.users = new UserModule();
    this.userRole = new UserRoles();
   }

   
  find(id: string) {
    return this.http.get<UserModule>(this.baseUrl + '/users/get-userById/' + id);
  }

   getAll(): Observable<UserModule[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of (this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<UserModule[]>(this.baseUrl + '/users/get-users')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  getInfoByEmpId(id: number){
    return this.http.get<UserModule>(this.baseUrl + '/users/get-userByEmpId/' + id);
  }
   
  submit(model: any) {
    return this.http.post(`${environment.securityUrl}/account/register`, model);
  }

  update(id: string,model: any){
    return this.http.put(this.baseUrl + '/users/update-user/'+id, model);
  }

  updatePassword(id: string,model: any){
    return this.http.put(this.baseUrl + '/users/update-password/'+id, model);
  }

  getSelectedUserRoles(){
    return this.http.get<SelectedStringModel[]>(this.baseUrl + '/userRole/get-selectedUserRoles/');
  }

  getUserRoleDetails(id: string){
    return this.http.get<UserRoles>(this.baseUrl + '/userRole/get-userRoleDetail/' + id);
  }
  
  updateUserRoles(id: string,model: any){
    return this.http.put(this.baseUrl + '/userRole/update-userRole/'+id, model);
  }

  resetUserPassword(id: string,model: any){
    return this.http.put(this.baseUrl + '/users/reset-password/'+id, model);
  }

}

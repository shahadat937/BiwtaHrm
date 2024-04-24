import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserRole } from '../model/user-role';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserRoleService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  userRoles: UserRole;
  constructor(private http: HttpClient) {
    this.userRoles = new UserRole();
   }
  find(id: number) {
    return this.http.get<UserRole>(this.baseUrl + '/userRole/get-userRoleDetail/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<UserRole[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<UserRole[]>(this.baseUrl + '/userRole/get-userRole')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/userRole/update-userRole/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/userRole/save-userRole', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/userRole/delete-UserRole/'+id);
  }

}
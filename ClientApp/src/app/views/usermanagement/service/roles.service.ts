import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AspNetRoles } from '../model/asp-net-roles';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  aspNetRoles: AspNetRoles;
  constructor(private http: HttpClient) {
    this.aspNetRoles = new AspNetRoles();
   }

   
  find(id: string) {
    return this.http.get<AspNetRoles>(this.baseUrl + '/aspNetRoles/get-roleById/' + id);
  }

   getAll(): Observable<AspNetRoles[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of (this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<AspNetRoles[]>(this.baseUrl + '/aspNetRoles/get-role')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
   
  submit(model: any) {
    return this.http.post(this.baseUrl + '/aspNetRoles/save-role/', model);
  }

  update(id: string,model: any){
    return this.http.put(this.baseUrl + '/aspNetRoles/update-role/'+id, model);
  }
  
  delete(id: number){
    return this.http.delete<AspNetRoles>(this.baseUrl + '/aspNetRoles/delete-role/' + id);
  }

}
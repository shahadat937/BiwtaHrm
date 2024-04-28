import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModule } from '../model/user.module';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  users: UserModule;
  constructor(private http: HttpClient) {
    this.users = new UserModule();
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
   
  submit(model: any) {
    return this.http.post(this.baseUrl + '/users/save-user', model);
  }
  update(id: number,model: any){
    return this.http.put(this.baseUrl + '/account/register', model);
  }
}

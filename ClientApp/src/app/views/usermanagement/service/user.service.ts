import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModule } from '../model/user.module';
import { environment } from 'src/environments/environment';

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
   
  submit(model: any) {
    return this.http.post(this.baseUrl + '/account/register', model);
  }
  update(id: number,model: any){
    return this.http.put(this.baseUrl + '/account/register', model);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NavbarSetting } from '../model/navbar-setting';

@Injectable({
  providedIn: 'root'
})
export class NavbarSettingService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  navbarSetting: NavbarSetting;
  constructor(private http: HttpClient) {
    this.navbarSetting = new NavbarSetting();
   }

   
  find(id: number) {
    return this.http.get<NavbarSetting>(this.baseUrl + '/navbarSetting/get-NavbarSettingDetail/' + id);
  }

  getAll() {
      return this.http.get<NavbarSetting[]>(this.baseUrl + '/navbarSetting/get-NavbarSettings');
  }

  
  getActive() {
    return this.http.get<NavbarSetting>(this.baseUrl + '/navbarSetting/get-ActiveNavbarSettings');
}

  
  submit(model: any) {
    const formData = this.toFormData(model);
    return this.http.post(this.baseUrl + '/navbarSetting/save-NavbarSetting', formData);
  }

  update(id: number,model: any){
    const formData = this.toFormData(model);
    return this.http.put(this.baseUrl + '/navbarSetting/update-NavbarSetting/'+id, formData);
  }
  
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/navbarSetting/delete-NavbarSetting/' + id);
  }
  
  private toFormData(model: any): FormData {
    const formData = new FormData();
    for (const key of Object.keys(model)) {
      if (model[key] instanceof File && model[key]) { // Check for null or undefined
        formData.append(key, model[key], model[key].name);
      } else if (model[key] !== null && model[key] !== undefined) { // Avoid null/undefined fields
        formData.append(key, model[key].toString());
      }
    }
    return formData;
  }
}

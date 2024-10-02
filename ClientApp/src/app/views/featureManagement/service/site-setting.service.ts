import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SiteSetting } from '../model/site-setting';

@Injectable({
  providedIn: 'root'
})
export class SiteSettingService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  siteSetting: SiteSetting;
  constructor(private http: HttpClient) {
    this.siteSetting = new SiteSetting();
   }

   
  find(id: number) {
    return this.http.get<SiteSetting>(this.baseUrl + '/siteSetting/get-SiteSettingDetail/' + id);
  }

  getAll() {
      return this.http.get<SiteSetting[]>(this.baseUrl + '/siteSetting/get-SiteSettings');
  }

  
  getActive() {
    return this.http.get<SiteSetting>(this.baseUrl + '/siteSetting/get-ActiveSiteSettings');
}

  
  submit(model: any) {
    const formData = this.toFormData(model);
    return this.http.post(this.baseUrl + '/siteSetting/save-SiteSetting', formData);
  }

  update(id: number,model: any){
    const formData = this.toFormData(model);
    return this.http.put(this.baseUrl + '/siteSetting/update-SiteSetting/'+id, formData);
  }
  
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/siteSetting/delete-SiteSetting/' + id);
  }
  
  private toFormData(model: any): FormData {
    const formData = new FormData();
    for (const key of Object.keys(model)) {
      if (model[key] instanceof File) {
        formData.append(key, model[key], model[key].name);
      } else {
        formData.append(key, model[key]);
      }
    }
    return formData;
  }

}

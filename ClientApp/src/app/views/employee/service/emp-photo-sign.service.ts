import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpPhotoSignModule } from '../model/emp-photo-sign.module';

@Injectable({
  providedIn: 'root'
})
export class EmpPhotoSignService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  imageUrl = environment.imageUrl;
  empPhotoSign: EmpPhotoSignModule;

  constructor(private http: HttpClient) { 
    this.empPhotoSign = new EmpPhotoSignModule();
  }

  findByEmpId(id: number) {
    return this.http.get<EmpPhotoSignModule>(`${this.baseUrl}/empPhotoSign/get-EmpPhotoSignByEmpId/${id}`);
  }
  
  saveEmpPhotoSignInfo(model: any) {
    const formData = this.toFormData(model);
    return this.http.post(`${this.baseUrl}/empPhotoSign/save-EmpPhotoSigns`, formData);
  }

  updateEmpPhotoSignInfo(id: number, model: any) {
    const formData = this.toFormData(model);
    return this.http.put(`${this.baseUrl}/empPhotoSign/update-EmpPhotoSigns/${id}`, formData);
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

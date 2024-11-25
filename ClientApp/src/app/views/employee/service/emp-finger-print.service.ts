import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpFingerprint } from '../model/emp-fingerprint';

@Injectable({
  providedIn: 'root'
})
export class EmpFingerPrintService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  imageUrl = environment.imageUrl;
  empFingerprint: EmpFingerprint;

  constructor(private http: HttpClient) { 
    this.empFingerprint = new EmpFingerprint();
  }

  findByEmpId(id: number) {
    return this.http.get<EmpFingerprint>(`${this.baseUrl}/empFingerprint/get-EmpFingerprintByEmpId/${id}`);
  }
  
  saveEmpFingerprintInfo(model: any) {
    const formData = this.toFormData(model);
    return this.http.post(`${this.baseUrl}/empFingerprint/save-EmpFingerprints`, formData);
  }

  updateEmpFingerprintInfo(id: number, model: any) {
    const formData = this.toFormData(model);
    return this.http.put(`${this.baseUrl}/empFingerprint/update-EmpFingerprints/${id}`, formData);
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
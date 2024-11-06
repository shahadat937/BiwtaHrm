import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpNomineeInfoModel } from '../model/emp-nominee-info-model';

@Injectable({
  providedIn: 'root'
})
export class EmpNomineeInfoService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empNomineeInfoModel: EmpNomineeInfoModel;

  constructor(private http: HttpClient) { 
    this.empNomineeInfoModel = new EmpNomineeInfoModel();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpNomineeInfoModel[]>(this.baseUrl + '/empNomineeInfo/get-EmpNomineeInfoByEmpId/' + id);
  }

  saveEmpNomineeInfo(model: any) {
    const formData = this.toFormData(model);
    return this.http.post(this.baseUrl + '/empNomineeInfo/save-EmpNomineeInfo', formData);
  }
  deleteEmpNomineeInfo(id: number) {
    return this.http.delete(this.baseUrl + '/empNomineeInfo/delete-EmpNomineeInfo/'+id);
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

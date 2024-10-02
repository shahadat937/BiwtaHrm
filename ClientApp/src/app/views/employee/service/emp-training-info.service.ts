import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { EmpTrainingInfo } from '../model/emp-training-info';

@Injectable({
  providedIn: 'root'
})
export class EmpTrainingInfoService {
  
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empTrainingInfo: EmpTrainingInfo;

  constructor(private http: HttpClient) { 
    this.empTrainingInfo = new EmpTrainingInfo();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpTrainingInfo[]>(this.baseUrl + '/empTrainingInfo/get-EmpTrainingInfoByEmpId/' + id);
  }
  
  save(model: FormData) {
    return this.http.post(this.baseUrl + '/empTrainingInfo/save-EmpTrainingInfo', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/empTrainingInfo/delete-EmpTrainingInfo/'+id);
  }

}
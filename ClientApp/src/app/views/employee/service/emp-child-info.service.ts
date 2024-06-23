import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { EmpChildInfoModule } from '../model/emp-child-info.module';

@Injectable({
  providedIn: 'root'
})
export class EmpChildInfoService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empChildInfoModule: EmpChildInfoModule;

  constructor(private http: HttpClient) { 
    this.empChildInfoModule = new EmpChildInfoModule();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpChildInfoModule[]>(this.baseUrl + '/empChildInfo/get-EmpChildInfoByEmpId/' + id);
  }

  getSelectedChildStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/childStatus/get-selectedChildStatus/');
  }
  
  saveEmpChildInfo(model: FormData) {
    return this.http.post(this.baseUrl + '/empChildInfo/save-EmpChildInfo', model);
  }
  deleteEmpChildInfo(id: number) {
    return this.http.delete(this.baseUrl + '/empChildInfo/delete-EmpChildInfo/'+id);
  }

}
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { EmpEducationInfoModule } from '../model/emp-education-info.module';

@Injectable({
  providedIn: 'root'
})
export class EmpEducationInfoService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empEducationInfoModule: EmpEducationInfoModule;

  constructor(private http: HttpClient) { 
    this.empEducationInfoModule = new EmpEducationInfoModule();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpEducationInfoModule[]>(this.baseUrl + '/empEducationInfo/get-EmpEducationInfoByEmpId/' + id);
  }

  getSelectedExamType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/examType/get-selectedExamType/');
  }
  getSelectedBoard(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/board/get-selectedBoard/');
  }
  getSelectedSubject(id: number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/subGroup/get-selectedsubgroupByExamType/' + id);
  }
  
  getSelectedResult(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Result/get-selectedResults/');
  }
  
  saveEmpEducationInfo(model: FormData) {
    return this.http.post(this.baseUrl + '/empEducationInfo/save-EmpEducationInfo', model);
  }
  deleteEmpEducationInfo(id: number) {
    return this.http.delete(this.baseUrl + '/empEducationInfo/delete-EmpEducationInfo/'+id);
  }

}
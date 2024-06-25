import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { EmpLanguageInfoModule } from '../model/emp-language-info.module';

@Injectable({
  providedIn: 'root'
})
export class EmpLanguageInfoService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empLanguageInfoModule: EmpLanguageInfoModule;

  constructor(private http: HttpClient) { 
    this.empLanguageInfoModule = new EmpLanguageInfoModule();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpLanguageInfoModule[]>(this.baseUrl + '/empLanguageInfo/get-EmpLanguageInfoByEmpId/' + id);
  }

  getSelectedLanguageName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Language/get-selectedlanguage/');
  }
  getSelectedCompetency(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Competence/get-selectedcompetence/');
  }
  
  saveEmpLanguageInfo(model: FormData) {
    return this.http.post(this.baseUrl + '/empLanguageInfo/save-EmpLanguageInfo', model);
  }
  deleteEmpLanguageInfo(id: number) {
    return this.http.delete(this.baseUrl + '/empLanguageInfo/delete-EmpLanguageInfo/'+id);
  }

}
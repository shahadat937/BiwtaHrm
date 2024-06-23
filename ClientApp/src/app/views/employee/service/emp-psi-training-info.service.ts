import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { EmpPsiTrainingInfoModule } from '../model/emp-psi-training-info.module';

@Injectable({
  providedIn: 'root'
})
export class EmpPsiTrainingInfoService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empPsiTrainingInfoModule: EmpPsiTrainingInfoModule;

  constructor(private http: HttpClient) { 
    this.empPsiTrainingInfoModule = new EmpPsiTrainingInfoModule();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpPsiTrainingInfoModule[]>(this.baseUrl + '/empPsiTrainingInfo/get-EmpPsiTrainingInfoByEmpId/' + id);
  }

  getSelectedTrainingName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/TrainingName/get-selectedTrainingName/');
  }
  
  saveEmpPsiTrainingInfo(model: FormData) {
    return this.http.post(this.baseUrl + '/empPsiTrainingInfo/save-EmpPsiTrainingInfo', model);
  }
  deleteEmpPsiTrainingInfo(id: number) {
    return this.http.delete(this.baseUrl + '/empPsiTrainingInfo/delete-EmpPsiTrainingInfo/'+id);
  }

}
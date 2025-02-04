import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { EmpTrainingInfo } from '../model/emp-training-info';
import { Observable } from 'rxjs';

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

  getSelectedTrainingType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/training-type/get-selectedtrainingtype');
  }
  getSelectedTrainingName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/TrainingName/get-selectedTrainingName');
  }
  getSelectedInstitute(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Institute/get-selectedinstitute');
  }
  getSelectedCourseDuration(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/courseDuration/get-selectedCourseDurations')
  }

  getEmpTrainingInfo(params: HttpParams): Observable<EmpTrainingInfo[]> {
    return this.http.get<EmpTrainingInfo[]>(this.baseUrl+ "/empTrainingInfo/get-EmpTrainingInfo", {params: params});
  }

}
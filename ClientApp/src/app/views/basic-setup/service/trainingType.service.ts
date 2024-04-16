import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {TrainingType}  from  './../model/trainingType'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TrainingTypeService {
  baseUrl = environment.apiUrl;
  trainingTypes: TrainingType;
  constructor(private http: HttpClient) {
    this.trainingTypes=new TrainingType();
   }


  getById(id: number) {
    return this.http.get<TrainingType>(this.baseUrl + '/training-type/get-trainingtypebyid/' + id);
  }
  getAll():Observable<TrainingType[]> {
    return this.http.get<TrainingType[]>(this.baseUrl + '/training-type/get-trainingType');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/training-type/update-trainingType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/training-type/save-trainingType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/training-type/delete-trainingType/'+id);
  }

}

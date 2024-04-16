import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Ward}  from  './../model/ward'
import { Observable } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class WardService {

  baseUrl = environment.apiUrl;
  wards: Ward;
    constructor(private http: HttpClient) {
      this.wards = new Ward();
     }
  
  getUnion(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/union/get-selectedUnion');
  }
  
  
    find(id: number) {
      return this.http.get<Ward>(this.baseUrl + '/ward/get-wardbyid/' + id);
    }
    getAll():Observable<Ward[]> {
      return this.http.get<Ward[]>(this.baseUrl + '/ward/get-ward');
    }
    update(id: number,model: any) {
      return this.http.put(this.baseUrl + '/ward/update-ward/'+id, model);
    }
    submit(model: any) {
      return this.http.post(this.baseUrl + '/ward/save-ward', model);
    }
    delete(id:number){
      return this.http.delete(this.baseUrl + '/ward/delete-ward/'+id);
    }
  
  }
  
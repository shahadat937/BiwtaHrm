import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Union}  from  './../model/union'
import { Observable } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class UnionService {

  baseUrl = environment.apiUrl;
  unions: Union;
    constructor(private http: HttpClient) {
      this.unions = new Union();
     }
  
  getThana(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Thana/get-selectedThana');
  }
  
  
    find(id: number) {
      return this.http.get<Union>(this.baseUrl + '/union/get-unionbyid/' + id);
    }
    getAll():Observable<Union[]> {
      return this.http.get<Union[]>(this.baseUrl + '/union/get-union');
    }
    update(id: number,model: any) {
      return this.http.put(this.baseUrl + '/union/update-union/'+id, model);
    }
    submit(model: any) {
      return this.http.post(this.baseUrl + '/union/save-union', model);
    }
    delete(id:number){
      return this.http.delete(this.baseUrl + '/union/delete-union/'+id);
    }
  
  }
  
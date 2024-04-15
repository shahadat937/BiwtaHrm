import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Thana}  from  './../model/thana'
import { Observable } from 'rxjs';

import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class ThanaService {
  baseUrl = environment.apiUrl;
  thanas: Thana;
  constructor(private http: HttpClient) {
    this.thanas=new Thana();
   }

   getupazila(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/upazila/get-selectedUpazila')
  }

  getById(id: number) {
    return this.http.get<Thana>(this.baseUrl + '/thana/get-thanabyid/' + id);
  }
  getAll():Observable<Thana[]> {
    return this.http.get<Thana[]>(this.baseUrl + '/thana/get-thana');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/thana/update-thana/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/thana/save-thana', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/thana/delete-thana/'+id);
  }

}

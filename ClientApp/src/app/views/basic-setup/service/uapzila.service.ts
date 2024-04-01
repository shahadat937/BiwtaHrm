import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Upazila}  from  './../model/upazila'
import { Observable } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class UapzilaService {
  baseUrl = environment.apiUrl;
  upazilas: Upazila;
  constructor(private http: HttpClient) {
    this.upazilas=new Upazila();
   }

  getById(id: number) {
    return this.http.get<Upazila>(this.baseUrl + '/upazila/get-upazilabyid/' + id);
  }
  getAll():Observable<Upazila[]> {
    return this.http.get<Upazila[]>(this.baseUrl + '/upazila/get-upazila');
  }

  getdistrict(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/district/get-selectedUpazila')
  }


  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/upazila/update-upazila/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/upazila/save-upazila', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/upazila/delete-upazila/'+id);
  }

}

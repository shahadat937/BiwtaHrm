import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {District}  from  './../model/district'

@Injectable({
  providedIn: 'root'
})
export class DistrictService  {
  baseUrl = environment.apiUrl;
  Districts: District[] = [];
  constructor(private http: HttpClient) { }
  find(id: number) {
    return this.http.get<District>(this.baseUrl + '/district/get-districtbyid/' + id);
  }
  getAll(id: number) {
    return this.http.get<District>(this.baseUrl + '/district/get-district');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/district/update-district/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/district/save-district', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/district/delete-district/'+id);
  }

}

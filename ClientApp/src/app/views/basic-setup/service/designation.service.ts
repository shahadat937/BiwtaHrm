import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { BloodGroup } from '../model/BloodGroup';
import { Observable } from 'rxjs';
import { Designation } from '../model/Designation';
@Injectable({
  providedIn: 'root'
})
export class DesignationService {
  baseUrl = environment.apiUrl;
  designation: Designation;
  constructor(private http: HttpClient) {
    this.designation = new Designation();
   }
  find(id: number) {
    return this.http.get<Designation>(this.baseUrl + '/designation/get-DesignationDetail/' + id);
  }
  getAll():Observable<Designation[]> {
    return this.http.get<Designation[]>(this.baseUrl + '/designation/get-Designation');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/designation/update-Designation/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/designation/save-Designation', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/designation/delete-Designation/'+id);
  }

}

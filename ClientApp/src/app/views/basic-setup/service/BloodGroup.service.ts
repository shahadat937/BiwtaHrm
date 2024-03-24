import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BloodGroup } from '../model/BloodGroup';
@Injectable({
  providedIn: 'root'
})
export class BloodGroupService {
  baseUrl = environment.apiUrl;
  BloodGroups: BloodGroup[] = [];
  constructor(private http: HttpClient) { }
  find(id: number) {
    return this.http.get<BloodGroup>(this.baseUrl + '/blood-group/get-bloodGroupDetail/' + id);
  }
  getAll(id: number) {
    return this.http.get<BloodGroup>(this.baseUrl + '/blood-group/get-bloodGroup');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/blood-group/update-bloodGroup/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/blood-group/save-bloodGroup', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/blood-group/delete-bloodGroup/'+id);
  }

}

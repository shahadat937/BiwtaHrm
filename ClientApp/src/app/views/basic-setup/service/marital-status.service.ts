import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MaritalStatus } from '../model/marital-status';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class MaritalStatusService {

  baseUrl = environment.apiUrl;
  maritalStatus: MaritalStatus;
  constructor(private http: HttpClient) {
    this.maritalStatus = new MaritalStatus();
   }
  find(id: number) {
    return this.http.get<MaritalStatus>(this.baseUrl + '/marital-status/get-maritalStatusById/' + id);
  }
  getAll():Observable<MaritalStatus[]> {
    return this.http.get<MaritalStatus[]>(this.baseUrl + '/marital-status/get-maritalStatus');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/marital-status/update-maritalStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/marital-status/save-maritalStatus', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/marital-status/delete-maritalStatus/'+id);
  }
}

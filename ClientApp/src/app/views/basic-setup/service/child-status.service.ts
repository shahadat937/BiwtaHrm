import { Injectable } from '@angular/core';
import { ChildStatus } from '../model/child-status';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChildStatusService {

  baseUrl = environment.apiUrl;
  childStatus: ChildStatus;
  constructor(private http: HttpClient) {
    this.childStatus = new ChildStatus();
   }
  find(id: number) {
    return this.http.get<ChildStatus>(this.baseUrl + '/childStatus/get-ChildStatusById/' + id);
  }
  getAll():Observable<ChildStatus[]> {
    return this.http.get<ChildStatus[]>(this.baseUrl + '/childStatus/get-ChildStatus');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/childStatus/update-ChildStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/childStatus/save-ChildStatus', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/childStatus/delete-ChildStatus/'+id);
  }
}

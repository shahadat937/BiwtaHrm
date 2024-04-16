import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Branch}  from  './../model/branch'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BranchService {
  baseUrl = environment.apiUrl;
  branchs: Branch;
  constructor(private http: HttpClient) {
    this.branchs=new Branch();
   }


  getById(id: number) {
    return this.http.get<Branch>(this.baseUrl + '/Branch/get-branchbyid/' + id);
  }
  getAll():Observable<Branch[]> {
    return this.http.get<Branch[]>(this.baseUrl + '/Branch/get-branch');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Branch/update-branch/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Branch/save-branch', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Branch/delete-branch/'+id);
  }

}

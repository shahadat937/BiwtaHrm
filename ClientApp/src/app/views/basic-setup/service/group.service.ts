import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Group}  from  './../model/group'
import { Observable } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  baseUrl = environment.apiUrl;
  groups: Group;
    constructor(private http: HttpClient) {
      this.groups = new Group();
     }
  
  getSubject(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Subject/get-selectedsubject');
  }
  
  
    find(id: number) {
      return this.http.get<Group>(this.baseUrl + '/group/get-groupbyid/' + id);
    }
    getAll():Observable<Group[]> {
      return this.http.get<Group[]>(this.baseUrl + '/group/get-group');
    }
    update(id: number,model: any) {
      return this.http.put(this.baseUrl + '/group/update-group/'+id, model);
    }
    submit(model: any) {
      return this.http.post(this.baseUrl + '/group/save-group', model);
    }
    delete(id:number){
      return this.http.delete(this.baseUrl + '/group/delete-group/'+id);
    }
  
  }
  
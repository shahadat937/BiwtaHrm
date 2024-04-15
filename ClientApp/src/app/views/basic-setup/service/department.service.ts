import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Department}  from  './../model/department'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  baseUrl = environment.apiUrl;
  departments: Department;
  constructor(private http: HttpClient) {
    this.departments=new Department();
   }


  getById(id: number) {
    return this.http.get<Department>(this.baseUrl + '/Department/get-departmentbyid/' + id);
  }
  getAll():Observable<Department[]> {
    return this.http.get<Department[]>(this.baseUrl + '/Department/get-department');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Department/update-department/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Department/save-department', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Department/delete-department/'+id);
  }

}

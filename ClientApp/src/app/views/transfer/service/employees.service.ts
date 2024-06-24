import { Employee } from './../../basic-setup/model/employees';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  [x: string]: any;
  baseUrl = environment.apiUrl;
  bsModelRef!: BsModalRef;
  cachedData: any[] = [];
  getemployees:any[]=[];
  private apiUrl = '';
  constructor(private modalService: BsModalService,private http: HttpClient) { }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiUrl);
  }
  saveEmployeeData(employeeData: any): Observable<any> {
    return this.http.post<any>('your-api-endpoint/save-employee', employeeData);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/your-api-endpoint/save-employee', model);
  }
  update(id:number){
    return this.http.delete(this.baseUrl + '/your-api-endpoint/save-employee/'+id);
  }
  demoEmployee: Employee = {
    empId:0,
    employeeName: '',
    department: '',
    designation: ''
  };

}
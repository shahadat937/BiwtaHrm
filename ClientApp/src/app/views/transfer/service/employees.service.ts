import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  bsModelRef!: BsModalRef;
  cachedData: any[] = [];
  getemployees:any[]=[];
  constructor(private modalService: BsModalService,private http: HttpClient) { }

  saveEmployeeData(employeeData: any): Observable<any> {
    return this.http.post<any>('your-api-endpoint/save-employee', employeeData);
  }


   demoEmployee: Employee = {
    empId:0,
    employeeName: '',
    department: '',
    designation: ''
  };
}
// user.model.ts
export interface Employee {
  empId: number;
  employeeName: string;
  department: string;
  designation: string;
}

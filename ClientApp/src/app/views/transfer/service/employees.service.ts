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
    // Make an HTTP POST request to your backend API endpoint
    // Replace 'your-api-endpoint' with the actual endpoint URL
    return this.http.post<any>('your-api-endpoint/save-employee', employeeData);
  }


   demoEmployee: Employee = {
    employeePMSNo:0,
    employeeName: '',
    department: '',
    designation: ''
  };
}
// user.model.ts
export interface Employee {
  employeePMSNo: number;
  employeeName: string;
  department: string;
  designation: string;
}

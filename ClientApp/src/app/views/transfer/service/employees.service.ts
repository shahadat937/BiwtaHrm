import { Employee } from './../../basic-setup/model/employees';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
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

  // getEmployees(): Observable<Employee[]> {
  //   return this.http.get<Employee[]>(this.apiUrl);
  // }

  getAll(): Observable<Employee[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Employee[]>(this.baseUrl + '/BankBranch/get-bankBranch')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }


  // saveEmployeeData(employeeData: any): Observable<any> {
  //   return this.http.post<any>('empBasicInfo/save-EmpBasicInfos', employeeData);
  // }
  // submit(model: any) {
  //   return this.http.post(this.baseUrl + '/empBasicInfo/save-EmpBasicInfos', model);
  // }
  // update(id:number){
  //   return this.http.delete(this.baseUrl + '/empBasicInfo/update-EmpBasicInfos/'+id);
  // }
  demoEmployee: Employee = {
    empId:0,
    employeeName: '',
    department: '',
    designation: ''
  };
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empBasicInfo/update-EmpBasicInfos/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/BankBranch/save-bankBranch', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/BankBranch/delete-bankBranch/'+id);
  }
}
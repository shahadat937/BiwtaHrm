import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class LeaveBalanceService {

  baseUrl: string;
  constructor(
    private http: HttpClient
  ) { 
    this.baseUrl = environment.apiUrl;
  }

  getLeaveBalance(params: HttpParams): Observable<any> {
    return this.http.get<any>(this.baseUrl+`/leaveRequest/get-LeaveAmountForAllLeaveTypeByEmp/`, {params: params});
  }

  getEmpInfo (empId:number): Observable<any>{
    return this.http.get<any>(this.baseUrl+`/empBasicInfo/get-EmpBasicInfosById/${empId}`);
  }

  getEmpInfoByCardNo(IdCardNo: string): Observable<any> {
    return this.http.get<any>(this.baseUrl+`/empBasicInfo/get-empBasicInfoByIdCardNo/${IdCardNo}`);
  }
}

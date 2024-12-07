import { Injectable } from '@angular/core';
import { LeaveType } from '../model/leave-type';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()

export class LeaveTypeService {
  cachedData: any[] = [];
  baseUrl: string ;
  leaveTypes: LeaveType;
  constructor(private http: HttpClient) { 
    this.baseUrl = environment.apiUrl;
    this.leaveTypes = new LeaveType();
  }

  getLeaveTypes(showReport?:boolean): Observable<LeaveType[]> {
    let params = new HttpParams();
    if(showReport) {
      params = params.set('showReport', showReport);
    }
    return this.http.get<LeaveType[]>(this.baseUrl+"/leaveType/get-leaveType", {params: params})
  }

  updateLeaveType(leaveType:LeaveType): Observable<any> {
    return this.http.put<any>(this.baseUrl+"/leaveType/update-leaveType",leaveType);
  }

  deleteLeaveType(leaveTypeId:number):Observable<any> {
    return this.http.delete<any>(this.baseUrl+`/leaveType/delete-leaveType/${leaveTypeId}`);
  }

  createLeaveType(leaveType:LeaveType): Observable<any> {
    return this.http.post<any>(this.baseUrl+"/leaveType/save-leaveType", leaveType);
  }

  getTakenLeaveReport(EmpIds:number[]) : Observable<any[]> {
    console.log(EmpIds);
    let params = new HttpParams();
    EmpIds.forEach(id => {
      params = params.append('empId',id);
    })

    return this.http.get<any[]>(this.baseUrl + "/leaveRequest/get-TakenLeaveReport", {params: params});
  }

}

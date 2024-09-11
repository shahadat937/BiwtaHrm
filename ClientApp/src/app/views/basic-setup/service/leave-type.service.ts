import { Injectable } from '@angular/core';
import { LeaveType } from '../model/leave-type';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
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

  getLeaveTypes(): Observable<LeaveType[]> {
    return this.http.get<LeaveType[]>(this.baseUrl+"/leaveType/get-leaveType")
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

}

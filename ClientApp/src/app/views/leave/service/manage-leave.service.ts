import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {LeaveModel} from '../models/leave-model'

@Injectable()
export class ManageLeaveService {
  cachedData: any[] = []
  baseUrl: string;


  constructor(
    private http: HttpClient
  ) { 
    this.baseUrl = environment.apiUrl;
  }

  getSelectedDepartment(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/department/get-selecteddepartment");
  }

  getLeaves(): Observable<LeaveModel[]> {
    return this.http.get<LeaveModel[]>(this.baseUrl+"/leaveRequest/get-LeaveRequest");
  }

  getLeaveStatusOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/leaveRequest/get-LeaveStatusOption");
  }

  getLeaveById(id:number) : Observable<LeaveModel> {
    return this.http.get<LeaveModel>(this.baseUrl+`/leaveRequest/get-LeaveRequestById/${id}`)
  }

  approveLeaveRequestByReviewer(leaveRequestId:number): Observable<any> {
    return this.http.put<any>(this.baseUrl + `/leaveRequest/approve-LeaveRequestByReviewer/${leaveRequestId}`,{});
  }

  denyLeaveRequestByReviewer(leaveRequestId:number): Observable<any> {
    return this.http.put<any>(this.baseUrl+`/leaveRequest/deny-LeaveRequestByReviewer/${leaveRequestId}`,{});
  }

  approveFinalLeaveRequest(leaveRequestId:number) : Observable<any> {
    return this.http.put<any>(this.baseUrl + `/leaveRequest/approve-LeaveRequestFinal/${leaveRequestId}`,{});
  }

  denyFinalLeaveRequest(leaveRequestId:number): Observable<any> {
    return this.http.put<any>(this.baseUrl+`/leaveRequest/deny-LeaveRequestFinal/${leaveRequestId}`,{});
  }

  getLeaveByFilter(params: HttpParams): Observable<LeaveModel[]> {
    return this.http.get<LeaveModel[]>(this.baseUrl+"/leaveRequest/get-LeaveRequestByFilter", {params:params});
  }
  
  getOldLeaveRequest(): Observable<LeaveModel[]> {
    return this.http.get<LeaveModel[]>(this.baseUrl+"/leaveRequest/get-OldLeaveRequest");
  }

  getLeaveFiles(leaveRequestId: number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+`/leaveRequest/get-LeaveFilesByLeaveRequestId/${leaveRequestId}`);
  }

  updateLeaveRequest(leaveRequest:FormData): Observable<any> {
    return this.http.put<any>(this.baseUrl+'/leaveRequest/update-LeaveRequest', leaveRequest)
  }

  deleteLeaveRequest(leaveRequestId:number): Observable<any> {
    return this.http.delete<any>(this.baseUrl+`/leaveRequest/delete-LeaveRequest/${leaveRequestId}`);
  }
}

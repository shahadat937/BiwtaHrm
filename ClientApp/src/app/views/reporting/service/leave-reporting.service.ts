import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LeaveReportingModal } from '../models/leave-reporting';

@Injectable({
  providedIn: 'root'
})
export class LeaveReportingService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getLeaveReporting(queryParams: any, departmentId: number, sectionId: number, designationId: number, leaveType: number, fromDate: any, toDate: any) {
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    params = params.append('designationId', designationId);
    params = params.append('leaveType', leaveType);
    params = params.append('fromDate', fromDate);
    params = params.append('toDate', toDate);
    return this.http.get<LeaveReportingModal>(this.baseUrl + '/reporting/get-leaveReportingResult', { params });
  }

}

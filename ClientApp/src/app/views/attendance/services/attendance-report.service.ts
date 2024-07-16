import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AttendanceReportService {
  baseUrl:string;
  constructor(private http:HttpClient) { 
    this.baseUrl = environment.apiUrl;
  }

  getEmpSummary(params:HttpParams):Observable<any> {
    return this.http.get<any>(this.baseUrl+"/attendance/get-AttendanceSummary", {params:params});
  }

  getOfficeOption():Observable<any> {
    return this.http.get<any[]>(this.baseUrl+"/Office/get-selectedoffice");
  }

  getDepartmentOption(officeId:number):Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+`/department/get-SelectedDepartmentByOfficeId/${officeId}`);
  }

  getFilteredEmpOption(filter:any):Observable<any[]> {

    return this.http.get<any[]>(this.baseUrl+"/empBasicInfo/get-SelectedFilteredEmpBasicInfo",{params:filter})
  }
}

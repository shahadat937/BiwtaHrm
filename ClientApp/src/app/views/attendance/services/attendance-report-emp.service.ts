import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AttendanceReportEmpService {
  cachedData: any[] = [];
  cachedFilter: HttpParams| null;
  baseUrl: string;
  constructor(
    private http:HttpClient
  ) {
    this.baseUrl = environment.apiUrl;
    this.cachedFilter = null;
   }
   
  getOfficeOption():Observable<any> {
    return this.http.get<any[]>(this.baseUrl+"/Office/get-selectedoffice");
  }

  getShiftOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/Shift/get-selectedshift");
  }

  getDepartmentOption(officeId:number):Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+`/department/get-SelectedDepartmentByOfficeId/${officeId}`);
  }

  getEmpOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + "/empBasicInfo/get-SelectedEmpBasicInfo");
  }

  getFilteredEmpOption(filter:any):Observable<any[]> {

    return this.http.get<any[]>(this.baseUrl+"/empBasicInfo/get-SelectedFilteredEmpBasicInfo",{params:filter})
  }

  getDesignationOption(departmentId:number) {
    return this.http.get<any[]>(this.baseUrl+`/designation/get-selectedDesignationByDepartmentId/${departmentId}`);
  }

  getAttendanceReport(filter:HttpParams):Observable<any[]> {

    if(this.cachedFilter==filter && this.cachedData.length>0) {
      return of (this.cachedData);
    }

    return this.http.get<any[]>(this.baseUrl+"/attendance/get-AttendanceReportByFilter",{params:filter});
  }
}

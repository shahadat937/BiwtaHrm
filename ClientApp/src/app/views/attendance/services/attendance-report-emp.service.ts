import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AttendanceReportEmpService {
  baseUrl: string;
  constructor(
    private http:HttpClient
  ) {
    this.baseUrl = environment.apiUrl;
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
}

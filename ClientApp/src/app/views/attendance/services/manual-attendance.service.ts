import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Attendances } from '../models/attendances';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ManualAttendanceService {
  cachedData: any[] =[];
  baseUrl = environment.apiUrl;
  attendances: Attendances;
  constructor(private http:HttpClient) { 
    this.attendances = new Attendances();
  }

  submit(model:Attendances) {
    console.log(model);
    if(model.inTime!=""&&model.inTime!=null) {
      model.inTime = model.inTime+":00";
    }

    if(model.outTime!=""&&model.outTime!=null) {
      model.outTime = model.outTime +":00";
    }

    return this.http.post(this.baseUrl+"/attendance/save-ManualAttendance",model);
  }

  getOfficeOption():Observable<any> {
    return this.http.get<any[]>(this.baseUrl+"/Office/get-selectedoffice");
  }

  getShiftOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/Shift/get-selectedshift");
  }

  getAttendanceStatusOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/attendanceStatus/get-SelectedAttendanceStatus")
  }

  getDepartmentOption(officeId:number):Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+`/department/get-SelectedDepartmentByOfficeId/${officeId}`);
  }

  getEmpOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + "/empBasicInfo/get-SelectedEmpBasicInfo");
  }
}

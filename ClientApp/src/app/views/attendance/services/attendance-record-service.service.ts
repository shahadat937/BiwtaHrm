import { Injectable } from '@angular/core';
import { AttendanceRecordModel } from '../models/attendance-record-model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttendanceRecordService {
  cachedData: any[]=[];
  AtdReportModel:AttendanceRecordModel;
  baseUrl:string;

  constructor(private http:HttpClient) { 
    this.AtdReportModel = new AttendanceRecordModel();
    this.baseUrl = environment.apiUrl;  
  }

  getAll():Observable<AttendanceRecordModel[]> {
    if(this.cachedData.length>0) {
      return of (this.cachedData);
    }
    return this.http.get<AttendanceRecordModel[]>(this.baseUrl+"/attendance/get-Attendance");
  }

  updateById(model:any) {
    return this.http.put(this.baseUrl+"/attendance/update-AttendanceById",model);
  }
}

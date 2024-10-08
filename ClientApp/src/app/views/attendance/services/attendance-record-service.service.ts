import { Injectable } from '@angular/core';
import { AttendanceRecordModel,AttendanceResult } from '../models/attendance-record-model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';
import { UpdateAttendanceModel } from '../models/update-attendance-model';

@Injectable()
export class AttendanceRecordService {
  cachedData: any;
  AtdRecordModel:AttendanceResult;
  UpdateAtdModel: UpdateAttendanceModel;
  baseUrl:string;

  constructor(private http:HttpClient) { 
    this.AtdRecordModel = new AttendanceResult();
    this.UpdateAtdModel = new UpdateAttendanceModel();
    this.baseUrl = environment.apiUrl;
    this.cachedData = null;
  }

  getAll():Observable<AttendanceResult> {
    //if(this.cachedData) {
    //  return of (this.cachedData);
    //}
    return this.http.get<AttendanceResult>(this.baseUrl+"/attendance/get-Attendance");
  }

  getAttendance(params: HttpParams): Observable<AttendanceResult> {
    return this.http.get<AttendanceResult>(this.baseUrl+"/attendance/get-Attendance",{params:params});
  }

  update(model:any) {
    return this.http.put(this.baseUrl+"/attendance/update-AttendanceById",model);
  }

  delete(attendanceId:number) {
    return this.http.delete(this.baseUrl+`/attendance/delete-AttendanceById/${attendanceId}`);
  }
  getAttendanceStatusOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/attendanceStatus/get-SelectedAttendanceStatus")
  }


  getAttendanceById(id:number):Observable<UpdateAttendanceModel> {
    return this.http.get<UpdateAttendanceModel>(this.baseUrl+`/attendance/get-AttendanceById/${id}`);
  }
}

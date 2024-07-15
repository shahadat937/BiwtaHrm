import { Injectable } from '@angular/core';
import { AttendanceRecordModel } from '../models/attendance-record-model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';
import { UpdateAttendanceModel } from '../models/update-attendance-model';

@Injectable({
  providedIn: 'root'
})
export class AttendanceRecordService {
  cachedData: any[]=[];
  AtdRecordModel:AttendanceRecordModel;
  UpdateAtdModel: UpdateAttendanceModel;
  baseUrl:string;

  constructor(private http:HttpClient) { 
    this.AtdRecordModel = new AttendanceRecordModel();
    this.UpdateAtdModel = new UpdateAttendanceModel();
    this.baseUrl = environment.apiUrl;  
  }

  getAll():Observable<AttendanceRecordModel[]> {
    if(this.cachedData.length>0) {
      return of (this.cachedData);
    }
    return this.http.get<AttendanceRecordModel[]>(this.baseUrl+"/attendance/get-Attendance");
  }

  update(model:any) {
    return this.http.put(this.baseUrl+"/attendance/update-AttendanceById",model);
  }

  delete(attendanceId:number) {
    return this.http.delete(this.baseUrl+`/attendance/delete-AttendanceById/${attendanceId}`);
  }
  getAttendanceStatusOption():Observable<any[]> {
    console.log("Hello World");
    return this.http.get<any[]>(this.baseUrl+"/attendanceStatus/get-SelectedAttendanceStatus")
  }

  getAttendance(id:number) {
    console.log(id);
  }

  getAttendanceById(id:number):Observable<UpdateAttendanceModel> {
    return this.http.get<UpdateAttendanceModel>(this.baseUrl+`/attendance/get-AttendanceById/${id}`);
  }
}

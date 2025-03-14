import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AttendanceDeviceModel } from '../model/attendance-device-model';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { PendingDeviceModel } from '../model/pending-device-model';
import { AssignEmployeeModel } from '../model/assign-employee-model';

@Injectable({
  providedIn: 'root'
})
export class AttendanceDeviceService {

  baseUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getDevice(): Observable<AttendanceDeviceModel[]> {
    return this.http.get<AttendanceDeviceModel[]>(this.baseUrl + "/AttendanceDevice/get-Device");
  }

  getPendingDevice(): Observable<PendingDeviceModel[]> {
    return this.http.get<PendingDeviceModel[]>(this.baseUrl + "/AttendanceDevice/get-PendingDevice");
  }

  addDevice(device: AttendanceDeviceModel) : Observable<any> {
    return this.http.post<any>(this.baseUrl + "/AttendanceDevice/add-Device",device);
  }

  updateDevice(device: AttendanceDeviceModel): Observable<any> {
    return this.http.put<any>(this.baseUrl + "/AttendanceDevice/update-Device", device);
  }

  rebootDevice(deviceId:number): Observable<any> {
    return this.http.post<any>(this.baseUrl+`/AttendanceDevice/reboot-Device/${deviceId}`,{});
  }

  deleteDevice(deviceId:number): Observable<any> {
    return this.http.delete<any>(this.baseUrl + `/AttendanceDevice/delete-Device/${deviceId}`);
  }

  assignEmployee(employee: AssignEmployeeModel):Observable<any> {
    return this.http.post<any>(this.baseUrl + "/AttendanceDevice/add-User",employee);
  }

  getSelectedDevice() : Observable<any[]> {
    return this.http.get<any>(this.baseUrl + "/AttendanceDevice/get-SelectedDevice");
  }

  enrollFingerprint(employee:any): Observable<any> {
    return this.http.post<any>(this.baseUrl + "/AttendanceDevice/enroll-Fingerprint",employee);
  }

  customCommand(command:FormData) : Observable<any> {
    return this.http.post<any>(this.baseUrl + "/AttendanceDevice/add-CustomCommand", command); 
  }

}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {LeaveModel} from '../models/leave-model'

@Injectable({
  providedIn: 'root'
})
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
}

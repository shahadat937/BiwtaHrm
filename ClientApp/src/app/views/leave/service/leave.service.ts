import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AddLeaveModel } from '../models/add-leave-model';
import { LeaveModel } from '../models/leave-model';

@Injectable()
export class LeaveService {

  cachedData: any[] = [];
  baseUrl : string;
  addLeaveModel: AddLeaveModel
  constructor(
    private http:HttpClient
  ) { 
    this.baseUrl = environment.apiUrl;
    this.addLeaveModel = new AddLeaveModel();
  }

  getSelectedLeaveType(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/leaveType/get-SelectedLeaveType");
  }
}

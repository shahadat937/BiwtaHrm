import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {LeaveRulesModel} from "../model/leave-rules-model"

@Injectable({
  providedIn: 'root'
})
export class LeaveRuleService {
  cachedData: any[] = [];
  baseUrl: string ;
  leaveRule: LeaveRulesModel; 
  constructor(private http: HttpClient) { 
    this.baseUrl = environment.apiUrl;
    this.leaveRule = new LeaveRulesModel();
  }

  getSelectedLeaveType():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/leaveType/get-SelectedLeaveType");
  }

  getLeaveRules(leaveTypeId:number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+`/leaveRules/get-LeaveRulesByLeaveTypeId/${leaveTypeId}`);
  }

  deleteLeaveRule(id:number):Observable<any> {
    return this.http.delete<any>(this.baseUrl+`/leaveRules/delete-LeaveRule/${id}`);
  }

  getSelectedRuleName(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/leaveRules/get-SelectedLeaveRulesName");
  }

  saveLeaveRule(leaveRule:LeaveRulesModel): Observable<any> {
    return this.http.post<any>(this.baseUrl+"/leaveRules/save-LeaveRule", leaveRule);
  }

  updateLeaveRule(leaveRule: LeaveRulesModel) {
    return this.http.put<any>(this.baseUrl+"/leaveRules/update-LeaveRule",leaveRule);
  }

  getLeaveRuleById(id:number):Observable<any> {
    return this.http.get<any>(this.baseUrl+"/leaveRules/get-leave")
  }
}

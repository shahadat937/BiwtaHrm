import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {AddLeaveModel} from '../models/add-leave-model'

@Injectable()
export class AddLeaveService {
  cachedData: any[] = [];
  baseUrl : string;
  addLeaveModel: AddLeaveModel; 

  constructor(
    private http: HttpClient
  ) { 
    this.baseUrl = environment.apiUrl;
    this.addLeaveModel = new AddLeaveModel();
  }

  getSelectedLeaveType(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/leaveType/get-SelectedLeaveType");
  }

  getEmpInfoByCard(cardNo:string):Observable<any> {
    return this.http.get<any>(this.baseUrl+`/empBasicInfo/get-empBasicInfoByIdCardNo/${cardNo}`);
  }

  getLeaveAmount(params:HttpParams): Observable<any> {
    return this.http.get<any>(this.baseUrl + "/leaveRequest/get-LeaveAmount", {params: params});
  }

  getSelectedCountry(): Observable<any[]> {
    return this.http.get<any>(this.baseUrl + "/country/get-selectedCountrys");
  }

  createLeaveRequest(formData: FormData):Observable<any> {
    return this.http.post<any>(this.baseUrl+"/leaveRequest/save-LeaveRequest",formData);
  }
}

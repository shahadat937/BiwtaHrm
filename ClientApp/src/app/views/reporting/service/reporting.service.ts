import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { EmpCountOnReportingDto } from '../models/emp-count-on-reporting-dto'

@Injectable({
  providedIn: 'root'
})
export class ReportingService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getEmployeeTypeCount(){
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-employeeTypeCount/');
  }  
  getEmployeeTypeReportingResult(queryParams: any, id:any, unAssigned: boolean){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    params = params.append('unAssigned', unAssigned);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-employeeTypeReportingResult`, { params });
  }


}

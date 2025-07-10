import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PrlRetirementReporting } from '../models/prl-retirement-reporting';

@Injectable({
  providedIn: 'root'
})
export class PrlRetirmentService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getPrlReporting(queryParams: any, CurrentDate: any, StartDate: any, EndDate: any, DepartmentId: any, SectionId: any, DesignationId: any, IsPRL: any, IsRetirment: any, IsGone: any, IsWillGone: any) {
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('CurrentDate', CurrentDate);
    params = params.append('StartDate', StartDate);
    params = params.append('EndDate', EndDate);
    params = params.append('DepartmentId', DepartmentId);
    params = params.append('SectionId', SectionId);
    params = params.append('DesignationId', DesignationId);
    params = params.append('IsPRL', IsPRL);
    params = params.append('IsRetirment', IsRetirment);
    params = params.append('IsGone', IsGone);
    params = params.append('IsWillGone', IsWillGone);
    return this.http.get<PrlRetirementReporting[]>(this.baseUrl + '/reporting/get-prlReportingResult', { params });
  }

}

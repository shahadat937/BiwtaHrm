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

      // Employee Type
  getEmployeeTypeCount(departmentId: any, sectionId: any){
    let queryParams;
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-employeeTypeCount', { params });
  }  
  getEmployeeTypeReportingResult(queryParams: any, id:number, unAssigned: boolean, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    params = params.append('unAssigned', unAssigned);
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-employeeTypeReportingResult`, { params });
  }

      // Religion
  getReligionCount(departmentId: any, sectionId: any){
    let queryParams;
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-religionCount', { params });
  }  
  getReligionReportingResult(queryParams: any, id:number, unAssigned: boolean, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    params = params.append('unAssigned', unAssigned);
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-religionReportingResult`, { params });
  }

  
  // Blood Group
  getBloodGroupCount(departmentId: any, sectionId: any){
    let queryParams;
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-BloodGroupCount', { params });
  }  
  getBloodGroupReportingResult(queryParams: any, id:number, unAssigned: boolean, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    params = params.append('unAssigned', unAssigned);
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-BloodGroupReportingResult`, { params });
  }
  
  // Gender
  getGenderCount(departmentId: any, sectionId: any){
    let queryParams;
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-genderCount', { params });
  }  
  getGenderReportingResult(queryParams: any, id:number, unAssigned: boolean, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    params = params.append('unAssigned', unAssigned);
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-genderReportingResult`, { params });
  }
  
  // Marital Status
  getMaritalStatusCount(departmentId: any, sectionId: any){
    let queryParams;
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-maritalStatusCount', { params });
  }  
  getMaritalStatusReportingResult(queryParams: any, id:number, unAssigned: boolean, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    params = params.append('unAssigned', unAssigned);
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-maritalStatusReportingResult`, { params });
  }


}

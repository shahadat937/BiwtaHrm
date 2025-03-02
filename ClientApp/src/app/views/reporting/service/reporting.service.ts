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
  getEmployeeTypeCount(departmentId: number, sectionId: number){
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
  getReligionCount(departmentId: number, sectionId: number){
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
  getBloodGroupCount(departmentId: number, sectionId: number){
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
  getGenderCount(departmentId: number, sectionId: number){
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
  getMaritalStatusCount(departmentId: number, sectionId: number){
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

  
  // Language
  getLanguageCount(departmentId: number, sectionId: number){
    let queryParams;
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-languageCount', { params });
  }  
  getLanguageReportingResult(queryParams: any, id:number, unAssigned: boolean, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    params = params.append('unAssigned', unAssigned);
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-LanguageReportingResult`, { params });
  }
  
  
  // TrainingType
  getTrainingTypeCount(departmentId: number, sectionId: number){
    let queryParams;
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-TrainingTypeCount', { params });
  }  
  getTrainingTypeReportingResult(queryParams: any, id:number, unAssigned: boolean, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    params = params.append('unAssigned', unAssigned);
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-TrainingTypeReportingResult`, { params });
  }
  

  // Employee List
  getEmployeeListReportingResult(queryParams: any, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-employeeListReporting`, { params });
  }

  
  // Vacant List
  getVacantListReportingResult(queryParams: any, departmentId: number, sectionId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-vacantReportingResult`, { params });
  }

  
  // Transfer & Posting
  getTransferPostingCount(departmentFrom: number, sectionFrom: number, departmentTo: number, sectionTo: number, dateTo: any, dateFrom: any){
    let queryParams;
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentFrom', departmentFrom);
    params = params.append('sectionFrom', sectionFrom);
    params = params.append('departmentTo', departmentTo);
    params = params.append('sectionTo', sectionTo);
    params = params.append('dateTo', dateTo);
    params = params.append('dateFrom', dateFrom);
    return this.http.get<EmpCountOnReportingDto>(this.baseUrl + '/reporting/get-TransferPostingCount', { params });
  }  
  getTransferPostingReportingResult(queryParams: any, departmentFrom: number, sectionFrom: number, departmentTo: number, sectionTo: number, dateTo: any, dateFrom: any, departmentStatus: any, joiningStatus: any){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentFrom', departmentFrom);
    params = params.append('sectionFrom', sectionFrom);
    params = params.append('departmentTo', departmentTo);
    params = params.append('sectionTo', sectionTo);
    params = params.append('dateTo', dateTo);
    params = params.append('dateFrom', dateFrom);
    params = params.append('departmentStatus', departmentStatus);
    params = params.append('joiningStatus', joiningStatus);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-TransferPostingReport`, { params });
  }


  
  // Address Reporting
  getAddressReportingResult(queryParams: any, isPresentAddress: boolean, departmentId: number, sectionId: number, countryId: number, divisionId: number, districtId: number, upazilaId: number){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('departmentId', departmentId);
    params = params.append('sectionId', sectionId);
    params = params.append('isPresentAddress', isPresentAddress);
    params = params.append('countryId', countryId);
    params = params.append('divisionId', divisionId);
    params = params.append('districtId', districtId);
    params = params.append('upazilaId', upazilaId);
    return this.http.get<any>(`${this.baseUrl}/reporting/get-addressReportingResult`, { params });
  }
}

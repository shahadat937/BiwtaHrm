import { Injectable } from '@angular/core';
import { EmpWorkHistory } from '../model/emp-work-history';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmpWorkHistoryService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empWorkHistory: EmpWorkHistory;

  constructor(private http: HttpClient) { 
    this.empWorkHistory = new EmpWorkHistory();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpWorkHistory[]>(this.baseUrl + '/empWorkHistory/get-EmpWorkHistoryByEmpId/' + id);
  }

  saveEmpWorkHistory(model: any) {
    return this.http.post(this.baseUrl + '/empWorkHistory/save-EmpWorkHistory', model);
  }
  deleteEmpWorkHistory(id: number) {
    return this.http.delete(this.baseUrl + '/empWorkHistory/delete-EmpWorkHistory/'+id);
  }

  findById(id: number) {
    return this.http.get<EmpWorkHistory>(this.baseUrl + '/empWorkHistory/get-EmpWorkHistoryDetails/' + id);
  }

  findCombinedById(id: number) {
    return this.http.get<EmpWorkHistory[]>(this.baseUrl + '/empWorkHistory/get-CombinedEmpWorkHistoryByEmpId/' + id);
  }
}

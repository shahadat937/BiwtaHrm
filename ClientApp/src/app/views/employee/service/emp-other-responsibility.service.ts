import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpOtherResponsibility } from '../model/emp-other-responsibility';

@Injectable({
  providedIn: 'root'
})
export class EmpOtherResponsibilityService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empOtherResponsibility: EmpOtherResponsibility;

  constructor(private http: HttpClient) { 
    this.empOtherResponsibility = new EmpOtherResponsibility();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpOtherResponsibility[]>(this.baseUrl + '/empOtherResponsibility/get-EmpOtherResponsibilityByEmpId/' + id);
  }

  saveEmpOtherResponsibility(model: FormData) {
    return this.http.post(this.baseUrl + '/empOtherResponsibility/save-EmpOtherResponsibility', model);
  }
  deleteEmpOtherResponsibility(id: number) {
    return this.http.delete(this.baseUrl + '/empOtherResponsibility/delete-EmpOtherResponsibility/'+id);
  }
  updateEmpOtherResponsibilityStatus(id: number) {
    return this.http.get(this.baseUrl + '/empOtherResponsibility/update-EmpOtherResponsibilityStatusByEmpId/'+id);
  }

}


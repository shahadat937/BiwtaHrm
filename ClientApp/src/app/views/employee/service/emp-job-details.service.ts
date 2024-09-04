import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpJobDetailsModule } from '../model/emp-job-details.module';
import { HttpClient } from '@angular/common/http';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Scale } from '../../basic-setup/model/Scale';

@Injectable({
  providedIn: 'root'
})
export class EmpJobDetailsService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empJobDetails: EmpJobDetailsModule;

  constructor(private http: HttpClient) { 
    this.empJobDetails = new EmpJobDetailsModule();
  }
  
  findByEmpId(id: number | null) {
    return this.http.get<EmpJobDetailsModule>(this.baseUrl + '/empJobDetail/get-EmpJobDetailByEmpId/' + id);
  }
  
  getDesignationByDepartmentId(departmentId: number | null, empJobDetailId: number,) {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/designation/get-selectedDesignationByDepartment?&departmentId=' + departmentId + '&empJobDetailId='+empJobDetailId);
  }
  
  getDesignationBySectionId(sectionId: number, empJobDetailId: number) {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/designation/get-selectedDesignationBySection?&sectionId=' + sectionId + '&empJobDetailId='+empJobDetailId);
  }
  
  getDesignationByOfficeId(officeId: number, empJobDetailId: number,) {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/designation/get-selectedDesignationByOffice?&officeId=' + officeId + '&empJobDetailId='+empJobDetailId);
  }

  getScaleByGradeId(id: number) {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/scale/get-selectedScale/' + id);
  }

  getBasicPayByScale(id: number) {
    return this.http.get<Scale>(this.baseUrl + '/scale/get-scaleDetail/' + id);
  }

  getAllDepartment(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department/get-selecteddepartment');
  }
  getOldDesignationByDepartment(id : number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/designation/get-selectedDesignationByDepartmentId/' + id);
  }
  
  getSelectedSection(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/section/get-selectedSection');
  }
  
  saveEmpJobDetails(model: any) {
    return this.http.post(this.baseUrl + '/empJobDetail/save-EmpJobDetails', model);
  }
  updateEmpJobDetails(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empJobDetail/update-EmpJobDetails/'+id, model);
  }
  
}

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpTransferPosting } from '../model/emp-transfer-posting';
import { Observable, of, map } from 'rxjs';
import { BasicInfoModule } from '../../employee/model/basic-info.module';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmpTransferPostingService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empTransferPosting: EmpTransferPosting;

  constructor(private http: HttpClient) { 
    this.empTransferPosting = new EmpTransferPosting();
  }

  getAll(queryParams: any, id: any): Observable<any> {
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    return this.http
      .get<any>(this.baseUrl + '/empTransferPosting/get-allEmpTransferPosting', {params})
      .pipe(
        map((data) => {
          this.cachedData = data; 
          return data;
        })
      );
  }

  getAllEmpTransferPostingApproveInfo(){
    return this.http.get<EmpTransferPosting[]>(this.baseUrl + '/empTransferPosting/get-AllEmpTransferPostingApproveInfo');
  }
  
  getAllEmpTransferPostingDeptApproveInfo(queryParams: any, empId:any, id: any){
        let params = new HttpParams({ fromObject: queryParams }); 
        params = params.append('empId', empId);
        params = params.append('id', id);
        return this.http.get<any>(`${this.baseUrl}/empTransferPosting/get-EmpTransferPostingDeptApprove`, { params });
  }
  
  getAllEmpTransferPostingJoiningInfo(queryParams: any, empId:any, id: any){
    let params = new HttpParams({ fromObject: queryParams }); 
    params = params.append('empId', empId);
    params = params.append('id', id);
    return this.http.get<any>(`${this.baseUrl}/empTransferPosting/get-EmpTransferPostingJoiningInfo`, { params });
  }
  
  findById(id: number) {
    return this.http.get<EmpTransferPosting>(this.baseUrl + '/empTransferPosting/get-EmpTransferPostingById/' + id);
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpTransferPosting>(this.baseUrl + '/empTransferPosting/get-EmpTransferPostingByEmpId/' + id);
  }

  findAllByEmpId(id: number) {
    return this.http.get<EmpTransferPosting[]>(this.baseUrl + '/empTransferPosting/get-AllEmpTransferPostingByEmpId/' + id);
  }

  getEmpBasicInfoByIdCardNo(id: string){
    return this.http.get<BasicInfoModule>(this.baseUrl + '/empBasicInfo/get-empBasicInfoByIdCardNo/' + id);
  }

  getDesignationByDepartment(id : number | null){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/designation/get-selectedDesignationByDepartmentId/' + id);
  }
  
  getSelectedReleaseType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/releaseType/get-selectedReleaseType');
  }
  
  saveEmpTransferPosting(model: any) {
  return this.http.post(this.baseUrl + '/empTransferPosting/save-EmpTransferPosting', model);
  }
  updateEmpTransferPosting(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empTransferPosting/update-EmpTransferPosting/'+id, model);
  }
  
  updateEmpTransferPostingStatus(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empTransferPosting/update-EmpTransferPostingStatus/'+id, model);
  }

  CurrentDeptJoinDateByEmpId(id: number) {
    return this.http.get(this.baseUrl + '/empTransferPosting/get-currentDeptJoinDateByEmpId/' + id);
  }

  
  deleteEmpTransferPosting(id: number) {
    return this.http.delete(this.baseUrl + '/empTransferPosting/delete-EmpTransferPosting/'+id);
  }
  
}

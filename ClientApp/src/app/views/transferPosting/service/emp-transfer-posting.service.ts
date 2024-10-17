import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpTransferPosting } from '../model/emp-transfer-posting';
import { Observable, of, map } from 'rxjs';
import { BasicInfoModule } from '../../employee/model/basic-info.module';
import { SelectedModel } from 'src/app/core/models/selectedModel';

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

  getAll(): Observable<EmpTransferPosting[]> {
    if (this.cachedData.length > 0) {
      return of (this.cachedData);
    } else {
      return this.http
        .get<EmpTransferPosting[]>(this.baseUrl + '/empTransferPosting/get-allEmpTransferPosting')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }

  getAllEmpTransferPostingApproveInfo(){
    return this.http.get<EmpTransferPosting[]>(this.baseUrl + '/empTransferPosting/get-AllEmpTransferPostingApproveInfo');
  }
  
  getAllEmpTransferPostingDeptApproveInfo(id: number){
    return this.http.get<EmpTransferPosting[]>(this.baseUrl + '/empTransferPosting/get-EmpTransferPostingDeptApprove/' + id);
  }
  
  getAllEmpTransferPostingJoiningInfo(id: number){
    return this.http.get<EmpTransferPosting[]>(this.baseUrl + '/empTransferPosting/get-EmpTransferPostingJoiningInfo/' + id);
  }
  
  findById(id: number) {
    return this.http.get<EmpTransferPosting>(this.baseUrl + '/empTransferPosting/get-EmpTransferPostingById/' + id);
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpTransferPosting>(this.baseUrl + '/empTransferPosting/get-EmpTransferPostingByEmpId/' + id);
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
  
}

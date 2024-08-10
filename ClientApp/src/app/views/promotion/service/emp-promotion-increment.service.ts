import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EmpPromotionIncrement } from '../model/emp-promotion-increment';

@Injectable({
  providedIn: 'root'
})
export class EmpPromotionIncrementService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empPromotionIncrement: EmpPromotionIncrement;

  constructor(private http: HttpClient) { 
    this.empPromotionIncrement = new EmpPromotionIncrement();
  }

  getAll(): Observable<EmpPromotionIncrement[]> {
    if (this.cachedData.length > 0) {
      return of (this.cachedData);
    } else {
      return this.http
        .get<EmpPromotionIncrement[]>(this.baseUrl + '/empPromotionIncrement/get-allEmpPromotionIncrement')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }

  getAllEmpPromotionIncrementApproveInfo(){
    return this.http.get<EmpPromotionIncrement[]>(this.baseUrl + '/empPromotionIncrement/get-AllEmpPromotionIncrementApproveInfo');
  }
  
  getAllEmpPromotionIncrementDeptApproveInfo(){
    return this.http.get<EmpPromotionIncrement[]>(this.baseUrl + '/empPromotionIncrement/get-EmpPromotionIncrementDeptApprove');
  }
  
  getAllEmpPromotionIncrementJoiningInfo(){
    return this.http.get<EmpPromotionIncrement[]>(this.baseUrl + '/empPromotionIncrement/get-EmpPromotionIncrementJoiningInfo');
  }
  
  findById(id: number) {
    return this.http.get<EmpPromotionIncrement>(this.baseUrl + '/empPromotionIncrement/get-EmpPromotionIncrementById/' + id);
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpPromotionIncrement>(this.baseUrl + '/empPromotionIncrement/get-EmpPromotionIncrementByEmpId/' + id);
  }

  
  saveEmpPromotionIncrement(model: any) {
  return this.http.post(this.baseUrl + '/empPromotionIncrement/save-EmpPromotionIncrement', model);
  }
  updateEmpPromotionIncrement(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empPromotionIncrement/update-EmpPromotionIncrement/'+id, model);
  }
  
  
}


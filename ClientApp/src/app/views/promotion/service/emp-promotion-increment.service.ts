import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { environment } from '../../../../../src/environments/environment';
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

  getAll(queryParams: any, id: any): Observable<any> {
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('id', id);
    return this.http
      .get<any>(this.baseUrl + '/empPromotionIncrement/get-allEmpPromotionIncrement', {params})
      .pipe(
        map((data) => {
          this.cachedData = data; 
          return data;
        })
      );
  }

  getAllEmpPromotionIncrementApproveInfo(queryParams: any, departmentId:any, id: any){
    let params = new HttpParams({ fromObject: queryParams }); 
    params = params.append('departmentId', departmentId);
    params = params.append('id', id);
    return this.http.get<any>(this.baseUrl + '/empPromotionIncrement/get-AllEmpPromotionIncrementApproveInfo',{params});
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
  
  deleteEmpPromotionIncrement(id: number) {
    return this.http.delete(this.baseUrl + '/empPromotionIncrement/delete-EmpPromotionIncrement/'+id);
  }
  
}


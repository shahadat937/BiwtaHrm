import { Injectable } from '@angular/core';
import { EmpShiftAssign } from '../model/emp-shift-assign';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmpShiftAssignService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empShiftAssign: EmpShiftAssign;

  constructor(private http: HttpClient) { 
    this.empShiftAssign = new EmpShiftAssign();
  }
  
  getAll(): Observable<EmpShiftAssign[]> {
    if (this.cachedData.length > 0) {
      return of (this.cachedData);
    } else {
      return this.http
        .get<EmpShiftAssign[]>(this.baseUrl + '/empShiftAssign/get-allEmpShiftAssign')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }
  
  findById(id: number) {
    return this.http.get<EmpShiftAssign>(this.baseUrl + '/empShiftAssign/get-EmpShiftAssignById/' + id);
  }

  
  saveEmpShiftAssign(model: any) {
    return this.http.post(this.baseUrl + '/empShiftAssign/save-EmpShiftAssigns', model);
  }
  updateEmpShiftAssign(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empShiftAssign/update-EmpShiftAssigns/'+id, model);
  }
}

import { Injectable } from '@angular/core';
import { EmpRewardPunishment } from '../../promotion/model/emp-reward-punishment';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmpRewardPunishmentService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empRewardPunishment: EmpRewardPunishment;

  constructor(private http: HttpClient) { 
    this.empRewardPunishment = new EmpRewardPunishment();
  }

  getAll(): Observable<EmpRewardPunishment[]> {
    if (this.cachedData.length > 0) {
      return of (this.cachedData);
    } else {
      return this.http
        .get<EmpRewardPunishment[]>(this.baseUrl + '/empRewardPunishment/get-allEmpRewardPunishment')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }
  
  findById(id: number) {
    return this.http.get<EmpRewardPunishment>(this.baseUrl + '/empRewardPunishment/get-empRewardPunishmentById/' + id);
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpRewardPunishment[]>(this.baseUrl + '/empRewardPunishment/get-empRewardPunishmentByEmpId/' + id);
  }

  
  saveEmpRewardPunishment(model: any) {
  return this.http.post(this.baseUrl + '/empRewardPunishment/save-EmpRewardPunishment', model);
  }
  updateEmpRewardPunishment(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empRewardPunishment/update-EmpRewardPunishment/'+id, model);
  }
  
  deleteEmpRewardPunishment(id: number) {
    return this.http.delete(this.baseUrl + '/empRewardPunishment/update-EmpRewardPunishment/'+id);
  }
  
}


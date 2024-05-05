import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpTnsferPostingJoin } from '../model/emp-tnsfer-posting-join';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class EmpTnsferPostingJoinService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empTnsferPostingJoin: EmpTnsferPostingJoin;

  constructor(private http: HttpClient) {
    this.empTnsferPostingJoin = new EmpTnsferPostingJoin();
    
   }
  find(id: number) {
    return this.http.get<EmpTnsferPostingJoin>(this.baseUrl + '/division/get-divisionbyid/' + id);
  }
  getAll(): Observable<EmpTnsferPostingJoin[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<EmpTnsferPostingJoin[]>(this.baseUrl + '/EmpTnsferPostingJoin/get-AllEmpTnsferPostingJoin')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  getDivisionByCountryId(id:number): Observable<SelectedModel[]>{
    return this.http.get<SelectedModel[]>(this.baseUrl + '/division/get-divisionByCountryId/'+id).pipe(
      map((data) => {
        return data;
      })
    );; 
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/EmpTnsferPostingJoin/update-EmpTnsferPostingJoin/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/EmpTnsferPostingJoin/save-EmpTnsferPostingJoin', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/EmpTnsferPostingJoin/delete-EmpTnsferPostingJoin/'+id);
  }

}


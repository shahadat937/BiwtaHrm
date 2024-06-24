import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpTnsferPostingJoin } from '../model/emp-tnsfer-posting-join';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

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
    return this.http.get<EmpTnsferPostingJoin>(this.baseUrl + '/EmpTnsferPostingJoin/get-EmpTnsferPostingJoinbyid/' + id);
  }
  getempTnsferPostingJoinAll(): Observable<EmpTnsferPostingJoin[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<EmpTnsferPostingJoin[]>(this.baseUrl + '/EmpTnsferPostingJoin/get-EmpTnsferPostingJoin')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }


  update(id5: number,model: any) {
    return this.http.put(this.baseUrl + '/EmpTnsferPostingJoin/update-EmpTnsferPostingJoin/'+id5, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/EmpTnsferPostingJoin/save-EmpTnsferPostingJoin', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/EmpTnsferPostingJoin/delete-EmpTnsferPostingJoin/'+id);
  }

}


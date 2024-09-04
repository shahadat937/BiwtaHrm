import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BankBranch } from '../model/bank-branch';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable()
export class BankBranchService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  bankBranchs: BankBranch;

  constructor(private http: HttpClient) {
    this.bankBranchs = new BankBranch();
    
   }
  find(id: number) {
    return this.http.get<BankBranch>(this.baseUrl + '/BankBranch/get-bankBranchbyid/' + id);
  }
  getAll(): Observable<BankBranch[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<BankBranch[]>(this.baseUrl + '/BankBranch/get-bankBranch')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/BankBranch/update-bankBranch/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/BankBranch/save-bankBranch', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/BankBranch/delete-bankBranch/'+id);
  }

}

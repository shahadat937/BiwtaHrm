import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FinancialYear } from '../model/financial-year';

import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FinancialYearService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  FinancialYear: FinancialYear;
  
  constructor(private http: HttpClient) {
    this.FinancialYear = new FinancialYear();
   }
   find(id: number) {
    return this.http.get<FinancialYear>(this.baseUrl + '/financialYear/get-FinancialYearDetail/' + id);
  }
   //Coustom
  getSelectedFinancialYear(){
    return this.http.get<any>(this.baseUrl + '/financialYear/get-selectedFinancialYears');
  }

  getAll(): Observable<FinancialYear[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<FinancialYear[]>(this.baseUrl + '/financialYear/get-FinancialYears')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/financialYear/update-FinancialYear/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/financialYear/save-FinancialYear', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/financialYear/delete-FinancialYear/'+id);
  }

}

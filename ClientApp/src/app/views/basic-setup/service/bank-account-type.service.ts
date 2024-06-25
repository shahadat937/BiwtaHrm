import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BankAccountType } from '../model/bank-account-type';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BankAccountTypeService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  bankAccountTypes: BankAccountType;
  constructor(private http: HttpClient) {
    this.bankAccountTypes = new BankAccountType();
  }
  find(id: number) {
    return this.http.get<BankAccountType>(
      this.baseUrl + '/BankAccountType/get-bankAccountTypebyid/' + id
    );
  }

  getAll(): Observable<BankAccountType[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<BankAccountType[]>(this.baseUrl + '/BankAccountType/get-bankAccountType')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/BankAccountType/update-bankAccountType/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/BankAccountType/save-bankAccountType', model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + '/BankAccountType/delete-bankAccountType/' + id);
  }
}

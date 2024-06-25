import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Bank } from '../model/bank';
import { HttpClient } from '@angular/common/http';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BankService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  banks: Bank;
  constructor(private http: HttpClient) {
    this.banks = new Bank();
  }
  find(id: number) {
    return this.http.get<Bank>(
      this.baseUrl + '/Bank/get-bankbyid/' + id
    );
  }
  selectGetBank() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/Bank/get-selectedbank'
    );
  }
  getAll(): Observable<Bank[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Bank[]>(this.baseUrl + '/Bank/get-bank')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/Bank/update-bank/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Bank/save-bank', model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + '/Bank/delete-bank/' + id);
  }
}

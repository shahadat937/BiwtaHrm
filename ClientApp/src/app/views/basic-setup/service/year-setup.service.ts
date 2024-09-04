import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { YearSetup } from '../model/yearsetup';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map } from 'rxjs';

@Injectable()
export class YearSetupService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  years: YearSetup;
  constructor(private http: HttpClient) {
    this.years = new YearSetup();
   }

   find(id: number) {
    return this.http.get<YearSetup>(this.baseUrl + '/year/get-yearDetail/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<YearSetup[]> {
    if (this.cachedData.length > 0) {
      return of(this.cachedData);
    } else {
      return this.http
        .get<YearSetup[]>(this.baseUrl + '/year/get-year')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/year/update-year/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/year/save-year', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/year/delete-year/'+id);
  }
}

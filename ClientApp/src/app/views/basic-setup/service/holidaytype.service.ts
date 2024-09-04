import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HolidayType } from '../model/holidaytype';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map } from 'rxjs';

@Injectable()

export class HolidaytypeService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  holidayType: HolidayType;
  constructor(private http: HttpClient) {
    this.holidayType = new HolidayType();
   }

   find(id: number) {
    return this.http.get<HolidayType>(this.baseUrl + '/holidayType/get-holidayTypeDetail/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<HolidayType[]> {
    if (this.cachedData.length > 0) {
      return of(this.cachedData);
    } else {
      return this.http
        .get<HolidayType[]>(this.baseUrl + '/holidayType/get-holidayType')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/holidayType/update-holidayType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/holidayType/save-holidayType', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/holidayType/delete-holidayType/'+id);
  }
}
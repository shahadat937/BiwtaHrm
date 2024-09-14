import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, model } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HolidayModel } from '../models/holiday-model';

@Injectable()
export class HolidaySetupService {
  baseUrl: string;
  cachedData: HolidayModel[] = [];
  model: HolidayModel;
  constructor(
    private http: HttpClient
  ) { 
    this.baseUrl = environment.apiUrl;
    this.model = new HolidayModel();
  }

  getHolidayTypeOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/holidayType/get-selectedholidayTypes");
  }

   getYear():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/year/get-selectedyears");
   }

   getHolidays(): Observable<HolidayModel[]> {
    if(this.cachedData.length>0) {
      return of (this.cachedData);
    }

    return this.http.get<HolidayModel[]>(this.baseUrl+"/holidays/get-Holidays")
        .pipe(
          map(data=> {
            this.cachedData = data;
            return data;
          })
        );
   }

   createHoliday(element:any):Observable<any> {

    let params = new HttpParams();
    params = params.set('holidayFrom', element.holidayFrom);
    params = params.set('holidayTo', element.holidayTo);
    
    delete element['holidayFrom'];
    delete element['holidayTo'];

    return this.http.post<any>(this.baseUrl+"/holidays/save-Holidays",element, {params:params});
   }

   updateHoliday(element:any):Observable<any> {

    delete element['yearName'];
    delete element['holidayTypeName'];
    delete element['officeId'];
    delete element['officeName'];
    delete element['officeBranchName'];
    delete element['officeBranchId'];
    delete element['menuPosition'];

    console.log(element);
    return this.http.put<any>(this.baseUrl+"/holidays/update-Holidays",element);
   }

   deleteHoliday(id:number): Observable<any> {
    return this.http.delete<any>(this.baseUrl+`/holidays/delete-Holidays/${id}`);
   }

   deleteHolidayByGroupId(groupId:number):Observable<any> {
    return this.http.delete<any>(this.baseUrl+`/holidays/delete-HolidaysByGroupId/${groupId}`);
   }
}

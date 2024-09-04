import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class WorkdayService {

  baseUrl:string = environment.apiUrl;
  constructor(private http: HttpClient) {

   }

  
   getYear():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/year/get-selectedyears");
   }

   getDay(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/weekDay/get-selectedWeekDays");
   }

   getWorkday(yearId:number): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+`/workday/get-SelectedWorkdayByYear/${yearId}`)
   }

   addWorkday(element:any):Observable<any> {
    return this.http.post<any>(this.baseUrl+"/workday/save-Workday",element);
   }

   deleteWorkday(id:number): Observable<any> {
    return this.http.delete<any>(this.baseUrl+`/workday/delete-Workday/${id}`);
   }
}

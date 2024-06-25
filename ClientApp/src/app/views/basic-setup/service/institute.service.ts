import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Institute } from '../model/institute';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InstituteService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  institutes: Institute;
  constructor(private http: HttpClient) {
    this.institutes = new Institute();
   }
   getById(id: number) {
    return this.http.get<Institute>(this.baseUrl + '/Institute/get-institutebyid/' + id);
  }
  // getAll():Observable<Institute[]> {
  //   return this.http.get<Institute[]>(this.baseUrl + '/Institute/get-institute');
  // }
  getAll(): Observable<Institute[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Institute[]>(this.baseUrl + '/Institute/get-institute')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Institute/update-institute/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Institute/save-institute', model);
  }
  
  
  delete(id: number) { 
    return this.http.delete(this.baseUrl + '/Institute/delete-institute/'+id);
  }
  
  
  }
  
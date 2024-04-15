import { Result } from './../model/result';
import { Injectable, inject } from '@angular/core';

import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResultService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  result: Result;
  constructor(private http: HttpClient) {
    this.result = new Result();
   }
  find(id: number) {
    return this.http.get<Result>(this.baseUrl + '/Result/get-resultDetail/' + id);
  }
  // getAll():Observable<Result[]> {
  //   return this.http.get<Result[]>(this.baseUrl + '/Result/get-result');
  // }
  getAll(): Observable<Result[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Result[]>(this.baseUrl + '/Result/get-result')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Result/update-result/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Result/save-result', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Result/delete-result/'+id);
  }

}
import { Result } from './../model/result';
import { Injectable, inject } from '@angular/core';

import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResultService {
  baseUrl = environment.apiUrl;
  result: Result;
  constructor(private http: HttpClient) {
    this.result = new Result();
   }
  find(id: number) {
    return this.http.get<Result>(this.baseUrl + '/Result/get-bloodGroupDetail/' + id);
  }
  getAll():Observable<Result[]> {
    return this.http.get<Result[]>(this.baseUrl + '/Result/get-result');
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
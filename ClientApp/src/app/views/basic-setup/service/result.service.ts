import { Injectable, inject } from '@angular/core';
import { Result } from '../model/result';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';

import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResultService {
  //baseUrl = environment.apiUrl;
  apiUrl = 'http://localhost:25971';

  constructor(private http: HttpClient) {}

  getAllResult() {
    //console.log('getAllEmployee', localStorage.getItem('token'));
    return this.http.get<Result[]>(this.apiUrl + '/api/hrm/Result/get-result');
    //return this.http.get<Result[]>(this.baseUrl + '/Result/get-result');
  }
}

import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Thana}  from  './../model/thana'
import { Observable, map, of } from 'rxjs';

import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable()
export class ThanaService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  thanas: Thana;
  constructor(private http: HttpClient) {
    this.thanas=new Thana();
   }

  

  getById(id: number) {
    return this.http.get<Thana>(this.baseUrl + '/thana/get-thanabyid/' + id);
  }
  getthanaNamesByUpazilaId(id:number): Observable<SelectedModel[]>{
    return this.http.get<SelectedModel[]>(this.baseUrl + '/thana/get-thanaByUpazilaId/'+id).pipe(
      map((data) => {
        return data;
      })
    );; 
  }
  // getAll():Observable<Thana[]> {
  //   return this.http.get<Thana[]>(this.baseUrl + '/thana/get-thana');
  // }
  getAll(): Observable<Thana[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Thana[]>(this.baseUrl + '/thana/get-thana')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/thana/update-thana/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/thana/save-thana', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/thana/delete-thana/'+id);
  }

}

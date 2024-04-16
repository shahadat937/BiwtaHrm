
import { Scale } from 'chart.js';
import { SelectionModel } from '@angular/cdk/collections';

import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';
import { Grade } from '../model/Grade';
import { GradeViewModel } from '../model/GradeViewModel';

@Injectable({
  providedIn: 'root'
})
export class GradeService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  grades: Grade;

  constructor(private http: HttpClient) {
    this.grades = new Grade();
  }

  find(id: number) {
    return this.http.get<Grade>(this.baseUrl + '/grade/get-GradeDetail/' + id);
  }
  //custom:
  selectModelGrade(){
    return this.http.get<GradeViewModel[]>(this.baseUrl + '/grade/get-selectedGrade');
  }

  getAll(): Observable<Grade[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Grade[]>(this.baseUrl + '/grade_cls_type_Vw/get-Grade_cls_type_Vw')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/grade/update-grade/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/grade/save-grade', model);
  }
  // delete(id: number) {
  //   return this.http.delete(this.baseUrl + '/grade/delete-grade/' + id);
  // }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/grade/delete-grade/'+id);
  }
}

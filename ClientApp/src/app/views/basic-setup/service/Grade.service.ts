
import { Scale } from 'chart.js';
import { SelectionModel } from '@angular/cdk/collections';

import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Grade } from '../model/Grade';
import { GradeViewModel } from '../model/GradeViewModel';

@Injectable({
  providedIn: 'root'
})
export class GradeService {
  baseUrl = environment.apiUrl;
  grades: Grade;
  selection = new SelectionModel<Grade>(true, []);
  constructor(private http: HttpClient) {
    this.grades = new Grade();
  }

  find(id: number) {
    return this.http.get<Grade>(this.baseUrl + '/grade/get-GradeDetail/' + id);
  }
  // getGrateScale(id: Number): Observable<Grade[]> {
  //   return this.http.get<Grade[]>(`${this.baseUrl}/scaleGradeView/get-scaleGradeView/${id}`);
  // }
  //custom:
  selectModelGrade(){
    return this.http.get<GradeViewModel[]>(this.baseUrl + '/grade/get-selectedGrade');
  }
  //Normal
  getAll(): Observable<Grade[]> {
    return this.http.get<Grade[]>(this.baseUrl + '/grade_cls_type_Vw/get-Grade_cls_type_Vw');
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/grade/update-grade/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/grade/save-grade', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/grade/delete-grade/' + id);
  }

}

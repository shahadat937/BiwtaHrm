
import { SelectionModel } from '@angular/cdk/collections';

import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GradeClass } from '../model/GradeClass';
import { GradeType } from '../model/GradeType';
import { GradeTypeViewModel } from '../model/GradeTypeViewModel';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GradeTypeService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  gradeTypes: GradeType;

  constructor(private http: HttpClient) {
    this.gradeTypes = new GradeType();
   }
//Custom
  getSelectGradeType(){
    return this.http.get<GradeTypeViewModel[]>(this.baseUrl + '/grade-type/get-selectedGradeTypes');
  }

  getAll(): Observable<GradeType[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<GradeType[]>(this.baseUrl + '/grade-type/get-selectedGradeTypes')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/grade-type/update-gradeType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/grade-type/save-gradeType', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/grade-type/delete-gradeType/'+id);
  }

}

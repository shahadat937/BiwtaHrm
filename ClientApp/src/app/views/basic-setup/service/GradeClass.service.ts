
import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GradeClass } from '../model/GradeClass';
import { GradeClassViewModel } from '../model/GradeClassViewModel';

import { Observable, map, of } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class GradeClassService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  gradeClass: GradeClass;
  
  constructor(private http: HttpClient) {
    this.gradeClass = new GradeClass();
   }
   find(id: number) {
    return this.http.get<GradeClass>(this.baseUrl + '/grade-class/get-gradeClassDetail/' + id);
  }
   //Coustom
  getSelectedGradeClass(){
    return this.http.get<GradeClassViewModel[]>(this.baseUrl + '/grade-class/get-selectedGradeClasss');
  }

  getAll(): Observable<GradeClass[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<GradeClass[]>(this.baseUrl + '/grade-class/get-gradeClass')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/grade-class/update-gradeClass/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/grade-class/save-gradeClass', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/grade-class/delete-gradeClass/'+id);
  }

}

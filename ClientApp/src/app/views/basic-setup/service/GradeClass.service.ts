
import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GradeClass } from '../model/GradeClass';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class GradeClassService {
  baseUrl = environment.apiUrl;
  gradeClass: GradeClass;
  //selection = new SelectionModel<Grade>(true, []);
  constructor(private http: HttpClient) {
    this.gradeClass = new GradeClass();
   }

  getSelectedGradeClass():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + '/grade-class/get-selectedGradeClasss');
  }

  getGradeClass() {
    return this.http.get<any[]>(this.baseUrl + '/grade-class/get-gradeClass');
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

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ExamType } from '../model/exam-type';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExamTypeService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  examTypes: ExamType;
  constructor(private http: HttpClient) {
    this.examTypes = new ExamType();
   }
  find(id: number) {
    return this.http.get<ExamType>(this.baseUrl + '/examType/get-ExamTypebyid/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<ExamType[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<ExamType[]>(this.baseUrl + '/examType/get-ExamType')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/examType/update-ExamType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/examType/save-ExamType', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/examType/delete-ExamType/'+id);
  }

}
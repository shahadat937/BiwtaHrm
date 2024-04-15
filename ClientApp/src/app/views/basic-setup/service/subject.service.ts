import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Subject}  from  './../model/subject'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  baseUrl = environment.apiUrl;
  subjects: Subject;
  constructor(private http: HttpClient) {
    this.subjects=new Subject();
   }


  getById(id: number) {
    return this.http.get<Subject>(this.baseUrl + '/Subject/get-subjectbyid/' + id);
  }
  getAll():Observable<Subject[]> {
    return this.http.get<Subject[]>(this.baseUrl + '/Subject/get-subject');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Subject/update-subject/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Subject/save-subject', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Subject/delete-subject/'+id);
  }

}

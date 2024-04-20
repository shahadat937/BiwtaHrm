import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Subject } from './../model/subject';
@Injectable({
  providedIn: 'root',
})
export class SubjectService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  subjects: Subject;
  constructor(private http: HttpClient) {
    this.subjects = new Subject();
  }

  getById(id: number) {
    return this.http.get<Subject>(
      this.baseUrl + '/Subject/get-subjectbyid/' + id
    );
  }
  // getAll(): Observable<Subject[]> {
  //   return this.http.get<Subject[]>(this.baseUrl + '/Subject/get-subject');
  // }
  getAll(): Observable<Subject[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Subject[]>(this.baseUrl + '/Subject/get-subject')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/Subject/update-subject/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Subject/save-subject', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/Subject/delete-subject/' + id);
  }
}

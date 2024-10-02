import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { CourseDuration } from '../model/course-duration';

@Injectable({
  providedIn: 'root'
})
export class CourseDurationService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  courseDuration: CourseDuration;

  constructor(private http: HttpClient) {
    this.courseDuration = new CourseDuration();
   }

  find(id: number) {
    return this.http.get<CourseDuration>(this.baseUrl + '/courseDuration/get-CourseDurationDetail/' + id);
  }

  getAll(): Observable<CourseDuration[]> {
    if (this.cachedData.length > 0) {
      return of(this.cachedData);
    } else {
      return this.http
        .get<CourseDuration[]>(this.baseUrl + '/courseDuration/get-CourseDuration')
        .pipe(
          map((data) => {
            this.cachedData = data;
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/courseDuration/update-CourseDuration/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/courseDuration/save-CourseDuration', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/courseDuration/delete-CourseDuration/'+id);
  }
  getSelectedCourseDuration(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/courseDuration/get-selectedCourseDurations')
  }
   
}


import { Injectable } from '@angular/core';
import { Gender } from '../model/gender';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';

@Injectable()
export class GenderService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  gender: Gender;
  constructor(private http: HttpClient) {
    this.gender = new Gender();
   }

   
   getById(id: number):Observable<Gender> {
    return this.http.get<Gender>(this.baseUrl + '/gender/get-genderById/' + id);
  }
  // getAll():Observable<Gender[]> {
  //   return this.http.get<Gender[]>(this.baseUrl + '/gender/get-gender');
  // }
  getAll(): Observable<Gender[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Gender[]>(this.baseUrl + '/gender/get-gender')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: Gender): Observable<Gender> {
    return this.http.put<Gender>(this.baseUrl + '/gender/update-gender/'+id, model);
  }
  submit(model: Gender): Observable<Gender> {
    return this.http.post<Gender>(this.baseUrl + '/gender/save-gender', model);
  }
  delete(id:number): Observable<Gender>{
    return this.http.delete<Gender>(this.baseUrl + '/gender/delete-gender/'+id);
  }
}

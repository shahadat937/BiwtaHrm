import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';
import { Occupation } from '../model/Occupation';
@Injectable()
export class OccupationService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  occupations: Occupation;
  constructor(private http: HttpClient) {
    this.occupations = new Occupation();
   }
  find(id: number) {
    return this.http.get<Occupation>(this.baseUrl + '/Occupation/get-occupationDetail/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<Occupation[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Occupation[]>(this.baseUrl + '/Occupation/get-occupation')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Occupation/update-occupation/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Occupation/save-occupation', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Occupation/delete-occupation/'+id);
  }

}

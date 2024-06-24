import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MaritalStatus } from '../model/marital-status';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable()
export class MaritalStatusService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  maritalStatus: MaritalStatus;
  constructor(private http: HttpClient) {
    this.maritalStatus = new MaritalStatus();
   }
  find(id: number) {
    return this.http.get<MaritalStatus>(this.baseUrl + '/marital-status/get-maritalStatusById/' + id);
  }
  // getAll():Observable<MaritalStatus[]> {
  //   return this.http.get<MaritalStatus[]>(this.baseUrl + '/marital-status/get-maritalStatus');
  // }
  getAll(): Observable<MaritalStatus[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<MaritalStatus[]>(this.baseUrl + '/marital-status/get-maritalStatus')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/marital-status/update-maritalStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/marital-status/save-maritalStatus', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/marital-status/delete-maritalStatus/'+id);
  }
}

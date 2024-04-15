import { Injectable } from '@angular/core';
import { ChildStatus } from '../model/child-status';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable()
export class ChildStatusService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  childStatus: ChildStatus;
  constructor(private http: HttpClient) {
    this.childStatus = new ChildStatus();
   }
  find(id: number) {
    return this.http.get<ChildStatus>(this.baseUrl + '/childStatus/get-ChildStatusById/' + id);
  }
  // getAll():Observable<ChildStatus[]> {
  //   return this.http.get<ChildStatus[]>(this.baseUrl + '/childStatus/get-ChildStatus');
  // }
  getAll(): Observable<ChildStatus[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<ChildStatus[]>(this.baseUrl + '/childStatus/get-ChildStatus')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/childStatus/update-ChildStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/childStatus/save-ChildStatus', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/childStatus/delete-ChildStatus/'+id);
  }
}

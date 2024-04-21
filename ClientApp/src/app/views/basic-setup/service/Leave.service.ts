import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Leave } from '../model/Leave';
import { Observable, map, of } from 'rxjs';
@Injectable()
export class LeaveService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  leaves: Leave;
  constructor(private http: HttpClient) {
    this.leaves = new Leave();
   }
  find(id: number) {
    return this.http.get<Leave>(this.baseUrl + '/Leave/get-LeaveDetail/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<Leave[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Leave[]>(this.baseUrl + '/Leave/get-Leave')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Leave/update-Leave/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Leave/save-Leave', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Leave/delete-Leave/'+id);
  }

}

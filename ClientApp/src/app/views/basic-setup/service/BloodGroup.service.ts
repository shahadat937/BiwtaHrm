import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BloodGroup } from '../model/BloodGroup';
import { Observable, map, of } from 'rxjs';
@Injectable()
export class BloodGroupService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  bloodGroups: BloodGroup;
  constructor(private http: HttpClient) {
    this.bloodGroups = new BloodGroup();
   }
  find(id: number) {
    return this.http.get<BloodGroup>(this.baseUrl + '/blood-group/get-bloodGroupDetail/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<BloodGroup[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/blood-group/update-bloodGroup/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/blood-group/save-bloodGroup', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/blood-group/delete-bloodGroup/'+id);
  }

}

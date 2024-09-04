import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {District}  from  './../model/district' 
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Observable, map, of } from 'rxjs';

@Injectable()
export class DistrictService  {
baseUrl = environment.apiUrl;
districts: District;
  cachedData: any[] = []; 
  constructor(private http: HttpClient) {
    this.districts = new District();
   }

getDivision(): Observable<SelectedModel[]>{
  return this.http.get<SelectedModel[]>(this.baseUrl + '/division/get-selecteddivision') .pipe(
    map((data) => {
      return data;
    })
  );;
}

getDistrictByDivisionId(id:number): Observable<SelectedModel[]>{
  return this.http.get<SelectedModel[]>(this.baseUrl + '/district/get-districByDivisionId/'+id).pipe(
    map((data) => {
      return data;
    })
  );; 
}

  find(id: number) {
    return this.http.get<District>(this.baseUrl + '/district/get-districtbyid/' + id);
  }
  // getAll():Observable<District[]> {
  //   return this.http.get<District[]>(this.baseUrl + '/district/get-district');
  // }
  getAll(): Observable<District[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<District[]>(this.baseUrl + '/district/get-district')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/district/update-district/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/district/save-district', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/district/delete-district/'+id);
  }

}

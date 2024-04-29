import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Upazila}  from  './../model/upazila'
import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class UapzilaService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  upazilas: Upazila;
  constructor(private http: HttpClient) {
    this.upazilas=new Upazila();
   }


   

  find(id: number) {
    return this.http.get<Upazila>(this.baseUrl + '/upazila/get-upazilabyid/' + id);
  }
  // getAll():Observable<Upazila[]> {
  //   return this.http.get<Upazila[]>(this.baseUrl + '/upazila/get-upazila');
  // }
  getAll(): Observable<Upazila[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Upazila[]>(this.baseUrl + '/upazila/get-upazila')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  getUpapzilaByDistrictId(id:number): Observable<SelectedModel[]>{
    return this.http.get<SelectedModel[]>(this.baseUrl + '/upazila/get-upazilaByDistrictId/'+id).pipe(
      map((data) => {
        return data;
      })
    );; 
  }

  getdistrict(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/district/get-selecteddistrict')
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/upazila/update-upazila/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/upazila/save-upazila', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/upazila/delete-upazila/'+id);
  }

}

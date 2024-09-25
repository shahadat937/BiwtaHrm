import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponsibilityType } from '../model/responsibility-type';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class ResponsibilityTypeService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  responsibilityType: ResponsibilityType;
  constructor(private http: HttpClient) {
    this.responsibilityType = new ResponsibilityType();
   }
  find(id: number) {
    return this.http.get<ResponsibilityType>(this.baseUrl + '/ResponsibilityType/get-ResponsibilityTypeDetail/' + id);
  }
  // getAll():Observable<ResponsibilityType[]> {
  //   return this.http.get<ResponsibilityType[]>(this.baseUrl + '/ResponsibilityType/get-ResponsibilityType');
  // }
  getAll(): Observable<ResponsibilityType[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<ResponsibilityType[]>(this.baseUrl + '/ResponsibilityType/get-ResponsibilityType')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  getSelectedResponsibilityType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/ResponsibilityType/get-selectedResponsibilityTypes')
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/ResponsibilityType/update-ResponsibilityType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/ResponsibilityType/save-ResponsibilityType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/ResponsibilityType/delete-ResponsibilityType/'+id);
  }

}

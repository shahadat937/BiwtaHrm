import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {District}  from  './../model/district'
import { Observable } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DistrictService  {
baseUrl = environment.apiUrl;
districts: District;
  constructor(private http: HttpClient) {
    this.districts = new District();
   }

getDivision(){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/division/get-selecteddivision');
}


  find(id: number) {
    return this.http.get<District>(this.baseUrl + '/district/get-districtbyid/' + id);
  }
  getAll():Observable<District[]> {
    return this.http.get<District[]>(this.baseUrl + '/district/get-district');
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


import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RetiredReason } from '../model/retired-reason';

import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable()
export class RetiredReasonService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  RetiredReason: RetiredReason;
  
  constructor(private http: HttpClient) {
    this.RetiredReason = new RetiredReason();
   }
   find(id: number) {
    return this.http.get<RetiredReason>(this.baseUrl + '/retiredReason/get-RetiredReasonDetail/' + id);
  }
   //Coustom
  getSelectedRetiredReason(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/retiredReason/get-selectedRetiredReasons');
  }

  getAll(): Observable<RetiredReason[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<RetiredReason[]>(this.baseUrl + '/retiredReason/get-RetiredReasons')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/retiredReason/update-RetiredReason/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/retiredReason/save-RetiredReason', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/retiredReason/delete-RetiredReason/'+id);
  }

}

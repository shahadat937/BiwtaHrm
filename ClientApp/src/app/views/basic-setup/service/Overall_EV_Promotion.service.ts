import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Overall_EV_Promotion } from '../model/Overall_EV_Promotion';
import { Observable, map, of } from 'rxjs';
@Injectable()
export class Overall_EV_PromotionService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  overall_EV_Promotions: Overall_EV_Promotion;
  constructor(private http: HttpClient) {
    this.overall_EV_Promotions = new Overall_EV_Promotion();
   }
  find(id: number) {
    return this.http.get<Overall_EV_Promotion>(this.baseUrl + '/overallEVPromotion/get-overallEVPromotionDetail/' + id);
  }
  getAll(): Observable<Overall_EV_Promotion[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Overall_EV_Promotion[]>(this.baseUrl + '/overallEVPromotion/get-overallEVPromotion')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/overallEVPromotion/update-overallEVPromotion/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/overallEVPromotion/save-overallEVPromotion', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/overallEVPromotion/delete-overallEVPromotion/'+id);
  }

}

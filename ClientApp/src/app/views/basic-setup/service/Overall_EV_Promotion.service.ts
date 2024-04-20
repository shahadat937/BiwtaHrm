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
    return this.http.get<Overall_EV_Promotion>(this.baseUrl + '/Overall_EV_Promotion/get-overall_EV_PromotionDetail/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<Overall_EV_Promotion[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Overall_EV_Promotion[]>(this.baseUrl + '/Overall_EV_Promotion/get-overall_EV_Promotion')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Overall_EV_Promotion/update-overall_EV_Promotion/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Overall_EV_Promotion/save-overall_EV_Promotion', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Overall_EV_Promotion/delete-overall_EV_Promotion/'+id);
  }

}

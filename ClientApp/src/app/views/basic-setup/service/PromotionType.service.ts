import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PromotionType } from '../model/PromotionType';
@Injectable({
  providedIn: 'root'
})
export class promotionTypeService {
  baseUrl = environment.apiUrl;
  promotionTypes: PromotionType;
  constructor(private http: HttpClient) {
    this.promotionTypes = new PromotionType();
   }
  find(id: number) {
    return this.http.get<PromotionType>(this.baseUrl + '/promotionType/get-promotionTypeDetail/' + id);
  }
  getAll():Observable<PromotionType[]> {
    return this.http.get<PromotionType[]>(this.baseUrl + '/promotionType/get-promotionType');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/promotionType/update-promotionType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/promotionType/save-promotionType', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/promotionType/delete-promotionType/'+id);
  }

}

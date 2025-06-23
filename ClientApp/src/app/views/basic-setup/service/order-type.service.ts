import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OrderType } from '../model/order-type';

import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class OrderTypeService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  OrderType: OrderType;
  
  constructor(private http: HttpClient) {
    this.OrderType = new OrderType();
   }
   find(id: number) {
    return this.http.get<OrderType>(this.baseUrl + '/orderType/get-OrderTypeDetail/' + id);
  }
   //Coustom
  getSelectedOrderType(){
    return this.http.get<any>(this.baseUrl + '/orderType/get-selectedOrderTypes');
  }

  getAll(): Observable<OrderType[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<OrderType[]>(this.baseUrl + '/orderType/get-OrderTypes')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/orderType/update-OrderType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/orderType/save-OrderType', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/orderType/delete-OrderType/'+id);
  }

}

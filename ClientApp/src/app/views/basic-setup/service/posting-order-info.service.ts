import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PostingOrderInfo } from '../model/posting-order-info';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable()
export class PostingOrderInfoService {
  [x: string]: any;

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  postingOrderInfos: PostingOrderInfo;

  constructor(private http: HttpClient) {
    this.postingOrderInfos = new PostingOrderInfo();
    
   }
  find(id: number) {
    return this.http.get<PostingOrderInfo>(this.baseUrl + '/postingOrderInfo/get-PostingOrderInfobyid/' + id);
  }
  getAll(): Observable<PostingOrderInfo[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<PostingOrderInfo[]>(this.baseUrl + '/postingOrderInfo/get-PostingOrderInfo')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }


  update(id2: number,model: any) {
    return this.http.put(this.baseUrl + '/postingOrderInfo/update-PostingOrderInfo/'+id2, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/postingOrderInfo/save-PostingOrderInfo', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/postingOrderInfo/delete-PostingOrderInfo/'+id);
  }

}

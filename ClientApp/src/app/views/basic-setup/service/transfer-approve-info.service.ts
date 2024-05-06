import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TransferApproveInfo } from '../model/transfer-approve-info';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class TransferApproveInfoService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  transferApproveInfos: TransferApproveInfo;

  constructor(private http: HttpClient) {
    this.transferApproveInfos = new TransferApproveInfo();
    
   }
  find(id: number) {
    return this.http.get<TransferApproveInfo>(this.baseUrl + '/division/get-divisionbyid/' + id);
  }
  getTransferApproveInfoAll(): Observable<TransferApproveInfo[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<TransferApproveInfo[]>(this.baseUrl + '/transferApproveInfo/get-transferApproveInfo')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  // getDivisionByCountryId(id:number): Observable<SelectedModel[]>{
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/division/get-divisionByCountryId/'+id).pipe(
  //     map((data) => {
  //       return data;
  //     })
  //   );; 
  // }





  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/transferApproveInfo/update-transferApproveInfo/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/transferApproveInfo/save-transferApproveInfo', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/transferApproveInfo/delete-transferApproveInfo/'+id);
  }

}

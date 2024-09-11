import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WidgetsService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  
  
  getAllWidgetsInfo(){
    return this.http.get<any[]>(this.baseUrl + '/widgets/get-widgets');
  }
  
  getAllUsersWidgetsInfo(){
    return this.http.get<any[]>(this.baseUrl + '/widgets/get-userWidgets');
  }
  
  getAllGendersWidgetsInfo(){
    return this.http.get<any[]>(this.baseUrl + '/widgets/get-gendersChart');
  }
  
  getAllFieldUnfieldWidgetsInfo(){
    return this.http.get<any[]>(this.baseUrl + '/widgets/get-fieldUnfieldChart');
  }

  getAllTransferWidgetsInfo(){
    return this.http.get<any[]>(this.baseUrl + '/widgets/get-transferWidgets');
  }
  
  getAllPromotionWidgetsInfo(){
    return this.http.get<any[]>(this.baseUrl + '/widgets/get-promotionWidgets');
  }

}

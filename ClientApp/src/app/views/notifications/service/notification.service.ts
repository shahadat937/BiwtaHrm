import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { UserNotification } from '../models/user-notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUserNotification(queryParams: any, empId:any){
    let params = new HttpParams({ fromObject: queryParams }); 
    params = params.append('empId', empId);
    return this.http.get<any>(`${this.baseUrl}/notification/get-notificationForUser`, { params });
  }

  getNoticeList(queryParams: any, empId:any){
    let params = new HttpParams({ fromObject: queryParams });
    params = params.append('empId', empId);
    return this.http.get<any>(`${this.baseUrl}/notification/get-noticeList`, { params });
  }

  submit(model: any) {
    return this.http.post(this.baseUrl + '/notification/save-notification', model);
  }
  
  updateNotificationStatus(model: any) {
    return this.http.post(this.baseUrl + '/notification/update-notificationStatus', model);
  }

}

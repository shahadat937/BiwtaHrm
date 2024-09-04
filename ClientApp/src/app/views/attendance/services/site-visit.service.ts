import { Injectable } from '@angular/core';
import { SiteVisitModel } from '../models/site-visit-model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable()
export class SiteVisitService {
  baseUrl: string;
  cachedData: SiteVisitModel[] = [];
  model: SiteVisitModel;

  constructor(
    private http: HttpClient
  ) { 
    this.baseUrl = environment.apiUrl;
    this.model = new SiteVisitModel();
  }

  getSiteVisitAll():Observable<SiteVisitModel[]> {
    if(this.cachedData.length>0) {
      return of (this.cachedData);
    } else {
      return this.http.get<SiteVisitModel[]>(this.baseUrl+"/siteVisit/get-SiteVisit")
      .pipe (
        map(data=> {
          this.cachedData = data;
          return data;
        })
      );
    }
  }

  getSiteVisitById(id:number):Observable<SiteVisitModel> {
    return this.http.get<SiteVisitModel>(this.baseUrl+`/siteVisit/get-sitevisitbyid/${id}`);
  }

  approveSiteVisit(id:number):Observable<any> {
    return this.http.put<any>(this.baseUrl+`/siteVisit/approve-SiteVisit/${id}`,{});
  }

  declineSiteVisit(id:number):Observable<any> {
    return this.http.put<any>(this.baseUrl+`/siteVisit/decline-SiteVisit/${id}`,{});
  }

  delete(id:number):Observable<any> {
    return this.http.delete<any>(this.baseUrl+`/siteVisit/delete-siteVisit/${id}`);
  }

  getEmpOption():Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/empBasicInfo/get-SelectedEmpBasicInfo");
  }

  submit(model:any):Observable<any> {
    return this.http.post<any>(this.baseUrl+"/siteVisit/save-SiteVisit",model);
  }

  update(model:any):Observable<any> {
    return this.http.put<any>(this.baseUrl+`/siteVisit/update-SiteVisit/${model.siteVisitId}`,model);
  }
}

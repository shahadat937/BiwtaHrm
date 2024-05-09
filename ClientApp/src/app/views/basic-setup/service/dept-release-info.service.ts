import { HttpClient } from '@angular/common/http';
import { DeptReleaseInfo } from './../model/dept-release-info';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeptReleaseInfoService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  deptReleaseInfo: DeptReleaseInfo;
  constructor(private http: HttpClient) {
    this.deptReleaseInfo = new DeptReleaseInfo();
   }
  find(id: number) {
    return this.http.get<DeptReleaseInfo>(this.baseUrl + '/depReleaseInfo/get-depReleaseInfobyid/' + id);
  }
  getdeptReleaseInfoAll(): Observable<DeptReleaseInfo[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<DeptReleaseInfo[]>(this.baseUrl + '/depReleaseInfo/get-depReleaseInfo')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/depReleaseInfo/update-depReleaseInfo/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/depReleaseInfo/save-depReleaseInfo', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/depReleaseInfo/delete-depReleaseInfo/'+id);
  }

}

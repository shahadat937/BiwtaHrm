import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BasicInfoModule } from '../model/basic-info.module';
import { Observable, of, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ManageEmployeeService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  basicInfoModule: BasicInfoModule;
  constructor(private http: HttpClient) {
    this.basicInfoModule = new BasicInfoModule();
   }

  //  getAll(): Observable<BasicInfoModule[]> {
  //   if (this.cachedData.length > 0) {
  //     return of (this.cachedData);
  //   } else {
  //     return this.http
  //       .get<BasicInfoModule[]>(this.baseUrl + '/empBasicInfo/get-allEmpBasicInfo')
  //       .pipe(
  //         map((data) => {
  //           this.cachedData = data;
  //           return data;
  //         })
  //       );
  //   }
  // }

  getAll(): Observable<BasicInfoModule[]> {
        return this.http.get<BasicInfoModule[]>(this.baseUrl + '/empBasicInfo/get-allEmpBasicInfo');
    }

  
  getAllPagination(queryParams: any): Observable<any> {
      const params = new HttpParams({ fromObject: queryParams });
      return this.http.get<any>(`${this.baseUrl}/empBasicInfo/get-allEmpBasicInfo`, { params });
  }

  getEmpBasicInfoByEmpId(id: number) {
    return this.http.get<BasicInfoModule>(this.baseUrl + '/empBasicInfo/get-EmpBasicInfosById/' + id);
  }

}

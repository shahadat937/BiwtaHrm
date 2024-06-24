import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BasicInfoModule } from '../model/basic-info.module';
import { Observable, of, map } from 'rxjs';
import { EmployeesModule } from '../model/employees.module';
import { HttpClient } from '@angular/common/http';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class EmpBasicInfoService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  basicInfo: BasicInfoModule;

  constructor(private http: HttpClient) { 
    this.basicInfo = new BasicInfoModule();
  }
  
  getAll(): Observable<BasicInfoModule[]> {
    if (this.cachedData.length > 0) {
      return of (this.cachedData);
    } else {
      return this.http
        .get<BasicInfoModule[]>(this.baseUrl + '/empBasicInfo/get-allEmpBasicInfo')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }

  findByEmpId(id: number) {
    return this.http.get<BasicInfoModule>(this.baseUrl + '/empBasicInfo/get-EmpBasicInfosById/' + id);
  }
  
  getSelectedEmployeeType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/employee-type/get-selectedEmployeeType');
  }
  
  findByAspNetUserId(id: string) {
    return this.http.get<BasicInfoModule>(this.baseUrl + '/empBasicInfo/get-EmpBasicInfoByAspNetUserId/' + id);
  }
  
  saveEmpBasicInfo(model: any) {
    return this.http.post(this.baseUrl + '/empBasicInfo/save-EmpBasicInfos', model);
  }
  updateEmpBasicInfo(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empBasicInfo/update-EmpBasicInfos/'+id, model);
  }



}

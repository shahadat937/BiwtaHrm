import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpSpouseInfoModule } from '../model/emp-spouse-info.module';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class EmpSpouseInfoService {
  
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empSpouseInfo: EmpSpouseInfoModule;

  constructor(private http: HttpClient) { 
    this.empSpouseInfo = new EmpSpouseInfoModule();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpSpouseInfoModule[]>(this.baseUrl + '/empSpouseInfo/get-EmpSpouseInfoByEmpId/' + id);
  }

  getSelectedOccupation(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Occupation/get-selectedOccupations/');
  }
  
  saveEmpSpouseInfo(model: any[]) {
    return this.http.post(this.baseUrl + '/empSpouseInfo/save-EmpSpouseInfo', model);
  }
  deleteEmpSpouseInfo(id: number) {
    return this.http.delete(this.baseUrl + '/empSpouseInfo/delete-EmpSpouseInfo/'+id);
  }

}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { EmpForeignTourInfoModule } from '../model/emp-foreign-tour-info.module';

@Injectable({
  providedIn: 'root'
})
export class EmpForeignTourInfoService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empForeignTourInfoModule: EmpForeignTourInfoModule;

  constructor(private http: HttpClient) { 
    this.empForeignTourInfoModule = new EmpForeignTourInfoModule();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpForeignTourInfoModule[]>(this.baseUrl + '/empForeignTourInfo/get-EmpForeignTourInfoByEmpId/' + id);
  }
  
  saveEmpForeignTourInfo(model: FormData) {
    return this.http.post(this.baseUrl + '/empForeignTourInfo/save-EmpForeignTourInfo', model);
  }
  deleteEmpForeignTourInfo(id: number) {
    return this.http.delete(this.baseUrl + '/empForeignTourInfo/delete-EmpForeignTourInfo/'+id);
  }

}
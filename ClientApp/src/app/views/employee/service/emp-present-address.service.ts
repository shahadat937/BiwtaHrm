import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpPresentAddressModule } from '../model/emp-present-address.module';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmpPresentAddressService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empPresentAddress: EmpPresentAddressModule;

  constructor(private http: HttpClient) { 
    this.empPresentAddress = new EmpPresentAddressModule();
  }

  
  findByEmpId(id: number) {
    return this.http.get<EmpPresentAddressModule>(this.baseUrl + '/empPresentAddress/get-EmpPresentAddressByEmpId/' + id);
  }
  
  saveEmpPresentInfo(model: any) {
    return this.http.post(this.baseUrl + '/empPresentAddress/save-EmpPresentAddresss', model);
  }
  updateEmpPresentInfo(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empPresentAddress/update-EmpPresentAddresss/'+id, model);
  }
}

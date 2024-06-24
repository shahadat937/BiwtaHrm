import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmpPermanentAddressModule } from '../model/emp-permanent-address.module';

@Injectable({
  providedIn: 'root'
})
export class EmpPermanentAddressService {
 
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empPermanentAddress: EmpPermanentAddressModule;

  constructor(private http: HttpClient) { 
    this.empPermanentAddress = new EmpPermanentAddressModule();
  }

  
  findByEmpId(id: number) {
    return this.http.get<EmpPermanentAddressModule>(this.baseUrl + '/empPermanentAddress/get-EmpPermanentAddressByEmpId/' + id);
  }
  
  saveEmpPermanentInfo(model: any) {
    return this.http.post(this.baseUrl + '/empPermanentAddress/save-EmpPermanentAddresss', model);
  }
  updateEmpPermanentInfo(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empPermanentAddress/update-EmpPermanentAddresss/'+id, model);
  }
}

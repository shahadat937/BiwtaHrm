import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { EmpBankInfoModule } from '../model/emp-bank-info.module';

@Injectable({
  providedIn: 'root'
})
export class EmpBankInfoService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  empBankInfoModule: EmpBankInfoModule;

  constructor(private http: HttpClient) { 
    this.empBankInfoModule = new EmpBankInfoModule();
  }
  
  findByEmpId(id: number) {
    return this.http.get<EmpBankInfoModule[]>(this.baseUrl + '/empBankInfo/get-EmpBankInfoByEmpId/' + id);
  }

  getSelectedBankAccountType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/BankAccountType/get-selectedbankAccountType/');
  }
  getSelectedBankName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Bank/get-selectedbank/');
  }
  getSelectedBankBranchName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/BankBranch/get-selectedBankBranch/');
  }
  
  saveEmpBankInfo(model: FormData) {
    return this.http.post(this.baseUrl + '/empBankInfo/save-EmpBankInfo', model);
  }
  deleteEmpBankInfo(id: number) {
    return this.http.delete(this.baseUrl + '/empBankInfo/delete-EmpBankInfo/'+id);
  }

}
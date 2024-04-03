import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmployeeType } from '../model/employee-type';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class EmployeeTypeService {

  baseUrl = environment.apiUrl;
  employeeType: EmployeeType;
  constructor(private http: HttpClient) {
    this.employeeType = new EmployeeType();
   }
  find(id: number) {
    return this.http.get<EmployeeType>(this.baseUrl + '/employee-type/get-employeeTypeById/' + id);
  }
  getAll():Observable<EmployeeType[]> {
    return this.http.get<EmployeeType[]>(this.baseUrl + '/employee-type/get-employeeType');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/employee-type/update-employeeType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/employee-type/save-employeeType', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/employee-type/delete-employeeType/'+id);
  }
}

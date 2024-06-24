import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmployeeType } from '../model/employee-type';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable()
export class EmployeeTypeService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  employeeType: EmployeeType;
  constructor(private http: HttpClient) {
    this.employeeType = new EmployeeType();
   }
  find(id: number) {
    return this.http.get<EmployeeType>(this.baseUrl + '/employee-type/get-employeeTypeById/' + id);
  }
  // getAll():Observable<EmployeeType[]> {
  //   return this.http.get<EmployeeType[]>(this.baseUrl + '/employee-type/get-employeeType');
  // }
  getAll(): Observable<EmployeeType[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<EmployeeType[]>(this.baseUrl + '/employee-type/get-employeeType')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
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

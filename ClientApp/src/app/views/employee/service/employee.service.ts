import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmployeesModule } from '../model/employees.module';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  employee: EmployeesModule;

  constructor(private http: HttpClient) {
    this.employee = new EmployeesModule();
   }

   
   getAll(): Observable<EmployeesModule[]> {
    if (this.cachedData.length > 0) {
      return of (this.cachedData);
    } else {
      return this.http
        .get<EmployeesModule[]>(this.baseUrl + '/employee/get-allEmployee')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }

  
  findByEmpId(id: number) {
    return this.http.get<EmployeesModule>(this.baseUrl + '/employee/get-employeeById/' + id);
  }
  
  findByAspNetUserId(id: string) {
    return this.http.get<EmployeesModule>(this.baseUrl + '/employee/get-employeeByAspNetUserId/' + id);
  }


}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Department } from './../model/department';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  departments: Department;
  constructor(private http: HttpClient) {
    this.departments = new Department();
  }


  
  
  getSelectDepartments(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department/get-selecteddepartment');
  }

  getById(id: number) {
    return this.http.get<Department>(
      this.baseUrl + '/Department/get-departmentbyid/' + id
    );
  }
  getAll(): Observable<Department[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Department[]>(this.baseUrl + '/Department/get-department')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number, model: any) {
    return this.http.put(
      this.baseUrl + '/Department/update-department/' + id,
      model
    );
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Department/save-department', model);
  }
  delete(id: number) {
    return this.http.delete(
      this.baseUrl + '/Department/delete-department/' + id
    );
  }

  
  getDepartmentByOfficeId(id:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Department/get-selectedDepartmentByOfficeId/' + id);
  }
}

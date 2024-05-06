import { HttpClient } from '@angular/common/http';
import { SubDepartment } from './../model/sub-department';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class SubDepartmentService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  subDepartments: SubDepartment;

  constructor(private http: HttpClient) {
    this.subDepartments = new SubDepartment();
    
   }
   //custom
  find(id: number) {
    return this.http.get<SubDepartment>(this.baseUrl + '/subDepartment/get-subDepartmentbyid/' + id);
  }

  getSelectSubDepartment(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/subDepartment/get-selectedSubDepartment');
  }

  getSubDepartmentByDepartmentId(id:number): Observable<SelectedModel[]>{
    return this.http.get<SelectedModel[]>(this.baseUrl + '/subDepartment/get-subDepartmentByDepartmentId/'+id).pipe(
      map((data) => {
        return data;
      })
    );; 
  }


  getAll(): Observable<SubDepartment[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<SubDepartment[]>(this.baseUrl + '/subDepartment/get-subDepartment')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/subDepartment/update-subDepartment/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/subDepartment/save-subDepartment', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/subDepartment/delete-subDepartment/'+id);
  }

}

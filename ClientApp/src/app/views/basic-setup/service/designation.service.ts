import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { BloodGroup } from '../model/BloodGroup';
import { Observable, map, of } from 'rxjs';
import { Designation } from '../model/Designation';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable()
export class DesignationService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  designation: Designation;
  constructor(private http: HttpClient) {
    this.designation = new Designation();
   }
  find(id: number) {
    return this.http.get<Designation>(this.baseUrl + '/designation/get-DesignationDetail/' + id);
  }
  // getAll():Observable<Designation[]> {
  //   return this.http.get<Designation[]>(this.baseUrl + '/designation/get-Designation');
  // }
  getAll(): Observable<Designation[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Designation[]>(this.baseUrl + '/designation/get-Designation')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/designation/update-Designation/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/designation/save-Designation', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/designation/delete-Designation/'+id);
  }
  getDesignationsByOfficeId(id:number){
    return this.http.get<Designation[]>(this.baseUrl + '/designation/get-designationByOfficeId/' + id);
  }
  
  getDesignationsByOfficeIdAndDepartmentId(officeId:number, departmentId:number){
    return this.http.get<Designation[]>(this.baseUrl + '/designation/get-designationByOfficeIdAndDepartmentId?&officeId=' + officeId + '&departmentId='+departmentId);
  }

  selectDesignation() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/designation/get-designation'
    );
  }

  getDesignationByDepartmentId(id:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/designation/get-selectedDesignationByDepartmentId/' + id);
  }
}

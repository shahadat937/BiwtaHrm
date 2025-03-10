import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmployeesModule } from '../model/employees.module';
import { BasicInfoModule } from '../model/basic-info.module';
import { PersonalInfoModule } from '../model/personal-info.module';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  employee: EmployeesModule;
  basicInfo: BasicInfoModule;
  personalInfo: PersonalInfoModule;

  constructor(private http: HttpClient) {
    this.employee = new EmployeesModule();
    this.basicInfo = new BasicInfoModule();
    this.personalInfo = new PersonalInfoModule();
   }

   
  //  getAll(): Observable<EmployeesModule[]> {
  //   if (this.cachedData.length > 0) {
  //     return of (this.cachedData);
  //   } else {
  //     return this.http
  //       .get<EmployeesModule[]>(this.baseUrl + '/employee/get-allEmployee')
  //       .pipe(
  //         map((data) => {
  //           this.cachedData = data; 
  //           return data;
  //         })
  //       );
  //   }
  // }
  
  getAll(): Observable<EmployeesModule[]> {
      return this.http.get<EmployeesModule[]>(this.baseUrl + '/employee/get-allEmployee');
  }

  
  findByEmpId(id: number) {
    return this.http.get<EmployeesModule>(this.baseUrl + '/employee/get-employeeById/' + id);
  }
  
  findByAspNetUserId(id: string) {
    return this.http.get<EmployeesModule>(this.baseUrl + '/employee/get-employeeByAspNetUserId/' + id);
  }

  getSelectedEmployeeType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/employee-type/get-selectedEmployeeType');
  }

  getSelectedGender(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/gender/get-selectedGenders');
  }

  getSelectedMaritalStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/marital-status/get-selectedMaritalStatus');
  }
  
  getSelectedBloodGroup(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/blood-group/get-selectedBloodGroups');
  }
  
  getSelectedReligion(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/religion/get-selectedReligions');
  }
  
  getSelectedHairColor(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/hairColor/get-selectedHairColors');
  }
  
  getSelectedEyeColor(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/eyesColor/get-selectedEyesColors');
  }


  saveEmployeeInfo(model: any) {
    return this.http.post(this.baseUrl + '/employee/save-employee', model);
  }
  updateEmployeeInfo(id: number,model: any) {
    return this.http.put(this.baseUrl + '/employee/update-employee/'+id, model);
  }

}

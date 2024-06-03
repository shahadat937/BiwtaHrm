import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PersonalInfoModule } from '../model/personal-info.module';
import { Observable, of, map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { BasicInfoModule } from '../model/basic-info.module';

@Injectable({
  providedIn: 'root'
})
export class EmpPersonalInfoService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  personalInfo: PersonalInfoModule;

  constructor(private http: HttpClient) { 
    this.personalInfo = new PersonalInfoModule();
  }
  
  getAll(): Observable<PersonalInfoModule[]> {
    if (this.cachedData.length > 0) {
      return of (this.cachedData);
    } else {
      return this.http
        .get<PersonalInfoModule[]>(this.baseUrl + '/empPersonalInfo/get-allEmpPersonalInfo')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }
  
  findByEmpId(id: number) {
    return this.http.get<PersonalInfoModule>(this.baseUrl + '/empPersonalInfo/get-EmpPersonalInfoByEmpId/' + id);
  }
  
  findBasicInfoByEmpId(id: number) {
    return this.http.get<BasicInfoModule>(this.baseUrl + '/empBasicInfo/get-EmpBasicInfosById/' + id);
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
  
  getSelectedRelationType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Relation/get-selectedRelation');
  }
  
  saveEmpPersonalInfo(model: any) {
    return this.http.post(this.baseUrl + '/empPersonalInfo/save-EmpPersonalInfos', model);
  }
  updateEmpPersonalInfo(id: number,model: any) {
    return this.http.put(this.baseUrl + '/empPersonalInfo/update-EmpPersonalInfos/'+id, model);
  }

}

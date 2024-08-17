import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { RoleFeature } from '../model/role-feature';
import { SelectedStringModel } from 'src/app/core/models/selectedStringModel';

@Injectable({
  providedIn: 'root'
})
export class RoleFeatureService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  
  roleFeature: RoleFeature;
  constructor(private http: HttpClient) {
    this.roleFeature = new RoleFeature();
   }

  
  getSelectedModule() {
    return this.http.get<SelectedStringModel[]>(this.baseUrl + '/userRole/get-selectedUserRoles');
  }

  
  getFeaturesByRoleId(roleId: string) {
    return this.http.get<RoleFeature[]>(this.baseUrl + '/roleFeatures/get-features-by-role/' + roleId);
  }

  saveRoleFeatures(model: FormData){
    return this.http.post(this.baseUrl + '/roleFeatures/save-roleFeatures', model);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { RoleFeature } from '../model/role-feature';
import { ModuleFeatureByRole } from '../model/module-feature-by-role';
import { FeaturePermission } from '../model/feature-permission';
import { SelectedStringModel } from 'src/app/core/models/selectedStringModel';
import { map, Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class RoleFeatureService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  featurePermission : FeaturePermission = new FeaturePermission;
  
  roleFeature: RoleFeature;
  constructor(private http: HttpClient,
    private toastr: ToastrService,) {
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
  
  getModuleFeaturesByRole(roleName: string): Observable<ModuleFeatureByRole[]>{
    return this.http.get<ModuleFeatureByRole[]>(this.baseUrl + '/roleFeatures/get-Modulefeatures-by-role/' + roleName);
  }

  
  getFeaturePermission(featurePath: string): Observable<FeaturePermission>{
    const encryptedUser = localStorage.getItem('encryptedUser');
    var roleName;
    if (encryptedUser) {
      const bytes = CryptoJS.AES.decrypt(encryptedUser, 'secret-key');
      // console.log(JSON.parse(bytes.toString(CryptoJS.enc.Utf8)));
      roleName = JSON.parse(bytes.toString(CryptoJS.enc.Utf8)).role;
    }
    return this.http.get<FeaturePermission>(this.baseUrl + '/roleFeatures/get-featurePermission?roleName=' + roleName + '&featurePath=' + featurePath).pipe(
      map((data) => {
        this.featurePermission = data; // Cache the data
        return data;
      })
    );;
    
  }

  unauthorizeAccress(){
    this.toastr.warning('Unauthorized Access', ` `, {
      positionClass: 'toast-top-right',
    });
  }

}



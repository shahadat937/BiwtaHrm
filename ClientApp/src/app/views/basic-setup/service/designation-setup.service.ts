import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DesignationSetup } from '../model/designation-setup';

@Injectable({
  providedIn: 'root'
})
export class DesignationSetupService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  designationSetup: DesignationSetup;

  constructor(private http: HttpClient) {
    this.designationSetup = new DesignationSetup();
   }

  find(id: number) {
    return this.http.get<DesignationSetup>(this.baseUrl + '/designationSetup/get-DesignationSetupDetail/' + id);
  }

  getAll(): Observable<DesignationSetup[]> {
    if (this.cachedData.length > 0) {
      return of(this.cachedData);
    } else {
      return this.http
        .get<DesignationSetup[]>(this.baseUrl + '/designationSetup/get-DesignationSetup')
        .pipe(
          map((data) => {
            this.cachedData = data;
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/designationSetup/update-DesignationSetup/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/designationSetup/save-DesignationSetup', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/designationSetup/delete-DesignationSetup/'+id);
  }
}

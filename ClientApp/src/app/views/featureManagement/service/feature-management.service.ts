import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Module } from '../model/module';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FeatureManagementService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  modules: Module;
  constructor(private http: HttpClient) {
    this.modules = new Module();
   }

   
  find(id: number) {
    return this.http.get<Module>(this.baseUrl + '/modules/get-moduleDetail/' + id);
  }

   getAll(): Observable<Module[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of (this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Module[]>(this.baseUrl + '/modules/get-modules')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  
  submit(model: any) {
    return this.http.post(this.baseUrl + '/modules/save-module', model);
  }

  update(id: number,model: any){
    return this.http.put(this.baseUrl + '/modules/update-module/'+id, model);
  }
  
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/modules/delete-module/' + id);
  }

  getSelectedModule() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/modules/get-selectedModules');
  }

}

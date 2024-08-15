import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Module } from '../model/module';
import { Feature } from '../model/feature';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FeatureManagementService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  modules: Module;
  features: Feature;
  constructor(private http: HttpClient) {
    this.modules = new Module();
    this.features = new Feature();
   }

   
  find(id: number) {
    return this.http.get<Module>(this.baseUrl + '/modules/get-moduleDetail/' + id);
  }

  getAll() {
      return this.http.get<Module[]>(this.baseUrl + '/modules/get-modules');
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


  findFeature(id: number) {
    return this.http.get<Feature>(this.baseUrl + '/features/get-FeatureDetail/' + id);
  }

  getAllFeature(){
      return this.http.get<Feature[]>(this.baseUrl + '/features/get-Features');
  }

  
  submitFeature(model: any) {
    return this.http.post(this.baseUrl + '/features/save-Feature', model);
  }

  updateFeature(id: number,model: any){
    return this.http.put(this.baseUrl + '/features/update-Feature/'+id, model);
  }
  
  deleteFeature(id: number) {
    return this.http.delete(this.baseUrl + '/features/delete-Feature/' + id);
  }

  getSelectedFeature() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/features/get-selectedFeatures');
  }

}

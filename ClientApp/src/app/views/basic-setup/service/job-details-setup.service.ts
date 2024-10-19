import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { JobDetailsSetup } from '../model/job-details-setup';

@Injectable({
  providedIn: 'root'
})
export class JobDetailsSetupService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  jobDetailsSetups: JobDetailsSetup;
  constructor(private http: HttpClient) {
    this.jobDetailsSetups = new JobDetailsSetup();
   }

   
  find(id: number) {
    return this.http.get<JobDetailsSetup>(this.baseUrl + '/jobDetailsSetup/get-JobDetailsSetupDetail/' + id);
  }

  getAll() {
      return this.http.get<JobDetailsSetup[]>(this.baseUrl + '/jobDetailsSetup/get-JobDetailsSetups');
  }

  
  getActive() {
    return this.http.get<JobDetailsSetup>(this.baseUrl + '/jobDetailsSetup/get-ActiveJobDetailsSetups');
}

  
  submit(model: any) {
    return this.http.post(this.baseUrl + '/jobDetailsSetup/save-JobDetailsSetup', model);
  }

  update(id: number,model: any){
    return this.http.put(this.baseUrl + '/jobDetailsSetup/update-JobDetailsSetup/'+id, model);
  }
  
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/jobDetailsSetup/delete-JobDetailsSetup/' + id);
  }
  
}

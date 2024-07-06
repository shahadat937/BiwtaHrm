import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { StaffFormModule } from '../model/staff-form.module';

@Injectable({
  providedIn: 'root'
})
export class OfficerFormserviceService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  StaffModels :StaffFormModule;
  constructor(private http: HttpClient) {
    this.StaffModels=new StaffFormModule();
   }
}

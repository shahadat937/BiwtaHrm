import { HttpClient } from '@angular/common/http';
import { StaffFormModule } from './../model/staff-form.module';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StaffFormServiceService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  staffModule:StaffFormModule
  constructor(private http: HttpClient) {
    this.staffModule=new StaffFormModule()
   }
}

import { HttpClient } from '@angular/common/http';
import { StaffFormpart3Module } from './../model/staff-formpart3.module';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StaffForm3ServicesService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  staffForm3Module:StaffFormpart3Module
  constructor(private http: HttpClient) {
    this.staffForm3Module=new StaffFormpart3Module();
   }
}

import { HttpClient } from '@angular/common/http';
import { StaffFormpart2Module } from './../model/staff-formpart2.module';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StaffForm2ServiceService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  staffForm2module:StaffFormpart2Module
  constructor(private http: HttpClient) {
    this.staffForm2module=new StaffFormpart2Module();
   }
}

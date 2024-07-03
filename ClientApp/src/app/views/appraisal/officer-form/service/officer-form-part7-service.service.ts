import { HttpClient } from '@angular/common/http';
import { OfficerFormPart7Module } from './../model/officer-form-part7.module';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OfficerFormPart7ServiceService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officerFormPart7:OfficerFormPart7Module
  constructor(private http: HttpClient) {
    this.officerFormPart7=new OfficerFormPart7Module();
   }
}

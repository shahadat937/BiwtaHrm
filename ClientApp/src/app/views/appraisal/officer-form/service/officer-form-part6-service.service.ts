import { HttpClient } from '@angular/common/http';
import { OfficerFormPart6Module } from './../model/officer-form-part6.module';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OfficerFormPart6ServiceService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officerFormPart6:OfficerFormPart6Module
  constructor(private http: HttpClient) {
    this.officerFormPart6=new OfficerFormPart6Module();
   }
}

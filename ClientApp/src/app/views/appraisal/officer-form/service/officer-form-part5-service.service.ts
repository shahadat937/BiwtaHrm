import { HttpClient } from '@angular/common/http';
import { OfficerFormPart5Module } from './../model/officer-form-part5.module';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class OfficerFormPart5ServiceService  {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officerFormpart5 :OfficerFormPart5Module
  constructor(private http: HttpClient) {
    this.officerFormpart5=new OfficerFormPart5Module();
   }
}

import { HttpClient } from '@angular/common/http';
import { OfficerFormPart3Module } from './../model/officer-form-part3.module';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OfficerFormPart3ServiceService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officerForm3Module :OfficerFormPart3Module
  constructor(private http: HttpClient) {
    this.officerForm3Module=new OfficerFormPart3Module()
   }
}

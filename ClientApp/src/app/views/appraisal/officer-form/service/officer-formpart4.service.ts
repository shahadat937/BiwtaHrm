import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{OfficerFormPart4Module} from '../model/officer-form-part4.module'
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })

export class OfficerFormpart4Service {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officerFormpart4 :OfficerFormPart4Module
  constructor(private http: HttpClient) {
    this.officerFormpart4=new OfficerFormPart4Module()
   }
}
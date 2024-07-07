import { OfficerFormPart2Module } from './../model/officer-form-part2.module';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OfficerFormPart2ServiceService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officerForm2Module :OfficerFormPart2Module
  constructor(private http: HttpClient) {
    this.officerForm2Module=new OfficerFormPart2Module()
   }
}

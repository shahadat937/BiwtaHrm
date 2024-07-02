import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{OfficerFormModule} from '../model/officer-form.module';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OfficerFormService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officerModels :OfficerFormModule;
  constructor(private http: HttpClient) {
    this.officerModels=new OfficerFormModule();
   }
}

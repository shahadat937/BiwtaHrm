import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{OfficerFormModule} from '../model/officer-form.module';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OfficerFormService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officerModels :OfficerFormModule;
  setFormData: any;
  constructor(private http: HttpClient) {
    this.officerModels=new OfficerFormModule();
   }

   getFormInfo(formId: number): Observable<any> {
    return this.http.get<any>(this.baseUrl + `/form/get-formAllInfoById/${formId}`);
   }
}

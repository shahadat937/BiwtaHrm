import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{OfficerFormModule} from '../model/officer-form.module';
import { HttpClient } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';

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
    if(this.cachedData.length>0) {
      return of (this.cachedData);
    }
    return this.http.get<any>(this.baseUrl + `/form/get-formAllInfoById/${formId}`).pipe(
      map(data=> {
        this.cachedData=data;
        return data;
      })
    );
   }

   saveFormData(formData:any): Observable<any> {
    return this.http.post<any>(this.baseUrl+"/form/save-FormData",formData);
   }
}

import { Injectable } from '@angular/core';
import { FormRecordModel } from '../models/form-record-model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormRecordService {

  cachedData: FormRecordModel[] = [];
  baseUrl: string ;
  constructor(
    private http:HttpClient
  ) { 
    this.baseUrl = environment.apiUrl;
  }

  getFormRecord(): Observable<FormRecordModel[]> {
    if(this.cachedData.length>0) {
      return of (this.cachedData);
    }
    return this.http.get<FormRecordModel[]>(this.baseUrl+"/formRecord/get-FormRecord").pipe(
      map(data=> {
        this.cachedData = data;
        return data;
      })
    );
  }

  getFormRecordByFormId(formId:number):Observable<FormRecordModel[]> {
    return this.http.get<FormRecordModel[]>(this.baseUrl+`/formRecord/get-FormRecordByFormId/${formId}`);
  }

  deleteRecordById(recordId:number):Observable<any> {
    return this.http.delete<any>(this.baseUrl+`/formRecord/delete-FormRecord/${recordId}`);
  }

  getFormData(formRecordId:number): Observable<any> {
    return this.http.get<any>(this.baseUrl+`/form/get-FormDataById/${formRecordId}`);
  }


  updateFormData(formData:any): Observable<any> {
    return this.http.put<any>(this.baseUrl+'/form/update-FormData', formData);
  }
}

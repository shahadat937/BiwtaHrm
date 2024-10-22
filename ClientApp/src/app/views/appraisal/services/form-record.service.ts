import { Injectable } from '@angular/core';
import { FormRecordModel } from '../models/form-record-model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { filter, map, Observable, of } from 'rxjs';
import { FormRecordFilter } from '../models/form-record-filter';

@Injectable()
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


  updateFormData(formData:any,updateRole:number): Observable<any> {
    let params = new HttpParams();

    if(updateRole !=-1)
    params = params.set('updateRole',updateRole);

    return this.http.put<any>(this.baseUrl+'/form/update-FormData', formData, {params: params});
  }

  empInfo(IdCardNo:string): Observable<any> {
    let param = new HttpParams();
    param = param.set('IdCardNo',IdCardNo);

    return this.http.get<any>(this.baseUrl+'/form/get-EmployeeInfoForForm',{params:param});
  }

  getFormRecordFiltered(filters:FormRecordFilter): Observable<any> {

    let params = this.getParamsFromFilterModel(filters);
    return this.http.get<any>(this.baseUrl+'/formRecord/get-FormRecord', {
      params: params
    });
  }

  getParamsFromFilterModel(filters: any) {
    let params = new HttpParams();
    for(const key of Object.keys(filters)) {
      
      if(filters[key]!=null) {
        const val = filters[key].toString();
        params = params.set(key,val)
      }
    }

    return params;
  }
}

import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Office } from '../model/office';
import { Observable, map, of } from 'rxjs';

import { SelectedModel } from '../../../core/models/selectedModel';


@Injectable()
export class OfficeService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  offices: Office;
  constructor(private http: HttpClient) {
    this.offices = new Office();
   }
   getById(id: number) {
    return this.http.get<Office>(this.baseUrl + '/Office/get-officebyid/' + id);
  }
  // getAll():Observable<Office[]> {
  //   return this.http.get<Office[]>(this.baseUrl + '/Office/get-office');
  // }
  getAll(): Observable<Office[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Office[]>(this.baseUrl + '/Office/get-office')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  


  selectGetoffice() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/Office/get-selectedoffice'
    );
  }

  getOneOffice(){
    return this.http.get<Office>(this.baseUrl + '/Office/get-oneOffice');
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Office/update-office/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Office/save-office', model);
  }
  
  
  delete(id: number) { 
    return this.http.delete(this.baseUrl + '/Office/delete-office/'+id);
  }
  
  
  }
  
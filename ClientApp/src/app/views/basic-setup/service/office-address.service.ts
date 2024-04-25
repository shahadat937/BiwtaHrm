import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OfficeAddress } from '../model/office-address';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OfficeAddressService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  officeAddresses: OfficeAddress;
  constructor(private http: HttpClient) {
    this.officeAddresses = new OfficeAddress();
   }
   getById(id: number) {
    return this.http.get<OfficeAddress>(this.baseUrl + '/OfficeAddress/get-officeAddressbyid/' + id);
  }
  // getAll():Observable<OfficeAddress[]> {
  //   return this.http.get<OfficeAddress[]>(this.baseUrl + '/OfficeAddress/get-officeAddress');
  // }
  getAll(): Observable<OfficeAddress[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<OfficeAddress[]>(this.baseUrl + '/OfficeAddress/get-officeAddress')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/OfficeAddress/update-officeAddress/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/OfficeAddress/save-officeAddress', model);
  }
  
  
  delete(id: number) { 
    return this.http.delete(this.baseUrl + '/OfficeAddress/delete-officeAddress/'+id);
  }
  
  
 
 



  }
  
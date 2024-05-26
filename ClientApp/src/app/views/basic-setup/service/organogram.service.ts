import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrganogramService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getOrganogramNamesOnly(): Observable<OrganogramOfficeNameDto[]> {
    if (this.cachedData.length > 0) {
      return of(this.cachedData);
    } else {
      return this.http
        .get<OrganogramOfficeNameDto[]>(this.baseUrl + '/organogram/get-organogramNamesOnly')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }
}

export interface OrganogramOfficeNameDto {
  name: string;
  directDesignations: OrganogramDesignationNameDto[];
  departments: OrganogramDepartmentNameDto[];
}

export interface OrganogramDepartmentNameDto {
  name: string;
  designations: OrganogramDesignationNameDto[];
  subDepartments: OrganogramDepartmentNameDto[];
}

export interface OrganogramDesignationNameDto {
  name: string;
}

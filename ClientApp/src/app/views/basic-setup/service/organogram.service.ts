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

  getOrganogramNamesOnly(): Observable<any[]> {
    if (this.cachedData.length > 0) {
      return of(this.cachedData);
    } else {
      return this.http
        .get<any[]>(this.baseUrl + '/organogram/get-organogramNamesOnly')
        .pipe(
          map((data) => {
            this.cachedData = data; 
            return data;
          })
        );
    }
  }
}

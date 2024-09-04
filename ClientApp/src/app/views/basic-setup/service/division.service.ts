import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';
import { Division } from '../model/division';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable()
export class DivisionService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  divisions: Division;

  constructor(private http: HttpClient) {
    this.divisions = new Division();
    
   }
  find(id: number) {
    return this.http.get<Division>(this.baseUrl + '/division/get-divisionbyid/' + id);
  }
  getAll(): Observable<Division[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Division[]>(this.baseUrl + '/division/get-division')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  getDivisionByCountryId(id:number): Observable<SelectedModel[]>{
    return this.http.get<SelectedModel[]>(this.baseUrl + '/division/get-divisionByCountryId/'+id).pipe(
      map((data) => {
        return data;
      })
    );; 
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/division/update-division/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/division/save-division', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/division/delete-division/'+id);
  }

}

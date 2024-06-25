import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ShiftModule } from '../models/shift.module';

@Injectable({
  providedIn: 'root'
})
export class ShiftService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  shifts : ShiftModule;

  constructor(private http: HttpClient) {
    this.shifts = new ShiftModule;
   }

    
  find(id: string) {
    return this.http.get<ShiftModule>(this.baseUrl + '/Shift/get-shiftbyid/' + id);
  }

   getAll(): Observable<ShiftModule[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of (this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<ShiftModule[]>(this.baseUrl + '/Shift/get-Shift')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
   
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Shift/save-Shift', model);
  }
  update(id: string,model: any){
    return this.http.put(this.baseUrl + '/Shift/update-Shift/'+id, model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Shift/delete-Shift/'+id);
  }
}

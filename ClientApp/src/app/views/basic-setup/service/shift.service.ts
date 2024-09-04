import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Shift } from './../model/shift';
@Injectable()
export class ShiftService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  shifts: Shift;
  constructor(private http: HttpClient) {
    this.shifts = new Shift();
  }

  getById(id: number) {
    return this.http.get<Shift>(this.baseUrl + '/Shift/get-shiftbyid/' + id);
  }
  // getAll(): Observable<Shift[]> {
  //   return this.http.get<Shift[]>(this.baseUrl + '/Shift/get-Shift');
  // }
  getAll(): Observable<Shift[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Shift[]>(this.baseUrl + '/Shift/get-Shift').pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
    }
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/Shift/update-Shift/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Shift/save-Shift', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/Shift/delete-Shift/' + id);
  }
}

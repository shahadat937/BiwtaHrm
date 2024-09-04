import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Punishment } from '../model/Punishment';
@Injectable()
export class PunishmentService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  punishments: Punishment;
  constructor(private http: HttpClient) {
    this.punishments = new Punishment();
  }
  find(id: number): Observable<Punishment> {
    return this.http.get<Punishment>(
      this.baseUrl + '/punishment/get-punishmentDetail/' + id
    );
  }
  // getAll(): Observable<Punishment[]> {
  //   return this.http.get<Punishment[]>(
  //     this.baseUrl + '/punishment/get-punishment'
  //   );
  // }
  getAll(): Observable<Punishment[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Punishment[]>(this.baseUrl + '/punishment/get-punishment')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number, model: any): Observable<any> {
    return this.http.put(
      this.baseUrl + '/punishment/update-punishment/' + id,
      model
    );
  }
  submit(model: any): Observable<any> {
    return this.http.post(this.baseUrl + '/punishment/save-punishment', model);
  }
  delete(id: number): Observable<any> {
    return this.http.delete(
      this.baseUrl + '/punishment/delete-punishment/' + id
    );
  }
}

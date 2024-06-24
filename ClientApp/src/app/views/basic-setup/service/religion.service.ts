import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Religion } from '../model/religion';
@Injectable()
export class ReligionService {
  cachedData: any[] = [];
  private _religions: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);
  religions$: Observable<any[]> = this._religions.asObservable();
  baseUrl = environment.apiUrl;
  religion: Religion;
  constructor(private http: HttpClient) {
    this.religion = new Religion();
  }
  getById(id: number) {
    return this.http.get<Religion>(
      `${this.baseUrl}/religion/get-religionById/${id}`
    );
  }
  // getAll(): Observable<Religion[]> {
  //   return this.http.get<Religion[]>(this.baseUrl + '/religion/get-religion');
  // }
  getAll(): Observable<Religion[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Religion[]>(this.baseUrl + '/religion/get-religion')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number, model: any) {
    return this.http.put(
      this.baseUrl + '/religion/update-religion/' + id,
      model
    );
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/religion/save-religion', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/religion/delete-religion/' + id);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from '../../../../environments/environment';
import { Ward } from './../model/ward';
@Injectable({
  providedIn: 'root',
})
export class WardService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  wards: Ward;
  constructor(private http: HttpClient) {
    this.wards = new Ward();
  }

  getUnion() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/union/get-selectedUnion'
    );
  }

  
  getward() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/ward/get-selectedward'
    );
  }

  find(id: number) {
    return this.http.get<Ward>(this.baseUrl + '/ward/get-wardbyid/' + id);
  }
  // getAll(): Observable<Ward[]> {
  //   return this.http.get<Ward[]>(this.baseUrl + '/ward/get-ward');
  // }
  getAll(): Observable<Ward[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Ward[]>(this.baseUrl + '/ward/get-ward').pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
    }
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/ward/update-ward/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/ward/save-ward', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/ward/delete-ward/' + id);
  }
}

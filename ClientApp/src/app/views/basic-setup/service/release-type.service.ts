import { Injectable } from '@angular/core';
import { ReleaseType } from '../model/release-type';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReleaseTypeService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  releaseType: ReleaseType;
  constructor(private http: HttpClient) {
    this.releaseType = new ReleaseType();
  }

  getById(id: number) {
    return this.http.get<ReleaseType>(this.baseUrl + '/releaseType/get-ReleaseTypebyid/' + id);
  }
  getAll(): Observable<ReleaseType[]> {
    if (this.cachedData.length > 0) {
      return of(this.cachedData);
    } else {
      return this.http.get<ReleaseType[]>(this.baseUrl + '/releaseType/get-ReleaseType').pipe(
        map((data) => {
          this.cachedData = data;
          return data;
        })
      );
    }
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/releaseType/update-ReleaseType/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/releaseType/save-ReleaseType', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/releaseType/delete-ReleaseType/' + id);
  }
}

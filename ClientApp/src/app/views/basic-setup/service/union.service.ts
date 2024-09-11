import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from '../../../../environments/environment';
import { Union } from './../model/union';
@Injectable()
export class UnionService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  unions: Union;
  constructor(private http: HttpClient) {
    this.unions = new Union();
  }

  getThana() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/Thana/get-selectedThana'
    );
  }

  find(id: number) {
    return this.http.get<Union>(this.baseUrl + '/union/get-unionbyid/' + id);
  }
  // getAll():Observable<Union[]> {
  //   return this.http.get<Union[]>(this.baseUrl + '/union/get-union');
  // }
  getAll(): Observable<Union[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Union[]>(this.baseUrl + '/union/get-union').pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
    }
  }
  getUnionNamesByThanaId(id:number): Observable<SelectedModel[]>{
    return this.http.get<SelectedModel[]>(this.baseUrl + '/union/get-unionByThanaId/'+id).pipe(
      map((data) => {
        return data;
      })
    );; 
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/union/update-union/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/union/save-union', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/union/delete-union/'+id);
  }
}

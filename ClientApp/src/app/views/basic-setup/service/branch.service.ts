import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Branch } from './../model/branch';
@Injectable({
  providedIn: 'root',
})
export class BranchService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  branchs: Branch;
  constructor(private http: HttpClient) {
    this.branchs = new Branch();
  }

  getById(id: number) {
    return this.http.get<Branch>(this.baseUrl + '/Branch/get-branchbyid/' + id);
  }
  // getAll():Observable<Branch[]> {
  //   return this.http.get<Branch[]>(this.baseUrl + '/Branch/get-branch');
  // }
  getAll(): Observable<Branch[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Branch[]>(this.baseUrl + '/Branch/get-branch').pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
    }
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/Branch/update-branch/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Branch/save-branch', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/Branch/delete-branch/' + id);
  }
}

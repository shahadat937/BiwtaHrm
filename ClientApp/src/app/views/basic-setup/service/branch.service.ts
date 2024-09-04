import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Branch } from './../model/branch';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable()
export class BranchService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  branchs: Branch;
  constructor(private http: HttpClient) {
    this.branchs = new Branch();
  }

  getSelectBranch(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/officeBranch/get-selectedOfficeBranch');
  }

  getById(id: number) {
    return this.http.get<Branch>(this.baseUrl + '/officeBranch/get-officeBranchid/' + id);
  }
  getAll(): Observable<Branch[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Branch[]>(this.baseUrl + '/officeBranch/get-officeBranch').pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
    }
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/officeBranch/update-officeBranch/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/officeBranch/save-officeBranch', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/officeBranch/delete-officeBranch/' + id);
  }
  getBranchByOfficeId(id:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/officeBranch/get-selectedBranchByOfficeId/' + id);
  }
  
}

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SubBranch } from '../model/sub-branch';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class SubBranchService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  subBranchs: SubBranch;

  constructor(private http: HttpClient) {
    this.subBranchs = new SubBranch();
    
   }
   //custom
  find(id: number) {
    return this.http.get<SubBranch>(this.baseUrl + '/SubBranch/get-subBranchbyid/' + id);
  }

  getSelectSubBranchs(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/SubBranch/get-selectedsubBranch');
  }


  getAll(): Observable<SubBranch[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<SubBranch[]>(this.baseUrl + '/SubBranch/get-subBranch')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/SubBranch/update-subBranch/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/SubBranch/save-subBranch', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/SubBranch/delete-subBranch/'+id);
  }

}
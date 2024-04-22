import { HttpClient } from '@angular/common/http';
import { Pool } from './../model/pool';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class PoolService {


  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  pools: Pool;

  constructor(private http: HttpClient) {
    this.pools = new Pool();
   }
   find(id: number) {
    return this.http.get<Pool>(this.baseUrl + '/pool/get-poolDetail/' + id);
  }

//Custom
  // getSelectGradeType(){
  //   return this.http.get<Pool[]>(this.baseUrl + '/grade-type/get-selectedGradeTypes');
  // }

  getAll(): Observable<Pool[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Pool[]>(this.baseUrl + '/pool/get-pool')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/pool/update-pool/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/pool/save-pool', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/pool/delete-pool/'+id);
  }


}

import { Injectable } from '@angular/core';
import { Result } from '../model/result';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResultService {

  baseUrl = environment.apiUrl;
  result: Result[] = [];
 // genderPagination = new GenderPagination();
  constructor(private http: HttpClient) { }

  // getResult(){
  //   let params = new HttpParams();

  //   // params = params.append('searchText', searchText.toString());
  //   // params = params.append('pageNumber', pageNumber.toString());
  //   // params = params.append('pageSize', pageSize.toString());

  //   return this.http.get(this.baseUrl + '/Result/get-result',{ observe: 'response', params })
  //   .pipe(
  //     map(response => {
  //       this.result = [...this.result];

  //      // this.genderPagination = response.body;
  //       //return this.genderPagination;
  //     })
  //   )
  // }

  find(id: number) {
    return this.http.get<Result>(this.baseUrl + '/Result/get-result/' + id);
  }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/gender/update-gender/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/gender/save-gender', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/gender/delete-gender/'+id);
  }
}

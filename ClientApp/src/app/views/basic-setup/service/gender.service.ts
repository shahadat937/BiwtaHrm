import { Injectable } from '@angular/core';
import { Gender } from '../model/gender';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable()
export class GenderService {
  baseUrl = environment.apiUrl;
  gender: Gender;
  constructor(private http: HttpClient) {
    this.gender = new Gender();
   }

   
  find(id: number) {
    return this.http.get<Gender>(this.baseUrl + '/gender/get-genderById/' + id);
  }
  getAll():Observable<Gender[]> {
    return this.http.get<Gender[]>(this.baseUrl + '/gender/get-gender');
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

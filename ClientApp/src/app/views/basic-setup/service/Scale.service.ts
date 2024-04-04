import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Scale } from '../model/Scale';
import { Observable } from 'rxjs';
import { GradeService } from './Grade.service';
@Injectable({
  providedIn: 'root'
})
export class ScaleService {
  baseUrl = environment.apiUrl;
  scales: Scale;
  constructor(private http: HttpClient) {
    this.scales = new Scale();
    
   }
   //custom
  find(id: number) {
    return this.http.get<Scale>(this.baseUrl + '/scale/get-scaleDetail/' + id);
  }
  getGrades() {
    return this.http.get<any[]>(this.baseUrl + '/scaleGradeView/get-scaleGradeView');
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/scale/update-scale/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/scale/save-scale', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/scale/delete-scale/'+id);
  }

}

import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Scale } from '../model/Scale';
import { Observable, map, of } from 'rxjs';
import { GradeService } from './Grade.service';
@Injectable({
  providedIn: 'root'
})
export class ScaleService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  scales: Scale;

  constructor(private http: HttpClient) {
    this.scales = new Scale();
    
   }
   //custom
  find(id: number) {
    return this.http.get<Scale>(this.baseUrl + '/scale/get-scaleDetail/' + id);
  }

  getAll(): Observable<Scale[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Scale[]>(this.baseUrl + '/scale/get-scale')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
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

import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HairColor } from '../model/hair-color';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HairColorService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  hairColors: HairColor;
  constructor(private http: HttpClient) {
    this.hairColors = new HairColor();
   }
  find(id: number) {
    return this.http.get<HairColor>(this.baseUrl + '/hairColor/get-hairColorDetail/' + id);
  }
  // getAll():Observable<BloodGroup[]> {
  //   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
  // }
  getAll(): Observable<HairColor[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<HairColor[]>(this.baseUrl + '/hairColor/get-hairColor')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/hairColor/update-hairColor/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/hairColor/save-hairColor', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/hairColor/delete-hairColor/'+id);
  }

}

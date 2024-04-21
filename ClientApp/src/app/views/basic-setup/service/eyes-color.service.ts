import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EyesColor } from '../model/eyes-color';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EyesColorService  {
  cachedData: any[] = [];
baseUrl = environment.apiUrl;
eyesColors: EyesColor;
constructor(private http: HttpClient) {
  this.eyesColors = new EyesColor();
 }
find(id: number) {
  return this.http.get<EyesColor>(this.baseUrl + '/eyesColor/get-eyesColorDetail/' + id);
}
// getAll():Observable<BloodGroup[]> {
//   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
// }
getAll(): Observable<EyesColor[]> {
  if (this.cachedData.length > 0) {
    // If data is already cached, return it without making a server call
    return of(this.cachedData);
  } else {
    // If data is not cached, make a server call to fetch it
    return this.http
      .get<EyesColor[]>(this.baseUrl + '/eyesColor/get-eyesColor')
      .pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
  }
}

update(id: number,model: any) {
  return this.http.put(this.baseUrl + '/eyesColor/update-eyesColor/'+id, model);
}
submit(model: any) {
  return this.http.post(this.baseUrl + '/eyesColor/save-eyesColor', model);
}
delete(id:number){
  return this.http.delete(this.baseUrl + '/eyesColor/delete-eyesColor/'+id);
}

}
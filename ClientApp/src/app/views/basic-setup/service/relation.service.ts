import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Relation } from '../model/relation';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RelationService {
  cachedData: any[] = [];
baseUrl = environment.apiUrl;
relations: Relation;
constructor(private http: HttpClient) {
  this.relations = new Relation();
 }
find(id: number) {
  return this.http.get<Relation>(this.baseUrl + '/Relation/get-relationDetail/' + id);
}
// getAll():Observable<BloodGroup[]> {
//   return this.http.get<BloodGroup[]>(this.baseUrl + '/blood-group/get-bloodGroup');
// }
getAll(): Observable<Relation[]> {
  if (this.cachedData.length > 0) {
    // If data is already cached, return it without making a server call
    return of(this.cachedData);
  } else {
    // If data is not cached, make a server call to fetch it
    return this.http
      .get<Relation[]>(this.baseUrl + '/Relation/get-relation')
      .pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
  }
}

update(id: number,model: any) {
  return this.http.put(this.baseUrl + '/Relation/update-Relation/'+id, model);
}
submit(model: any) {
  return this.http.post(this.baseUrl + '/Relation/save-Relation', model);
}
delete(id:number){
  return this.http.delete(this.baseUrl + '/Relation/delete-Relation/'+id);
}

}


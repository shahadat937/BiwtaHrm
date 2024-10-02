import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TrainingName } from '../model/trainingName';
import { Observable, map, of } from 'rxjs';

@Injectable()

export class TrainingNameService {

cachedData: any[] = [];
baseUrl = environment.apiUrl;
trainingNameses: TrainingName;
constructor(private http: HttpClient) {
  this.trainingNameses = new TrainingName();
 }
find(id: number) {
  return this.http.get<TrainingName>(this.baseUrl + '/TrainingName/get-trainingNamebyid/' + id);
}
// getAll():Observable<TrainingName[]> {
//   return this.http.get<TrainingName[]>(this.baseUrl + '/TrainingName/get-trainingName');
// }
getAll(): Observable<TrainingName[]> {
  if (this.cachedData.length > 0) {
    // If data is already cached, return it without making a server call
    return of(this.cachedData);
  } else {
    // If data is not cached, make a server call to fetch it
    return this.http
      .get<TrainingName[]>(this.baseUrl + '/TrainingName/get-trainingName')
      .pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
  }
}

update(id: number,model: any) {
  return this.http.put(this.baseUrl + '/TrainingName/update-trainingName/'+id, model);
}
submit(model: any) {
  return this.http.post(this.baseUrl + '/TrainingName/save-trainingName', model);
}


delete(id: number) { 
  return this.http.delete(this.baseUrl + '/TrainingName/delete-trainingName/'+id);
}


}

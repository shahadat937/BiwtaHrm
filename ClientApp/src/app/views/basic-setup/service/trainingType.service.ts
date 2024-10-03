import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { TrainingType } from './../model/trainingType';
@Injectable()
export class TrainingTypeService {
  find(arg0: number) {
    throw new Error('Method not implemented.');
  }
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  trainingTypes: TrainingType;
  constructor(private http: HttpClient) {
    this.trainingTypes = new TrainingType();
  }

  getById(id: number) {
    return this.http.get<TrainingType>(
      this.baseUrl + '/training-type/get-trainingtypebyid/' + id
    );
  }
  // getAll(): Observable<TrainingType[]> {
  //   return this.http.get<TrainingType[]>(
  //     this.baseUrl + '/training-type/get-trainingType'
  //   );
  // }
  getAll(): Observable<TrainingType[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<TrainingType[]>(this.baseUrl + '/training-type/get-trainingType')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  update(id: number, model: any) {
    return this.http.put(
      this.baseUrl + '/training-type/update-trainingType/' + id,
      model
    );
  }
  submit(model: any) {
    return this.http.post(
      this.baseUrl + '/training-type/save-trainingType',
      model
    );
  }
  delete(id: number) {
    return this.http.delete(
      this.baseUrl + '/training-type/delete-trainingType/' + id
    );
  }
}

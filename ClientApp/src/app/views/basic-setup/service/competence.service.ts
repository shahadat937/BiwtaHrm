import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Competence } from '../model/competence';
import { Observable, map, of } from 'rxjs';

import { SelectedModel } from '../../../core/models/selectedModel';

@Injectable()
export class CompetenceService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  competences: Competence;
  constructor(private http: HttpClient) {
    this.competences = new Competence();
   }
   getById(id: number) {
    return this.http.get<Competence>(this.baseUrl + '/Competence/get-competencebyid/' + id);
  }
  // getAll():Observable<Competence[]> {
  //   return this.http.get<Competence[]>(this.baseUrl + '/Competence/get-competence');
  // }
  getAll(): Observable<Competence[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Competence[]>(this.baseUrl + '/Competence/get-competence')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }
  


  selectGetcompetence() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/Competence/get-selectedcompetence'
    );
  }



  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Competence/update-competence/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Competence/save-competence', model);
  }
  
  
  delete(id: number) { 
    return this.http.delete(this.baseUrl + '/Competence/delete-competence/'+id);
  }
  
  
  }
  
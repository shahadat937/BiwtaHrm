import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Language } from '../model/language';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  languages: Language;
  constructor(private http: HttpClient) {
    this.languages = new Language();
   }
  find(id: number) {
    return this.http.get<Language>(this.baseUrl + '/Language/get-languagebyid/' + id);
  }

  getAll(): Observable<Language[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Language[]>(this.baseUrl + '/Language/get-language')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Language/update-language/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Language/save-language', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Language/delete-language/'+id);
  }

}

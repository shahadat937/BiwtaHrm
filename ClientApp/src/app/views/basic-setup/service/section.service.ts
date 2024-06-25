import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Section } from '../model/section';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SectionService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  sections: Section;
  constructor(private http: HttpClient) {
    this.sections = new Section();
  }
  find(id: number) {
    return this.http.get<Section>(
      this.baseUrl + '/section/get-Sectionbyid/' + id
    );
  }
  // selectGetBank() {
  //   return this.http.get<SelectedModel[]>(
  //     this.baseUrl + '/Bank/get-selectedbank'
  //   );
  // }
  getAll(): Observable<Section[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Section[]>(this.baseUrl + '/section/get-Section')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/section/update-Section/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/section/save-Section', model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + '/section/delete-Section/' + id);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from '../../../../environments/environment';
import { Group } from './../model/group';
@Injectable({
  providedIn: 'root',
})
export class GroupService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  groups: Group;
  constructor(private http: HttpClient) {
    this.groups = new Group();
  }

  getSubject() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/Subject/get-selectedsubject'
    );
  }

  find(id: number) {
    return this.http.get<Group>(this.baseUrl + '/subGroup/get-subgroupbyid/' + id);
  }
  // getAll():Observable<Group[]> {
  //   return this.http.get<Group[]>(this.baseUrl + '/group/get-group');
  // }
  getAll(): Observable<Group[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http.get<Group[]>(this.baseUrl + '/subGroup/get-subgroup').pipe(
        map((data) => {
          this.cachedData = data; // Cache the data
          return data;
        })
      );
    }
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/subGroup/update-subgroup/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/subGroup/save-subgroup', model);
  }
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/subGroup/delete-subgroup/' + id);
  }
}

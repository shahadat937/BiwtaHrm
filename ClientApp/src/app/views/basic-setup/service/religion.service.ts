import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Religion } from '../model/religion';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class ReligionService {
  private _religions: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);
  religions$: Observable<any[]> = this._religions.asObservable();

  baseUrl = environment.apiUrl;
  religion: Religion;
  constructor(private http: HttpClient) {
    this.religion = new Religion();
   }
  getById(id: number) {
    return this.http.get<Religion>(this.baseUrl + '/religion/get-religionById/' + id);
  }
  getAll():Observable<Religion[]> {
    return this.http.get<Religion[]>(this.baseUrl + '/religion/get-religion');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/religion/update-religion/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/religion/save-religion', model);
  }
  delete(id:number){
    return this.http.delete(this.baseUrl + '/religion/delete-religion/'+id);
  }
  setReligions(religions: any[]): void {
    this._religions.next(religions);
  }

  getReligions(): Observable<any[]> {
    return this.religions$;
  }
}

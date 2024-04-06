import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Punishment } from '../model/Punishment';
@Injectable({
  providedIn: 'root',
})
export class PunishmentService {
  baseUrl = environment.apiUrl;
  punishments: Punishment;
  constructor(private http: HttpClient) {
    this.punishments = new Punishment();
  }
  find(id: number): Observable<Punishment> {
    return this.http.get<Punishment>(
      this.baseUrl + '/punishment/get-punishmentDetail/' + id
    );
  }
  getAll(): Observable<Punishment[]> {
    return this.http.get<Punishment[]>(
      this.baseUrl + '/punishment/get-punishment'
    );
  }
  update(id: number, model: any): Observable<any> {
    return this.http.put(
      this.baseUrl + '/punishment/update-punishment/' + id,
      model
    );
  }
  submit(model: any): Observable<any> {
    return this.http.post(this.baseUrl + '/punishment/save-punishment', model);
  }
  delete(id: number): Observable<any> {
    return this.http.delete(
      this.baseUrl + '/punishment/delete-punishment/' + id
    );
  }
}

import { environment } from '../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Shift}  from  './../model/shift'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShiftService  {
  baseUrl = environment.apiUrl;
  shifts: Shift;
  constructor(private http: HttpClient) {
    this.shifts=new Shift();
   }


  getById(id: number) {
    return this.http.get<Shift>(this.baseUrl + '/Shift/get-shiftbyid/' + id);
  }
  getAll():Observable<Shift[]> {
    return this.http.get<Shift[]>(this.baseUrl + '/Shift/get-Shift');
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/Shift/update-Shift/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/Shift/save-Shift', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/Shift/delete-Shift/'+id);
  }

}

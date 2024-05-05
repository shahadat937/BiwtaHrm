import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, map, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ShiftModule } from '../models/shift.module';

@Injectable({
  providedIn: 'root'
})
export class ShiftService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  shifts : ShiftModule;

  constructor(private http: HttpClient) {
    this.shifts = new ShiftModule;
   }
}

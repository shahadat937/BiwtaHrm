import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StaffForm3ServicesService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {
   }
}

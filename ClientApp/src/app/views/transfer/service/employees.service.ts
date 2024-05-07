import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  cachedData: any[] = [];
  
  constructor(private http: HttpClient) {
    
  }
   demoUser: User = {
    firstName: 'John',
    lastName: 'Doe',
    email: 'john.doe@example.com'
  };
}


// user.model.ts
export interface User {
  firstName: string;
  lastName: string;
  email: string;
}

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  bsModelRef!: BsModalRef;
  cachedData: any[] = [];
  getemployees:any[]=[];
  constructor(private modalService: BsModalService,private http: HttpClient) { }

  confirm(title = 'Confirmation', 
    message = 'Are you sure you want to do this?', 
    btnOkText = 'Ok', 
    btnCancelText = 'Cancel'): Observable<boolean> {
      const config = {
        initialState: {
          title, 
          message,
          btnOkText,
          btnCancelText
        }
      }
    
    
    return new Observable<boolean>(this.getResult());
  }

  private getResult() {
    return (observer:any) => {
      // Check if this.bsModelRef is defined and if onHidden is available
      if (this.bsModelRef && this.bsModelRef.onHidden) {
        const subscription = this.bsModelRef.onHidden.subscribe(() => {
          if (this.bsModelRef && this.bsModelRef.content) {
            observer.next(this.bsModelRef.content.result);
            observer.complete();
          }
        });
  
        return {
          unsubscribe() {
            subscription.unsubscribe();
          }
        };
      } else {
        // Handle the case when this.bsModelRef.onHidden is undefined
        // For example, you can complete the observer immediately
        observer.error("bsModelRef.onHidden is undefined");
        return {
          unsubscribe() {
            // No need to unsubscribe as there's no subscription
          }
        };
      }
    };
  }

  saveEmployee(employeeData: Employee): Observable<any> {
   
    this.cachedData.push(employeeData);
    return this.http.post<any>('', employeeData); // Example: Replace 'your-api-endpoint' with your actual API endpoint
  }


   demoEmployee: Employee = {
    Id:0,
    firstName: '',
    lastName: '',
    email: ''
  };
}
// user.model.ts
export interface Employee {
  Id:number;
  firstName: string;
  lastName: string;
  email: string;
}

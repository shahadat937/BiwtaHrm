import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { environment } from '../../../environments/environment';
import * as CryptoJS from 'crypto-js';



@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiUrl;
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.currentUserSubject = new BehaviorSubject<User>(currentUserJSON);
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string, remember: boolean) {
    return this.http
      .post<any>(`${environment.securityUrl}/account/login`, {
        email,
        password,
        remember
      })
      .pipe(
        map((user) => {
          // store user details and jwt token in local storage to keep user logged in between page refreshes

          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
          
          const encryptedUser = CryptoJS.AES.encrypt(JSON.stringify(user), 'secret-key').toString();
          localStorage.setItem('encryptedUser', encryptedUser);
          return user;
        })
      );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    localStorage.removeItem('encryptedUser');
    this.currentUserSubject.next(null!);
    return of({ success: false });
  }

  verifyToken(token:string) : Observable<any> {
    let payload = {
       "userName":"string",
      'token':token
    }
    return this.http.post<any>(`${environment.securityUrl}/account/verifyToken`,payload)
  }
}

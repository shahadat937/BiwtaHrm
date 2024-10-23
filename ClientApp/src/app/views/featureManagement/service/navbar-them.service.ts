import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { NavbarThem } from '../model/navbar-them';

@Injectable({
  providedIn: 'root'
})
export class NavbarThemService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  navbarThem: NavbarThem;
  constructor(private http: HttpClient) {
    this.navbarThem = new NavbarThem();
   }

   
  find(id: number) {
    return this.http.get<NavbarThem>(this.baseUrl + '/navbarThem/get-NavbarThembyid/' + id);
  }

  getAll() {
      return this.http.get<NavbarThem[]>(this.baseUrl + '/navbarThem/get-NavbarThem');
  }

  
  submit(model: any) {
    return this.http.post(this.baseUrl + '/navbarThem/save-NavbarThem', model);
  }

  update(id: number,model: any){
    return this.http.put(this.baseUrl + '/navbarThem/update-NavbarThem/'+id, model);
  }
  
  delete(id: number) {
    return this.http.delete(this.baseUrl + '/navbarThem/delete-NavbarThem/' + id);
  }

  getSelectedNavbarThem() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/navbarThem/get-selectedNavbarThem');
  }

}

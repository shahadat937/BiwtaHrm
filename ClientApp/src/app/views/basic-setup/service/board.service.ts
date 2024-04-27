import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Board } from '../model/board';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of } from 'rxjs';
import { Border } from '@coreui/angular/lib/utilities/border.type';

@Injectable({
  providedIn: 'root'
})
export class BoardService {

  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  boards: Board;
  constructor(private http: HttpClient) {
    this.boards = new Board();
  }
  find(id: number) {
    return this.http.get<Board>(
      this.baseUrl + '/Bank/get-bankbyid/' + id
    );
  }

  getAll(): Observable<Border[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Border[]>(this.baseUrl + '/board/get-Board')
        .pipe(
          map((data: any[]) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/board/update-Board/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/board/save-Board', model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + '/board/delete-Board/' + id);
  }
}
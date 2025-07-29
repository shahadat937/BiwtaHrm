import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { SelectedModel } from '../../../core/models/selectedModel';
import { Country } from '../model/Country';

@Injectable()
export class CountryService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  countrys: Country;
  constructor(private http: HttpClient) {
    this.countrys = new Country();
  }
  find(id: number) {
    return this.http.get<Country>(
      this.baseUrl + '/country/get-countryDetail/' + id
    );
  }
  selectGetCountry() {
    return this.http.get<SelectedModel[]>(
      this.baseUrl + '/country/get-selectedCountrys'
    );
  }
  getAll(): Observable<Country[]> {
    if (this.cachedData.length > 0) {
      // If data is already cached, return it without making a server call
      return of(this.cachedData);
    } else {
      // If data is not cached, make a server call to fetch it
      return this.http
        .get<Country[]>(this.baseUrl + '/country/get-country')
        .pipe(
          map((data) => {
            this.cachedData = data; // Cache the data
            return data;
          })
        );
    }
  }

  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/country/update-country/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/country/save-country', model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + '/country/delete-country/' + id);
  }
  
  getDeaultCountryId() {
    return this.http.get<number>(
      this.baseUrl + '/country/get-defaultCountryId'
    );
  }
}

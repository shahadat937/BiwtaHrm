import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { OfficeOrder } from '../models/office-order';

@Injectable()
export class OfficeOrderService {
  
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getAll(queryParams: any, orderTypeId: number, departmentId: number, sectionId: number, designationId: number,  orderNo: string, fromDate: any, toDate: any) {
      let params = new HttpParams({ fromObject: queryParams });
      params = params.append('orderTypeId', orderTypeId);
      params = params.append('departmentId', departmentId);
      params = params.append('sectionId', sectionId);
      params = params.append('designationId', designationId);
      params = params.append('orderNo', orderNo);
      params = params.append('fromDate', fromDate);
      params = params.append('toDate', toDate);
      return this.http.get<any>(`${this.baseUrl}/officeOrder/get-OfficeOrders`, { params });
  }

  find(id: number) {
    return this.http.get<OfficeOrder>(this.baseUrl + '/officeOrder/get-OfficeOrderDetail/' + id);
  }
  
  save(model: any) {
    const formData = this.toFormData(model);
    return this.http.post(`${this.baseUrl}/officeOrder/save-OfficeOrder`, formData);
  }

  update(id: number, model: any) {
    const formData = this.toFormData(model);
    return this.http.put(`${this.baseUrl}/officeOrder/update-OfficeOrder/${id}`, formData);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + '/officeOrder/delete-OfficeOrder/' + id);
  }

  private toFormData(model: any): FormData {
    const formData = new FormData();
    for (const key of Object.keys(model)) {
      if (model[key] instanceof File) {
        formData.append(key, model[key], model[key].name);
      } else {
        formData.append(key, model[key]);
      }
    }
    return formData;
  }
  
}

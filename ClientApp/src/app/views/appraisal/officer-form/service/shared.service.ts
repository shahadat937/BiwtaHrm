import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private formData: any = {};

  setFormData(part: string, data: any) {
    this.formData[part] = data;
  }
  getFormData(part: string) {
    return this.formData[part];
  }
  getAllFormData() {
    return this.formData;
  }

  constructor() { }
}

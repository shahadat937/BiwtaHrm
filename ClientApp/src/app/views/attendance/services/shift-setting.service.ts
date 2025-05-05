import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { TreeShift } from '../models/tree-shift';
import { ShiftType } from '../models/shift-type';
import { ShiftSetting } from '../models/shift-setting';

@Injectable({
  providedIn: 'root'
})
export class ShiftSettingService {
  cachedData: any[] = [];
  baseUrl = environment.apiUrl;
  shiftType: ShiftType;
  shiftSetting: ShiftSetting;

  constructor(private http: HttpClient) {
    this.shiftType = new ShiftType();
    this.shiftSetting = new ShiftSetting();
  }


  getAllShiftSetting() {
    return this.http.get<ShiftSetting[]>(this.baseUrl + '/shiftSetting/get-ShiftSettings/');
  }
  getSelectedShiftSetting() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/shiftSetting/get-selectedShiftSettings');
  }
  getActiveShiftSetting() {
    return this.http.get<any>(this.baseUrl + '/shiftSetting/get-ActiveShiftSettings');
  }
  findShiftSetting(id: number) {
    return this.http.get<ShiftSetting>(this.baseUrl + '/shiftSetting/get-ShiftSettingDetail/' + id);
  }

  submitShiftSetting(model: any) {
    return this.http.post(this.baseUrl + '/shiftSetting/save-ShiftSetting', model);
  }
  updateShiftSetting(id: number, model: any) {
    return this.http.put(this.baseUrl + '/shiftSetting/update-ShiftSetting/' + id, model);
  }
  deleteShiftSetting(id: number) {
    return this.http.delete(this.baseUrl + '/shiftSetting/delete-ShiftSetting/' + id);
  }


  getAllShiftType() {
    return this.http.get<ShiftType[]>(this.baseUrl + '/shiftType/get-ShiftTypes/');
  }
  getTreeShiftType() {
    return this.http.get<TreeShift[]>(this.baseUrl + '/shiftType/get-TreeShiftTypes/');
  }
  getSelectedShiftType() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/shiftType/get-selectedShiftTypes');
  }
  getActiveShiftType() {
    return this.http.get<any>(this.baseUrl + '/shiftType/get-ActiveShiftTypes');
  }
  findShiftType(id: number) {
    return this.http.get<ShiftType>(this.baseUrl + '/shiftType/get-ShiftTypeDetail/' + id);
  }

  submitShiftType(model: any) {
    return this.http.post(this.baseUrl + '/shiftType/save-ShiftType', model);
  }
  updateShiftType(id: number, model: any) {
    return this.http.put(this.baseUrl + '/shiftType/update-ShiftType/' + id, model);
  }
  deleteShiftType(id: number) {
    return this.http.delete(this.baseUrl + '/shiftType/delete-ShiftType/' + id);
  }
}

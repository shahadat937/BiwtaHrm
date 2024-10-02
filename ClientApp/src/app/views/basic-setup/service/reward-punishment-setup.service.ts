import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RewardPunishmentType } from '../model/reward-punishment-type';
import { RewardPunishmentPriority } from '../model/reward-punishment-priority';
import { Observable, of, map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class RewardPunishmentSetupService {
  cachedTypeData: any[] = [];
  cachedPriorityData: any[] = [];
  baseUrl = environment.apiUrl;
  rewardPunishmentType: RewardPunishmentType;
  rewardPunishmentPriority: RewardPunishmentPriority;

  constructor(private http: HttpClient) {
    this.rewardPunishmentType = new RewardPunishmentType();
    this.rewardPunishmentPriority = new RewardPunishmentPriority();
   }

  findRewardType(id: number) {
    return this.http.get<RewardPunishmentType>(this.baseUrl + '/rewardPunishmentType/get-RewardPunishmentTypeDetail/' + id);
  }
  findRewardPriotiry(id: number) {
    return this.http.get<RewardPunishmentPriority>(this.baseUrl + '/rewardPunishmentPriority/get-RewardPunishmentPriorityDetail/' + id);
  }

  getAllRewardType(): Observable<RewardPunishmentType[]> {
    if (this.cachedTypeData.length > 0) {
      return of(this.cachedTypeData);
    } else {
      return this.http
        .get<RewardPunishmentType[]>(this.baseUrl + '/rewardPunishmentType/get-RewardPunishmentType')
        .pipe(
          map((data) => {
            this.cachedTypeData = data;
            return data;
          })
        );
    }
  }
  
  getAllRewardPriotiry(): Observable<RewardPunishmentPriority[]> {
    if (this.cachedPriorityData.length > 0) {
      return of(this.cachedPriorityData);
    } else {
      return this.http
        .get<RewardPunishmentPriority[]>(this.baseUrl + '/rewardPunishmentPriority/get-RewardPunishmentPriority')
        .pipe(
          map((data) => {
            this.cachedPriorityData = data;
            return data;
          })
        );
    }
  }

  updateRewardType(id: number,model: any) {
    return this.http.put(this.baseUrl + '/rewardPunishmentType/update-RewardPunishmentType/'+id, model);
  }
  submitRewardType(model: any) {
    return this.http.post(this.baseUrl + '/rewardPunishmentType/save-RewardPunishmentType', model);
  } 
  deleteRewardType(id:number){
    return this.http.delete(this.baseUrl + '/rewardPunishmentType/delete-RewardPunishmentType/'+id);
  }
  getSelectedRewardType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/rewardPunishmentType/get-selectedRewardPunishmentTypes')
  }
  
  updateRewardPriority(id: number,model: any) {
    return this.http.put(this.baseUrl + '/rewardPunishmentPriority/update-RewardPunishmentPriority/'+id, model);
  }
  submitRewardPriority(model: any) {
    return this.http.post(this.baseUrl + '/rewardPunishmentPriority/save-RewardPunishmentPriority', model);
  } 
  deleteRewardPriority(id:number){
    return this.http.delete(this.baseUrl + '/rewardPunishmentPriority/delete-RewardPunishmentPriority/'+id);
  }
  getSelectedRewardPriority(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/rewardPunishmentPriority/get-selectedRewardPunishmentPrioritys')
  }
   
}



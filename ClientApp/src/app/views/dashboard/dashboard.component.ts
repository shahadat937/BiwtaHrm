import { Component, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';

import { DashboardChartsData, IChartProps } from './dashboard-charts-data';
import { WidgetsService } from './DashboardWidgets/service/widgets.service';
import { Subscription } from 'rxjs';
import { RoleDashboardService } from '../usermanagement/service/role-dashboard.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { RoleDashboard } from '../usermanagement/model/role-dashboard';
import { ToastrService } from 'ngx-toastr';
import * as CryptoJS from 'crypto-js';

interface IUser {
  name: string;
  state: string;
  registered: string;
  country: string;
  usage: number;
  period: string;
  payment: string;
  activity: string;
  avatar: string;
  status: string;
  color: string;
}

@Component({
  templateUrl: 'dashboard.component.html',
  styleUrls: ['dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
  
  subscription: Subscription[]=[];
  dashboardPermission = new RoleDashboard();

  constructor(
    private roleDashboardService: RoleDashboardService,
    private authService: AuthService,
    private toastr: ToastrService,){
  }

  ngOnInit(): void {
    const currentEncryptedUser =  localStorage.getItem('encryptedUser');
    if(currentEncryptedUser){
      const bytes = CryptoJS.AES.decrypt(currentEncryptedUser, 'secret-key');
      const roleName = JSON.parse(bytes.toString(CryptoJS.enc.Utf8)).role;
      this.getRolePermission(roleName);
    }
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  getRolePermission(roleName: string){
    this.subscription.push(this.roleDashboardService.getRoleDashboardPermission(roleName).subscribe((res) => {
      if(res){
        this.dashboardPermission = res;
      }
      else{
        this.toastr.warning('', 'Permission Not Found', {
          positionClass: 'toast-top-right',
        });
      }
    }))
  }

}

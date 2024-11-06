import { Component, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';

import { DashboardChartsData, IChartProps } from './dashboard-charts-data';
import { WidgetsService } from './DashboardWidgets/service/widgets.service';
import { Subscription } from 'rxjs';
import { RoleDashboardService } from '../usermanagement/service/role-dashboard.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { RoleDashboard } from '../usermanagement/model/role-dashboard';

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
  
  subscription: Subscription = new Subscription();
  dashboardPermission = new RoleDashboard();

  constructor(
    private roleDashboardService: RoleDashboardService,
    private authService: AuthService,){
  }

  ngOnInit(): void {
    this.subscription = this.authService.currentUser.subscribe(data => {
      if(data.role!=null) {
        this.getRolePermission(data.role);
      }
    });

    
  }

  ngOnDestroy(): void {
    if(this.subscription!=null) {
      this.subscription.unsubscribe();
    }
  }

  getRolePermission(roleName: string){
    this.subscription = this.roleDashboardService.getRoleDashboardPermission(roleName).subscribe((res) => {
      this.dashboardPermission = res;
    })
  }

}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { RoleDashboard } from '../model/role-dashboard';
import { RoleDashboardService } from '../service/role-dashboard.service';

@Component({
  selector: 'app-role-dashboard',
  templateUrl: './role-dashboard.component.html',
  styleUrl: './role-dashboard.component.scss'
})
export class RoleDashboardComponent implements OnInit, OnDestroy {
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  roleDashboard: RoleDashboard[] = [];
  RoleDashboardsForm: FormGroup;
  loading: boolean = false;

  constructor(
    public roleDashboardService: RoleDashboardService,
    private fb: FormBuilder,
    private toastr: ToastrService,
  ) {
    this.RoleDashboardsForm = this.fb.group({
      roleDashboardsList: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.getAllRoleDashboardList();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  getAllRoleDashboardList(){
    this.subscription.push(
    this.roleDashboardService.getRoleDashboardList().subscribe((res) => {
      if (res.length > 0) {
        this.roleDashboard = res;
        this.pathRoleDashboardList(res);
      }
      else{
        this.roleDashboard = [];
      }
    })
    )
    
  }

  get RoleDashboardListArray() {
    return this.RoleDashboardsForm.get('roleDashboardsList') as FormArray;
  }

  pathRoleDashboardList(roleDashboardInfoList: RoleDashboard[]) {
    const control = this.RoleDashboardListArray;
    control.clear();

    roleDashboardInfoList.forEach(dashboardInfo => {
      control.push(this.fb.group({
        id: [dashboardInfo.id],
        roleId: [dashboardInfo.roleId],
        roleName: [dashboardInfo.roleName],
        dashboardPermission: [dashboardInfo.dashboardPermission === true],
        empDashboardPermission: [dashboardInfo.empDashboardPermission === true],
        isActive: [dashboardInfo.isActive === true],
      }));
    });

    this.RoleDashboardsForm.markAsDirty();
  }

  submit(){
    this.loading = true;
    this.subscription.push(
      this.roleDashboardService.saveRoleDashboard(this.RoleDashboardsForm.get("roleDashboardsList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
    }))
    )
    
  }
}

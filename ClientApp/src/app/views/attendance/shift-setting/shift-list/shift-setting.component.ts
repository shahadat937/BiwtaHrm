import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { RoleFeatureService } from 'src/app/views/featureManagement/service/role-feature.service';
import { ShiftSettingService } from '../../services/shift-setting.service';
import { TreeShift } from '../../models/tree-shift';

@Component({
  selector: 'app-shift-setting',
  templateUrl: './shift-setting.component.html',
  styleUrl: './shift-setting.component.scss',
})
export class ShiftSettingComponent implements OnInit, OnDestroy {

  icons = { cilArrowLeft, cilPlus, cilBell };
  subscription: Subscription[]=[];
  expandedRows = {};

  treeShiftInfo : TreeShift[] = [];

  constructor(
      private route: ActivatedRoute,
      private router: Router,
      private confirmService: ConfirmService,
      private toastr: ToastrService,
      public roleFeatureService: RoleFeatureService,
      public shiftSettingService: ShiftSettingService,
    ) {
  }
  
  ngOnInit(): void {
    this.getPermission();
  }


  
  getPermission(){
    this.roleFeatureService.getFeaturePermission('manageShift').subscribe((item) => {
      if(item.viewStatus == true){
        this.getTreeShiftInfo();
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    });
  }

  getTreeShiftInfo(){
    this.subscription.push(this.shiftSettingService.getTreeShiftType().subscribe((res) => {
      this.treeShiftInfo = res;
    }))
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }
  

}

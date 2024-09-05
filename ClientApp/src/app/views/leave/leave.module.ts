import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule as TableModuleN } from 'primeng/table';
import {
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseModule,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ProgressModule,
  SharedModule,
  TableModule,
  UtilitiesModule,
  PaginationModule,
  SpinnerModule
} from '@coreui/angular';

import { LeaveRoutingModule } from './leave-routing.module';
import { AddleaveComponent } from './addleave/addleave.component';
import { ManageleaveComponent } from './manageleave/manageleave.component';
import { BrowserModule } from '@angular/platform-browser';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { LeaveDetailViewComponent } from './manageleave/leave-detail-view/leave-detail-view.component';
import { ManageLeaveService } from './service/manage-leave.service';
import { LeaveService } from './service/leave.service';
import { AddLeaveService } from './service/add-leave.service';
import {LeaveBalanceService} from './service/leave-balance.service'
import { ReviewLeaveComponent } from './review-leave/review-leave.component';
import { FinalApprovalComponent } from './final-approval/final-approval.component';
import { PersonalLeaveComponent } from './personal-leave/personal-leave.component';
import { LeaveBalanceComponent } from './leave-balance/leave-balance.component';

@NgModule({
  declarations: [
    AddleaveComponent,
    ManageleaveComponent,
    LeaveDetailViewComponent,
    ReviewLeaveComponent,
    FinalApprovalComponent,
    PersonalLeaveComponent,
    LeaveBalanceComponent
  ],
  imports: [
    CommonModule,
    LeaveRoutingModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    ProgressModule,
    CollapseModule,
    TableModule,
    UtilitiesModule,
    PaginationModule,
    SharedCustomModule,
    SpinnerModule,
    TableModuleN
  ],providers:[
    ManageLeaveService,
    LeaveService,
    AddLeaveService,
    LeaveBalanceService
  ]
})
export class LeaveModule { }

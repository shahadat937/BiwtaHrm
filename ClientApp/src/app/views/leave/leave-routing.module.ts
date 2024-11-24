import { ManageleaveComponent } from './manageleave/manageleave.component';
import { AddleaveComponent } from './addleave/addleave.component';
import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ReviewLeaveComponent } from './review-leave/review-leave.component'
import {FinalApprovalComponent} from './final-approval/final-approval.component'
import {PersonalLeaveComponent} from './personal-leave/personal-leave.component'
import {LeaveBalanceComponent} from './leave-balance/leave-balance.component'
import { OldLeaveEntryComponent } from './old-leave-entry/old-leave-entry.component';

const routes: Routes = [
  {
    path:'addleave',
    component:AddleaveComponent
  },
  {
    path:'manageleave',
    component:ManageleaveComponent
  },
  {
    path: 'reviewleave',
    component: ReviewLeaveComponent
  },
  {
    path: 'finalapprove',
    component: FinalApprovalComponent
  },
  {
    path: 'personalleave',
    component: PersonalLeaveComponent
  },
  {
    path: 'leavebalance',
    component: LeaveBalanceComponent
  },
  {
    path: 'old-leave-entry',
    component: OldLeaveEntryComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveRoutingModule { }

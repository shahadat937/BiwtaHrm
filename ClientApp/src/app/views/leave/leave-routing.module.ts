import { ManageleaveComponent } from './manageleave/manageleave.component';
import { AddleaveComponent } from './addleave/addleave.component';
import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReviewLeaveComponent } from './review-leave/review-leave.component'
import {FinalApprovalComponent} from './final-approval/final-approval.component'
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
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveRoutingModule { }

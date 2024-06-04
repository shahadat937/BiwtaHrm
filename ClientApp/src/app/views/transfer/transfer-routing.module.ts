import { DepartmetnReleaseComponent } from './departmetn-release/departmetn-release.component';
import { ReleaseComponent } from './release/release.component';
import { PostingComponent } from './posting/posting.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmpModalComponent } from './emp-modal/emp-modal.component';
import { TransferlistComponent } from './transferlist/transferlist.component';
import { getPositionOfLineAndCharacter } from 'typescript';
import { TransferPostingHistoryComponent } from './transfer-posting-history/transfer-posting-history.component';
import { TransferApprovedComponent } from './transfer-approved/transfer-approved.component';
import { TransferApprovedListComponent } from './transfer-approved-list/transfer-approved-list.component';
import { DepartmetnReleaseListComponent } from './departmetn-release-list/departmetn-release-list.component';
import { DeptReleaseInfo } from '../basic-setup/model/dept-release-info';
import { EmployePostingJoinListComponent } from './employe-posting-join-list/employe-posting-join-list.component';
import { EmployePostingJoinApproveComponent } from './employe-posting-join-approve/employe-posting-join-approve.component';


const routes: Routes = [
  {
    path:'postingOrderInfo',
    component:PostingComponent
  },
  { path: 'update-postingOrderInfo/:postingOrderInfoId',
    component: PostingComponent,
  },
  {
    path:'release',
    component:ReleaseComponent
  },
  {
    path:'EmpModal',
    component:EmpModalComponent
  },
  {
    path:'TransferOrderList',
    component:TransferlistComponent
  },
  {
    path: 'update-TransferOrderList/:postingOrderInfoId',
    component: PostingComponent,
  },
  {
    path:'transferApproveInfoList',
    component:TransferApprovedListComponent
  },
  {
    path:'transferApproveInfo/:postingOrderInfoId',
    component:TransferApprovedComponent
  },

  {
    path:'transferApproveInfo',
    component:TransferApprovedComponent
  },
  {
    path:'transferApproveInfoList',
    component:TransferApprovedListComponent
  },
  {
    path:'transferApproveInfoList/:transferApproveInfoId',
    component:TransferApprovedListComponent
  },
  {
    path:'departmetnReleaseList',
    component:DepartmetnReleaseListComponent
  },
  // {
  //   path:'departmetnRelease/:transferApproveInfoId',
  //   component:DepartmetnReleaseComponent
  // },
  {
    path:'employePostingJoinList',
    component:EmployePostingJoinListComponent
  },
  {
    path:'employePostingJoin/:depReleaseInfoId',
    component:EmployePostingJoinApproveComponent
  },
{
  path:"transferhistory",
  component:TransferPostingHistoryComponent
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransferRoutingModule { }

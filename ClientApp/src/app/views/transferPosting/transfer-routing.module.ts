import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrainsferPostingListComponent } from './trainsfer-posting-list/trainsfer-posting-list.component';
import { TransferPostingApplicationComponent } from './transfer-posting-application/transfer-posting-application.component';
import { TransferPostingApprovalListComponent } from './approval/transfer-posting-approval-list/transfer-posting-approval-list.component';
import { DepartmentApprovalListComponent } from './approval/department-approval-list/department-approval-list.component';
import { JoiningReportingListComponent } from './approval/joining-reporting-list/joining-reporting-list.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Transfer and Posting',
    },
    children: [
      {
        path: 'transferPostingList',
        component: TrainsferPostingListComponent,
        data: {
          title: 'Transfer and Posting List',
        },
      },
      {
        path: 'transferPostingApplication',
        component: TransferPostingApplicationComponent,
        data: {
          title: 'Transfer and Posting Application',
        },
      },
      {
        path: 'transferPostingApprovalList',
        component: TransferPostingApprovalListComponent,
        data: {
          title: 'Transfer and Posting Approval List',
        },
      },
      {
        path: 'departmentApprovalList',
        component: DepartmentApprovalListComponent,
        data: {
          title: 'Department Approval List',
        },
      },
      {
        path: 'joiningReportingList',
        component: JoiningReportingListComponent,
        data: {
          title: 'Joining Reporting List',
        },
      },
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransferPostingRoutingModule { }

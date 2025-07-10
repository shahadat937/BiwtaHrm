import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeManagementReportingComponent } from './employee-management-reporting/employee-management-reporting.component';
import { EmployeeListReportingComponent } from './employee-list-reporting/employee-list-reporting.component';
import { VacancyReportComponent } from './vacancy-report/vacancy-report.component';
import { TransferPostingReportComponent } from './transfer-posting-report/transfer-posting-report.component';
import { AddressReportingComponent } from './address-reporting/address-reporting.component';
import { LeaveReportingComponent } from './leave-reporting/leave-reporting.component';
import { PrlRetirementReportingComponent } from './prl-retirement-reporting/prl-retirement-reporting.component';



const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Reporting',
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard',
      },
      {
        path: 'employee-management',
        component: EmployeeManagementReportingComponent,
        data: {
          title: 'Employee Management Reporting',
        },
      },
      {
        path: 'employee-list',
        component: EmployeeListReportingComponent,
        data: {
          title: 'Employee Reporting Reporting',
        },
      },
      {
        path: 'vacancy-report',
        component: VacancyReportComponent,
        data: {
          title: 'Vacancy Report',
        },
      },
      {
        path: 'transfer-posting-report',
        component: TransferPostingReportComponent,
        data: {
          title: 'Transfer & Posting Report',
        },
      },
      {
        path: 'address-report',
        component: AddressReportingComponent,
        data: {
          title: 'Address Report',
        },
      },
      {
        path: 'leave-report',
        component: LeaveReportingComponent,
        data: {
          title: 'Leave Report',
        },
      },
      {
        path: 'prl-retirement-report',
        component: PrlRetirementReportingComponent,
        data: {
          title: 'PRL & Retirment Report',
        },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportingRoutingModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeManagementReportingComponent } from './employee-management-reporting/employee-management-reporting.component';



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
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportingRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewUsersComponent } from './add-employee/view-users/view-users.component';
import { ViewInformationListComponent } from './add-employee/view-information-list/view-information-list.component';
import { EmployeeListComponent } from './manage-employee/employee-list/employee-list.component';
import { EmployeeInformationComponent } from './manage-employee/employee-information/employee-information.component';
import { ViewEmployeeComponent } from './add-employee/view-employee/view-employee.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Employee Management',
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'addEmployee',
      },
      {
        path: 'employeeList',
        component: ViewEmployeeComponent,
        data: {
          title: 'Employee List',
        },
      },
      {
        path: 'manageEmployee',
        component: EmployeeListComponent,
        data: {
          title: 'Employee List',
        },
      },
      {
        path: 'create-new-employee',
        component: ViewInformationListComponent,
        data: {
          title: 'Create New Employee',
        },
      },
      {
        path: 'update-employee-information/:id',
        component: ViewInformationListComponent,
        data: {
          title: 'Update Employee Information',
        },
      },
      
      {
        path: 'employee-information/:id',
        component: EmployeeInformationComponent,
        data: {
          title: 'View Employee Information',
        },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }

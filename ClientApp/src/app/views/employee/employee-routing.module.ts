import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageEmployeeComponent } from './manage-employee/manage-employee.component';
import { ViewUsersComponent } from './add-employee/view-users/view-users.component';
import { ViewInformationListComponent } from './add-employee/view-information-list/view-information-list.component';

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
        path: 'addEmployee',
        component: ViewUsersComponent,
        data: {
          title: 'User List',
        },
      },
      {
        path: 'manageEmployee',
        component: ManageEmployeeComponent,
        data: {
          title: 'Manage Employee',
        },
      },
      {
        path: 'view-imformation-list/:id',
        component: ViewInformationListComponent,
        data: {
          title: 'Employee Information List',
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

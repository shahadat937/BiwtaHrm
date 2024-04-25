import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageEmployeeComponent } from './manage-employee/manage-employee.component';
import { ViewUsersComponent } from './add-employee/view-users/view-users.component';
import { ViewInformationListComponent } from './add-employee/view-information-list/view-information-list.component';

const routes: Routes = [
  {
    path: 'addEmployee',
    component: ViewUsersComponent,
  },
  {
    path: 'manageEmployee',
    component: ManageEmployeeComponent,
  },
  {
    path: 'view-imformation-list',
    component: ViewInformationListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }

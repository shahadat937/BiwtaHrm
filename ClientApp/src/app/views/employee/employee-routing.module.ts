import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageEmployeeComponent } from './manage-employee/manage-employee.component';
import { ViewUsersComponent } from './add-employee/view-users/view-users.component';
import { ViewInformationListComponent } from './add-employee/view-information-list/view-information-list.component';
import { BasicInformationComponent } from './add-employee/employee-informations/basic-information/basic-information.component';

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
  {
    path: 'basic-information',
    component: BasicInformationComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }

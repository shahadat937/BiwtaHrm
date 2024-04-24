import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageEmployeeComponent } from './manage-employee/manage-employee.component';

const routes: Routes = [
  {
    path: 'addEmployee',
    component: AddEmployeeComponent,
  },
  {
    path: 'manageEmployee',
    component: ManageEmployeeComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }

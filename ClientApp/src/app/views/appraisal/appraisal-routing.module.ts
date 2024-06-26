import { ManageFormComponent } from './manage-form/manage-form.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StaffFormComponent } from './staff-form/staff-form.component';
import { OfficerFormComponent } from './officer-form/officer-form.component';
import { StaffForm2Component } from './staff-form/staff-form-2/staff-form-2.component';
import { StaffForm3Component } from './staff-form/staff-form-3/staff-form-3.component';

const routes: Routes = [
  {
    path:'staffForm',
    component:StaffFormComponent
  },
  {
    path:'manageForm',
    component:ManageFormComponent
  }
  ,
  {
    path:'officerForm',
    component:OfficerFormComponent
  }
  ,
  {
    path: "staffForm2",
    component:StaffForm2Component
  }
  ,
  {
    path: "staffForm3",
    component:StaffForm3Component
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppraisalRoutingModule { }

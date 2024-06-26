import { ManageFormComponent } from './manage-form/manage-form.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StaffFormComponent } from './staff-form/staff-form.component';
import { OfficerFormComponent } from './officer-form/officer-form.component';
import { OfficerForm2Component } from './officer-form/officer-form-2/officer-form-2.component';

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
  },
  {
    path:'officerForm2',
    component:OfficerForm2Component
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppraisalRoutingModule { }

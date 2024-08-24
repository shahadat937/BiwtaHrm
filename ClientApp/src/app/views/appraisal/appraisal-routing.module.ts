
import { ManageFormComponent } from './manage-form/manage-form.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StaffFormComponent } from './staff-form/staff-form.component';
import { OfficerFormComponent } from './officer-form/officer-form.component';
import { OfficerForm2Component } from './officer-form/officer-form-2/officer-form-2.component';
import { OfficerFormPart3Component } from './officer-form/officer-form-part-3/officer-form-part-3.component';
import { OfficerFormPart5Component } from './officer-form/officer-form-part-5/officer-form-part-5.component';
import { OfficerFormPart4Component } from './officer-form/officer-form-part-4/officer-form-part-4.component';
import { OfficerFormPart6Component } from './officer-form/officer-form-part-6/officer-form-part-6.component';
import { OfficerFormPart7Component } from './officer-form/officer-form-part-7/officer-form-part-7.component';

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
  },
  {
    path:'officerFormPart3',
    component:OfficerFormPart3Component
  },
  {
    path:'officerFormPart4',
    component:OfficerFormPart4Component
  },
  {
    path:'officerFormPart5',
    component:OfficerFormPart5Component
  },
  {
    path:'officerFormPart6',
    component:OfficerFormPart6Component
  },
  {
    path:'officerFormPart7',
    component:OfficerFormPart7Component
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppraisalRoutingModule { }

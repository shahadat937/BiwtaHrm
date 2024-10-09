
import { ManageFormComponent } from './manage-form/manage-form.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StaffFormComponent } from './staff-form/staff-form.component';
import { OfficerFormComponent } from './officer-form/officer-form.component';
import { OfficerFormApplicationComponent } from './officer-form/officer-form-application/officer-form-application.component';
import { ReportingFormComponent } from './officer-form/reporting-form/reporting-form.component';
import { CounterSignatureFormOfficerComponent } from './officer-form/counter-signature-form-officer/counter-signature-form-officer.component';
import { ReceiverFormOfficerComponent } from './officer-form/receiver-form-officer/receiver-form-officer.component';


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
    path: 'apply',
    component: OfficerFormApplicationComponent
  },
  {
    path: "reportingFormOfficer/:formRecordId",
    component: ReportingFormComponent
  },
  {
    path: 'counterSignatureFormOfficer/:formRecordId',
    component: CounterSignatureFormOfficerComponent
  },
  {
    path: 'receiverFormOfficer/:formRecorcId',
    component: ReceiverFormOfficerComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppraisalRoutingModule { }

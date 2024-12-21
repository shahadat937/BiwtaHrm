import { ManageFormComponent } from './manage-form/manage-form.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StaffFormComponent } from './staff-form/staff-form.component';
import { OfficerFormComponent } from './officer-form/officer-form.component';
import { OfficerFormApplicationComponent } from './officer-form/officer-form-application/officer-form-application.component';
import { ReportingFormComponent } from './officer-form/reporting-form/reporting-form.component';
import { CounterSignatureFormOfficerComponent } from './officer-form/counter-signature-form-officer/counter-signature-form-officer.component';
import { ReceiverFormOfficerComponent } from './officer-form/receiver-form-officer/receiver-form-officer.component';
import {ManageFormOfficerRfComponent} from './manage-form/manage-form-officer-rf/manage-form-officer-rf.component'
import { ManageFormOfficerCsComponent } from './manage-form/manage-form-officer-cs/manage-form-officer-cs.component';
import { ManageFormOfficerRComponent } from './manage-form/manage-form-officer-r/manage-form-officer-r.component';
import { ApplyComponent } from './apply/apply.component';
import {MyFormRecordComponent} from './manage-form/my-form-record/my-form-record.component';


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
    component: ApplyComponent
  },
  {
    path: "reportingFormOfficer/:formRecordId/:formId",
    component: ReportingFormComponent
  },
  {
    path: 'counterSignatureFormOfficer/:formRecordId/:formId',
    component: CounterSignatureFormOfficerComponent
  },
  {
    path: 'receiverFormOfficer/:formRecordId/:formId',
    component: ReceiverFormOfficerComponent
  },
   {
    path: 'manageFormOfficerRF',
    component: ManageFormOfficerRfComponent
   },
   {
    path: 'manageFormOfficerCs',
    component: ManageFormOfficerCsComponent
   },
   {
    path: 'manageFormOfficerR',
    component: ManageFormOfficerRComponent
   },
   {
    path: 'MyFormRecord',
    component: MyFormRecordComponent
   }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppraisalRoutingModule { }

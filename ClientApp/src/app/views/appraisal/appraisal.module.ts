import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseDirective,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ProgressModule,
  SharedModule,
  TableModule, 
  UtilitiesModule
} from '@coreui/angular';

import { AppraisalRoutingModule } from './appraisal-routing.module';
import { StaffFormComponent } from './staff-form/staff-form.component';
import { ManageFormComponent } from './manage-form/manage-form.component';
import { OfficerFormComponent } from './officer-form/officer-form.component';
import { OfficerForm2Component } from './officer-form/officer-form-2/officer-form-2.component';


@NgModule({
  declarations: [
    StaffFormComponent,
    ManageFormComponent,
    OfficerFormComponent,
    OfficerForm2Component
  ],
  imports: [
    CommonModule,
    AppraisalRoutingModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    FormModule,
    ButtonModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    ProgressModule,
    CollapseDirective,
    TableModule,
    UtilitiesModule
  ]
})
export class AppraisalModule { }

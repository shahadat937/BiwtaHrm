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
} from '@coreui/angular';

import { AppraisalRoutingModule } from './appraisal-routing.module';
import { StaffFormComponent } from './staff-form/staff-form.component';
import { ManageFormComponent } from './manage-form/manage-form.component';
import { OfficerFormComponent } from './officer-form/officer-form.component';
import { StaffForm2Component } from './staff-form/staff-form-2/staff-form-2.component';
import { StaffForm3Component } from './staff-form/staff-form-3/staff-form-3.component';

@NgModule({
  declarations: [
    StaffFormComponent,
    ManageFormComponent,
    OfficerFormComponent,
    StaffForm2Component,
    StaffForm3Component
  ],
  imports: [
    CommonModule,
    AppraisalRoutingModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    ProgressModule,
    CollapseDirective,
    TableModule
  ]
})
export class AppraisalModule { }

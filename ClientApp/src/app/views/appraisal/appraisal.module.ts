import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabViewModule } from 'primeng/tabview';
import { StepperModule } from 'primeng/stepper';
import { IconDirective } from '@coreui/icons-angular';
import { TableModule as TableModulePN } from 'primeng/table';
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
  UtilitiesModule,
  PaginationModule,
  SpinnerModule,
} from '@coreui/angular';

import { FormsModule }   from '@angular/forms';
import { AppraisalRoutingModule } from './appraisal-routing.module';
import { StaffFormComponent } from './staff-form/staff-form.component';
import { ManageFormComponent } from './manage-form/manage-form.component';
import { OfficerFormComponent } from './officer-form/officer-form.component';
import { OfficerForm2Component } from './officer-form/officer-form-2/officer-form-2.component';
import { OfficerFormPart3Component } from './officer-form/officer-form-part-3/officer-form-part-3.component';
import { OfficerFormPart4Component } from './officer-form/officer-form-part-4/officer-form-part-4.component';
import { OfficerFormPart5Component } from './officer-form/officer-form-part-5/officer-form-part-5.component';
import { OfficerFormPart6Component } from './officer-form/officer-form-part-6/officer-form-part-6.component';
import { OfficerFormPart7Component } from './officer-form/officer-form-part-7/officer-form-part-7.component';
import { FieldComponent } from './field/field.component';
import { UpdateFormComponent } from './update-form/update-form.component';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations: [
    StaffFormComponent,
    ManageFormComponent,
    OfficerFormComponent,
    OfficerForm2Component,
    OfficerForm2Component,
    OfficerFormPart3Component,
    OfficerFormPart4Component,
    OfficerFormPart5Component,
    OfficerFormPart6Component,
    OfficerFormPart7Component,
    FieldComponent,
    UpdateFormComponent
  ],
  imports: [
    TabViewModule,
    CommonModule,
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
    UtilitiesModule,
    PaginationModule,
    SpinnerModule,
    FormsModule,
    AppraisalRoutingModule,
    StepperModule,
    TableModulePN,
    IconDirective,
    MatIconModule

  ]
})
export class AppraisalModule { }

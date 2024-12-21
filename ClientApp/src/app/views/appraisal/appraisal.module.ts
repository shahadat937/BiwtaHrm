import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabViewModule } from 'primeng/tabview';
import { StepperModule } from 'primeng/stepper';
import { IconDirective } from '@coreui/icons-angular';
import { TableModule as TableModulePN } from 'primeng/table';
import {CalendarModule} from 'primeng/calendar'
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
import { FieldComponent } from './field/field.component';
import { UpdateFormComponent } from './update-form/update-form.component';
import {MatIconModule} from '@angular/material/icon';
import { ViewFormRecordComponent } from './manage-form/view-form-record/view-form-record.component';
import { FormRecordService } from './services/form-record.service';
import { OfficerFormApplicationComponent } from './officer-form/officer-form-application/officer-form-application.component';
import { ReportingFormComponent } from './officer-form/reporting-form/reporting-form.component';
import { CounterSignatureFormOfficerComponent } from './officer-form/counter-signature-form-officer/counter-signature-form-officer.component';
import { ReceiverFormOfficerComponent } from './officer-form/receiver-form-officer/receiver-form-officer.component';
import { EmpBasicInfoService } from '../employee/service/emp-basic-info.service';
import { ApplicationHeaderComponent } from './application-header/application-header.component';
import { ManageFormOfficerRfComponent } from './manage-form/manage-form-officer-rf/manage-form-officer-rf.component';
import { ManageFormOfficerCsComponent } from './manage-form/manage-form-officer-cs/manage-form-officer-cs.component';
import { ManageFormOfficerRComponent } from './manage-form/manage-form-officer-r/manage-form-officer-r.component';
import { BadgeModule } from '@coreui/angular';
import { ApplyComponent } from './apply/apply.component';
import { GradeService } from '../basic-setup/service/Grade.service';
import { FieldPrintComponent } from './field-print/field-print.component';
import { NgxPrintModule } from 'ngx-print';
import { MyFormRecordComponent } from './manage-form/my-form-record/my-form-record.component';
// import { NgxPrintModule } from 'ngx-print';


@NgModule({
  declarations: [
    StaffFormComponent,
    ManageFormComponent,
    OfficerFormComponent,
    FieldComponent,
    UpdateFormComponent,
    ViewFormRecordComponent,
    OfficerFormApplicationComponent,
    ReportingFormComponent,
    CounterSignatureFormOfficerComponent,
    ReceiverFormOfficerComponent,
    ApplicationHeaderComponent,
    ManageFormOfficerRfComponent,
    ManageFormOfficerCsComponent,
    ManageFormOfficerRComponent,
    ApplyComponent,
    FieldPrintComponent,
    MyFormRecordComponent,
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
    MatIconModule,
    CalendarModule,
    BadgeModule,
    NgxPrintModule

  ],providers:[
    FormRecordService,
    EmpBasicInfoService,
    GradeService
  ]
})
export class AppraisalModule { }

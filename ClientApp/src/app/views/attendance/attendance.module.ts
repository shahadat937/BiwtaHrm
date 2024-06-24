import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AttendanceRoutingModule } from './attendance-routing.module';
import { ManageShiftComponent } from './manage-shift/manage-shift.component';
import { WorkdaySettingComponent } from './workday-setting/workday-setting.component';
import { AttendanceRecordComponent } from './attendance-record/attendance-record.component';
import { ManualAttendanceComponent } from './manual-attendance/manual-attendance.component';
import { SiteVisitComponent } from './site-visit/site-visit.component';
import { AttendanceReportComponent } from './attendance-report/attendance-report.component';
import { DocsComponentsModule } from '@docs-components/docs-components.module';
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
  SpinnerModule,
} from '@coreui/angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { ToastrService } from 'ngx-toastr';
import { ShiftService } from './services/shift.service';


@NgModule({
  declarations: [
    ManageShiftComponent,
    WorkdaySettingComponent,
    AttendanceRecordComponent,
    ManualAttendanceComponent,
    SiteVisitComponent,
    AttendanceReportComponent
  ],
  imports: [
    CommonModule,
    AttendanceRoutingModule,
    DocsComponentsModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
    FormModule,
    ButtonModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    SharedCustomModule,
    ProgressModule,
    SpinnerModule,
    CollapseDirective,
  ],
  providers: [ 
    ToastrService,
    ShiftService,
  ],
})
export class AttendanceModule { }

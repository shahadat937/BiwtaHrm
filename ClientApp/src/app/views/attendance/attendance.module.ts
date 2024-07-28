import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AttendanceRoutingModule } from './attendance-routing.module';
import { ManageShiftComponent } from './manage-shift/manage-shift.component';
import { WorkdaySettingComponent } from './workday-setting/workday-setting.component';
import { AttendanceRecordComponent } from './attendance-record/attendance-record.component';
import { ManualAttendanceComponent } from './manual-attendance/manual-attendance.component';
import { SiteVisitComponent } from './site-visit/site-visit.component';
import { AttendanceReportComponent } from './attendance-report/attendance-report.component';
import {AttendanceSummaryComponent} from './attendance-summary/attendance-summary.component'
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { ButtonModule as PButtonModule,ButtonDirective } from 'primeng/button';
import {CalendarModule} from 'primeng/calendar';
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
  TabsModule,
  TabContentComponent,
  TabPaneComponent
} from '@coreui/angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { ToastrService } from 'ngx-toastr';
import { ShiftService } from './services/shift.service';
import { ManualAttendanceService } from './services/manual-attendance.service';
import {AttendanceRecordService} from './services/attendance-record-service.service';
import {AttendanceReportService} from './services/attendance-report.service'
import {TimeFormatPipe} from './pipes/time-format.pipe'
import { TableModule } from 'primeng/table';
import { TooltipModule } from '@coreui/angular';
import { BadgeModule } from '@coreui/angular';




@NgModule({
  declarations: [
    ManageShiftComponent,
    WorkdaySettingComponent,
    AttendanceRecordComponent,
    ManualAttendanceComponent,
    SiteVisitComponent,
    AttendanceReportComponent,
    AttendanceSummaryComponent,
    TimeFormatPipe
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
    TabsModule,
    TabContentComponent,
    TabPaneComponent,
    PButtonModule,
    ButtonDirective,
    CalendarModule,
    TableModule, 
    TooltipModule,
    BadgeModule

  ],
  providers: [ 
    ToastrService,
    ShiftService,
    ManualAttendanceService,
    AttendanceReportService
  ],
})
export class AttendanceModule { }

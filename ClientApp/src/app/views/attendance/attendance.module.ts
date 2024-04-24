import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AttendanceRoutingModule } from './attendance-routing.module';
import { ManageShiftComponent } from './manage-shift/manage-shift.component';
import { WorkdaySettingComponent } from './workday-setting/workday-setting.component';
import { AttendanceRecordComponent } from './attendance-record/attendance-record.component';
import { ManualAttendanceComponent } from './manual-attendance/manual-attendance.component';
import { SiteVisitComponent } from './site-visit/site-visit.component';
import { AttendanceReportComponent } from './attendance-report/attendance-report.component';


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
    AttendanceRoutingModule
  ]
})
export class AttendanceModule { }

import { AttendanceReportComponent } from './attendance-report/attendance-report.component';
import { SiteVisitComponent } from './site-visit/site-visit.component';
import { ManualAttendanceComponent } from './manual-attendance/manual-attendance.component';
import { AttendanceRecordComponent } from './attendance-record/attendance-record.component';
import { WorkdaySettingComponent } from './workday-setting/workday-setting.component';
import { ManageShiftComponent } from './manage-shift/manage-shift.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AttendanceSummaryComponent } from './attendance-summary/attendance-summary.component';
import { ManageSiteVisitComponent } from './manage-site-visit/manage-site-visit.component';
import { ShiftSettingComponent } from './shift-setting/shift-list/shift-setting.component';

const routes: Routes = [
  {
    path: 'manageShift',
    component: ManageShiftComponent,
  },

  {
    path: 'update-shift/:shiftId',
    component: ManageShiftComponent,
  },

  {
    path: 'workdaySetting',
    component: WorkdaySettingComponent,
  },
  {
    path: 'attendanceRecord',
    component: AttendanceRecordComponent,
  },
  {
    path: 'manualAttendance',
    component: ManualAttendanceComponent,
  },
  {
    path: 'siteVisit',
    component: SiteVisitComponent,
  },
  {
    path: 'manageSiteVisit',
    component: ManageSiteVisitComponent
  },
  {
    path: 'attendanceReport',
    component: AttendanceReportComponent,
  },
  {
    path: 'attendanceSummary',
    component: AttendanceSummaryComponent
  },
  {
    path: 'shift-setting',
    component: ShiftSettingComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AttendanceRoutingModule { }

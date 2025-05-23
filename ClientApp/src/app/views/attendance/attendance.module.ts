import { NgModule } from '@angular/core';
import { CommonModule  } from '@angular/common';
import { AttendanceRoutingModule } from './attendance-routing.module';
import { ManageShiftComponent } from './manage-shift/manage-shift.component';
import { WorkdaySettingComponent } from './workday-setting/workday-setting.component';
import { AttendanceRecordComponent } from './attendance-record/attendance-record.component';
import { ManualAttendanceComponent } from './manual-attendance/manual-attendance.component';
import { SiteVisitComponent } from './site-visit/site-visit.component';
import { AttendanceReportComponent } from './attendance-report/attendance-report.component';
import {AttendanceSummaryComponent} from './attendance-summary/attendance-summary.component';
import {HolidaySetupComponent} from './workday-setting/holiday-setup/holiday-setup.component'
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
import { AttendanceReportEmpService } from './services/attendance-report-emp.service';
import { HolidaySetupService } from './services/holiday-setup.service';
import { SiteVisitService } from './services/site-visit.service';
import { WorkdayService } from './services/workday.service';
import { ManageSiteVisitComponent } from './manage-site-visit/manage-site-visit.component';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { DropdownModule as PDropDownModule } from 'primeng/dropdown';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { SectionService } from '../basic-setup/service/section.service';
import { DepartmentService } from '../basic-setup/service/department.service';
import { MatTableModule } from '@angular/material/table';
import { PrintAttReportComponent } from './attendance-report/print-att-report/print-att-report.component';
import {NgxPrintModule} from 'ngx-print'
import { LeaveTypeService } from '../basic-setup/service/leave-type.service';
import { PrintEmpAttReportComponent } from './attendance-summary/print-emp-att-report/print-emp-att-report.component';
import {ShiftSettingComponent} from './shift-setting/shift-list/shift-setting.component';
import { ShiftTypeModalComponent } from './shift-setting/shift-modal/shift-type-modal/shift-type-modal.component';
import { ShiftSettingModalComponent } from './shift-setting/shift-modal/shift-setting-modal/shift-setting-modal.component';
import { UpdateAttendanceQueryComponent } from './update-attendance-query/update-attendance-query.component';



@NgModule({
  declarations: [
    ManageShiftComponent,
    WorkdaySettingComponent,
    AttendanceRecordComponent,
    ManualAttendanceComponent,
    SiteVisitComponent,
    AttendanceReportComponent,
    AttendanceSummaryComponent,
    HolidaySetupComponent,
    TimeFormatPipe,
    ManageSiteVisitComponent,
    PrintAttReportComponent,
    PrintEmpAttReportComponent,
    ShiftSettingComponent,
    ShiftTypeModalComponent,
    ShiftSettingModalComponent,
    UpdateAttendanceQueryComponent
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
    BadgeModule,
    InputTextModule,
    MultiSelectModule,
    PDropDownModule,
    MatTableModule,
    MatSortModule,
    NgxPrintModule

  ],
  providers: [ 
    ToastrService,
    ShiftService,
    ManualAttendanceService,
    AttendanceReportService,
    AttendanceRecordService,
    AttendanceReportEmpService,
    HolidaySetupService,
    SiteVisitService,
    WorkdayService,
    SectionService,
    DepartmentService,
    LeaveTypeService
  ],
})
export class AttendanceModule { }

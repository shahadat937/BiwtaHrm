import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CardModule, FormModule, GridModule, ButtonGroupModule, ListGroupModule, ProgressModule, SpinnerModule, CollapseDirective, BadgeModule, WidgetStatFComponent, InputGroupComponent, ButtonDirective } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxPrintModule } from 'ngx-print';
import { SharedModule } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { ReportingRoutingModule } from './reporting-routing.module';
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { EmployeeManagementReportingComponent } from './employee-management-reporting/employee-management-reporting.component';
import { EmployeeListReportingComponent } from './employee-list-reporting/employee-list-reporting.component';
import { VacancyReportComponent } from './vacancy-report/vacancy-report.component';
import { TransferPostingReportComponent } from './transfer-posting-report/transfer-posting-report.component';
import { AddressReportingComponent } from './address-reporting/address-reporting.component';
import { CountryService } from '../basic-setup/service/Country.service';
import { DepartmentService } from '../basic-setup/service/department.service';
import { DesignationService } from '../basic-setup/service/designation.service';
import { DistrictService } from '../basic-setup/service/district.service';
import { DivisionService } from '../basic-setup/service/division.service';
import { SectionService } from '../basic-setup/service/section.service';
import { UapzilaService } from '../basic-setup/service/uapzila.service';
import { LeaveReportingComponent } from './leave-reporting/leave-reporting.component';
import { PrlRetirementReportingComponent } from './prl-retirement-reporting/prl-retirement-reporting.component';
import { CalendarModule } from 'primeng/calendar';



@NgModule({
  declarations: [
    EmployeeManagementReportingComponent,
    EmployeeListReportingComponent,
    VacancyReportComponent,
    TransferPostingReportComponent,
    AddressReportingComponent,
    LeaveReportingComponent,
    PrlRetirementReportingComponent
  ],
  imports: [
    CommonModule,
    ReportingRoutingModule,
    DocsComponentsModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
    FormModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    SharedCustomModule,
    ProgressModule,
    SpinnerModule,
    ModalModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatTableModule,
    CardModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatListModule,
    MatBottomSheetModule,
    MatDialogModule,
    CollapseDirective,
    IconModule,
    TableModule,
    MatCardModule,
    TooltipModule,
    MatTabsModule,
    TagModule, 
    IconFieldModule, 
    InputTextModule, 
    InputIconModule, 
    MultiSelectModule, 
    HttpClientModule, 
    DropdownModule,
    ScrollingModule,
    BadgeModule,
    NgxPrintModule,
    WidgetStatFComponent,
    InputGroupComponent,
    ButtonDirective,
    CalendarModule
  ],
  providers:
  [
    DepartmentService,
    SectionService,
    DesignationService,
    CountryService,
    UapzilaService,
    DistrictService,
    DivisionService,
  ],
})
export class ReportingModule { }

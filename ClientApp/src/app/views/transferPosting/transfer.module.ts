import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertComponent, BadgeComponent, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, PopoverModule, SpinnerModule, TableModule, TooltipModule } from '@coreui/angular';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import {CollapseDirective} from '@coreui/angular';

import {
  ButtonGroupModule,ButtonModule,CardModule,DropdownModule,FormModule, GridModule, ListGroupModule, 
  ProgressModule,SharedModule,} from '@coreui/angular';
import { FormsModule } from '@angular/forms';
import { TransferPostingRoutingModule } from './transfer-routing.module';
import { TrainsferPostingListComponent } from './trainsfer-posting-list/trainsfer-posting-list.component';
import { TransferPostingApplicationComponent } from './transfer-posting-application/transfer-posting-application.component';
import { TransferPostingApprovalComponent } from './approval/transfer-posting-approval/transfer-posting-approval.component';
import { DepartmentApprovalComponent } from './approval/department-approval/department-approval.component';
import { IconModule } from '@coreui/icons-angular';
import { DepartmentApprovalListComponent } from './approval/department-approval-list/department-approval-list.component';
import { TransferPostingApprovalListComponent } from './approval/transfer-posting-approval-list/transfer-posting-approval-list.component';
import { JoiningReportingComponent } from './approval/joining-reporting/joining-reporting.component';
import { JoiningReportingListComponent } from './approval/joining-reporting-list/joining-reporting-list.component';
import { TransferPostingInfoComponent } from './transfer-posting-info/transfer-posting-info.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { OfficeService } from '../basic-setup/service/office.service';
import { DepartmentService } from '../basic-setup/service/department.service';
import { GradeService } from '../basic-setup/service/Grade.service';
import { SectionService } from '../basic-setup/service/section.service';
import { ReleaseTypeService } from '../basic-setup/service/release-type.service';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  declarations: [
    TrainsferPostingListComponent,
    TransferPostingApplicationComponent,
    TransferPostingApprovalComponent,
    DepartmentApprovalComponent,
    DepartmentApprovalListComponent,
    TransferPostingApprovalListComponent,
    JoiningReportingComponent,
    JoiningReportingListComponent,
    TransferPostingInfoComponent,

  ],
  imports: [
    MatCardModule,
    SpinnerModule,
    DropdownModule,
    FormModule,
    GridModule,
    ListGroupModule,
    ProgressModule,
    SharedModule,
    ButtonGroupModule,
    ButtonModule,
    CardModule,
    CommonModule,
    TransferPostingRoutingModule,
    MatFormFieldModule,
    MatTableModule,
    FormsModule,
    MatPaginatorModule,
    MatInputModule,
    CollapseDirective,
    ModalBodyComponent,
    CommonModule,
    ModalComponent,
    ModalFooterComponent,
    ModalHeaderComponent,
    IconModule,
    MatIconModule,
    MatButtonModule,
    TableModule,
    PopoverModule,
    AlertComponent,
    TooltipModule,
    BadgeComponent,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
  ],
  providers:[
    OfficeService,
    DepartmentService,
    GradeService,
    SectionService,
    ReleaseTypeService,
  ],
  bootstrap: []
})
export class TransferPostingModule {
  
 }

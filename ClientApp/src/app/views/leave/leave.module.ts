import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule as TableModuleN } from 'primeng/table';
import {
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseModule,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ProgressModule,
  SharedModule,
  TableModule,
  UtilitiesModule,
  PaginationModule,
  SpinnerModule
} from '@coreui/angular';

import { LeaveRoutingModule } from './leave-routing.module';
import { AddleaveComponent } from './addleave/addleave.component';
import { ManageleaveComponent } from './manageleave/manageleave.component';
import { BrowserModule } from '@angular/platform-browser';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { LeaveDetailViewComponent } from './manageleave/leave-detail-view/leave-detail-view.component';

@NgModule({
  declarations: [
    AddleaveComponent,
    ManageleaveComponent,
    LeaveDetailViewComponent
  ],
  imports: [
    CommonModule,
    LeaveRoutingModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    ProgressModule,
    CollapseModule,
    TableModule,
    UtilitiesModule,
    PaginationModule,
    SharedCustomModule,
    SpinnerModule,
    TableModuleN
  ]
})
export class LeaveModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
  PaginationModule
} from '@coreui/angular';

import { LeaveRoutingModule } from './leave-routing.module';
import { AddleaveComponent } from './addleave/addleave.component';
import { ManageleaveComponent } from './manageleave/manageleave.component';

@NgModule({
  declarations: [
    AddleaveComponent,
    ManageleaveComponent
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
    
  ]
})
export class LeaveModule { }

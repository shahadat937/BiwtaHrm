import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

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
    LeaveRoutingModule
  ]
})
export class LeaveModule { }

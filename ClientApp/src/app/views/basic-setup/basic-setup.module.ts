import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BasicSetupRoutingModule } from './basic-setup-routing.module';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { BloodGroupListComponent } from './bloodgroup/bloodgroup-list/bloodgroup-list.component';
import { ButtonModule } from '@coreui/angular';


@NgModule({
  declarations: [NewAccountTypeComponent,BloodGroupListComponent],
  imports: [
    CommonModule,
    BasicSetupRoutingModule,
SharedCustomModule,

  ]
})
export class BasicSetupModule { }

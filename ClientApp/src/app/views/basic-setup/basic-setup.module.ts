import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BasicSetupRoutingModule } from './basic-setup-routing.module';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { BloodGroupComponent } from './blood-group/blood-group.component';
import { ScaleComponent } from './scale/scale.component';
import {DistrictComponent} from './district/district.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CardModule, FormModule, GridModule, ButtonModule, ButtonGroupModule, DropdownModule, SharedModule, ListGroupModule, ToastModule, ProgressModule } from '@coreui/angular';
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { AppToastComponent } from '../notifications/toasters/toast-simple/toast.component';


@NgModule({
  declarations:
  [
    NewAccountTypeComponent,
    BloodGroupComponent,
    ScaleComponent,
    DistrictComponent,
  ],
  imports: [
    CommonModule,
    BasicSetupRoutingModule,
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
    ToastModule,
    ProgressModule
  ]
})
export class BasicSetupModule { }

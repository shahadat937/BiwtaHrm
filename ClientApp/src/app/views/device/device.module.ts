import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageDeviceComponent } from './manage-device/manage-device.component';
import {SharedCustomModule} from '../../shared/shared.module';
import {DeviceRoutingModule} from './device-routing.module';
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
import { TableModule } from 'primeng/table';
import { BadgeModule } from '@coreui/angular';
import { AddDeviceComponent } from './add-device/add-device.component';
import { DeviceModalComponent } from './device-modal/device-modal.component';
import { AssignEmployeeToDeviceComponent } from './assign-employee-to-device/assign-employee-to-device.component';
import { CustomCommandModalComponent } from './custom-command-modal/custom-command-modal.component';



@NgModule({
  declarations: [
    ManageDeviceComponent,
    AddDeviceComponent,
    DeviceModalComponent,
    AssignEmployeeToDeviceComponent,
    CustomCommandModalComponent
  ],
  imports: [
    CommonModule,
    SharedCustomModule,
    DeviceRoutingModule,
    FormsModule,
    ReactiveFormsModule,
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
    TabPaneComponent,
    TableModule,
    BadgeModule
  ]
})
export class DeviceModule { }

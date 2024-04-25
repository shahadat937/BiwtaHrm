import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ProgressModule,
  SharedModule,
  SpinnerModule,
} from '@coreui/angular';
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { ToastrService } from 'ngx-toastr';

import { SharedCustomModule } from 'src/app/shared/shared.module';

import { EmployeeRoutingModule } from './employee-routing.module';
import { ManageEmployeeComponent } from './manage-employee/manage-employee.component';
import { ViewUsersComponent } from './add-employee/view-users/view-users.component';
import { ViewInformationListComponent } from './add-employee/view-information-list/view-information-list.component';
import { BasicInformationComponent } from './add-employee/employee-informations/basic-information/basic-information.component';
import { PersonalInformationComponent } from './add-employee/employee-informations/personal-information/personal-information.component';


@NgModule({
  declarations: [
    ManageEmployeeComponent,
    ViewUsersComponent,
    ViewInformationListComponent,
    BasicInformationComponent,
    PersonalInformationComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
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
  ]
})
export class EmployeeModule { }

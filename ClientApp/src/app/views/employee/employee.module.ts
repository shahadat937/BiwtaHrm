import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseDirective,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ModalModule,
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
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatListModule } from '@angular/material/list';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FamilyInformationComponent } from './add-employee/employee-informations/family-information/family-information.component';
import { EmpPresentAddressComponent } from './add-employee/employee-informations/emp-present-address/emp-present-address.component';
import { EmpPermanentAddressComponent } from './add-employee/employee-informations/emp-permanent-address/emp-permanent-address.component';
import { CountryService } from '../basic-setup/service/country.service';


@NgModule({
  declarations: [
    ManageEmployeeComponent,
    ViewUsersComponent,
    ViewInformationListComponent,
    BasicInformationComponent,
    PersonalInformationComponent,
    FamilyInformationComponent,
    EmpPresentAddressComponent,
    EmpPermanentAddressComponent,
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
    MatInputModule,
    MatBottomSheetModule,
    MatDialogModule,
    CollapseDirective,
  ],
  providers:
  [
    CountryService,
  ],
})
export class EmployeeModule { }

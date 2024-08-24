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
  TableModule,
  TooltipModule,
} from '@coreui/angular';
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { ToastrService } from 'ngx-toastr';

import { SharedCustomModule } from 'src/app/shared/shared.module';

import { EmployeeRoutingModule } from './employee-routing.module';
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
import { EmpPresentAddressComponent } from './add-employee/employee-informations/emp-present-address/emp-present-address.component';
import { EmpPermanentAddressComponent } from './add-employee/employee-informations/emp-permanent-address/emp-permanent-address.component';
import { CountryService } from '../basic-setup/service/country.service';
import { EmpJobDetailsComponent } from './add-employee/employee-informations/emp-job-details/emp-job-details.component';
import { EmpSpouseInfoComponent } from './add-employee/employee-informations/emp-spouse-info/emp-spouse-info.component';
import { EmpChildInfoComponent } from './add-employee/employee-informations/emp-child-info/emp-child-info.component';
import { EmpEducationInfoComponent } from './add-employee/employee-informations/emp-education-info/emp-education-info.component';
import { EmpPsiTrainingInfoComponent } from './add-employee/employee-informations/emp-psi-training-info/emp-psi-training-info.component';
import { EmpBankInfoComponent } from './add-employee/employee-informations/emp-bank-info/emp-bank-info.component';
import { EmpLanguageInfoComponent } from './add-employee/employee-informations/emp-language-info/emp-language-info.component';
import { EmpForeignTourInfoComponent } from './add-employee/employee-informations/emp-foreign-tour-info/emp-foreign-tour-info.component';
import { EmpPhotoSignComponent } from './add-employee/employee-informations/emp-photo-sign/emp-photo-sign.component';
import { IconModule } from '@coreui/icons-angular';
import { EmployeeListComponent } from './manage-employee/employee-list/employee-list.component';
import { EmployeeInformationComponent } from './manage-employee/employee-information/employee-information.component';
import { ViewEmployeeComponent } from './add-employee/view-employee/view-employee.component';
import { UpdateUserInfoComponent } from './add-employee/update-user-info/update-user-info.component';
import { EmpNomineeInfoComponent } from './add-employee/employee-informations/emp-nominee-info/emp-nominee-info.component';
import { EmpIdCardGenerateComponent } from './manage-employee/emp-id-card-generate/emp-id-card-generate.component';
import { MatCardModule } from '@angular/material/card';
import { BengaliDigitPipe } from './manage-employee/emp-id-card-generate/bangali-digit.pipe';
import { BengaliDatePipe } from './manage-employee/emp-id-card-generate/bengali-date.pipe';
import {MatTabsModule} from '@angular/material/tabs';



@NgModule({
  declarations: [
    ViewUsersComponent,
    ViewInformationListComponent,
    BasicInformationComponent,
    PersonalInformationComponent,
    EmpPresentAddressComponent,
    EmpPermanentAddressComponent,
    EmpJobDetailsComponent,
    EmpSpouseInfoComponent,
    EmpChildInfoComponent,
    EmpEducationInfoComponent,
    EmpPsiTrainingInfoComponent,
    EmpBankInfoComponent,
    EmpLanguageInfoComponent,
    EmpForeignTourInfoComponent,
    EmpPhotoSignComponent,
    EmployeeListComponent,
    EmployeeInformationComponent,
    ViewEmployeeComponent,
    UpdateUserInfoComponent,
    EmpNomineeInfoComponent,
    EmpIdCardGenerateComponent,
    BengaliDigitPipe,
    BengaliDatePipe,
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
  ],
  providers:
  [
    CountryService,
  ],
})
export class EmployeeModule { }

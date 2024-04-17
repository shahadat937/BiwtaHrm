import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChildStatusService } from './service/child-status.service';
import { EmployeeTypeService } from './service/employee-type.service';
import { GenderService } from './service/gender.service';
import { MaritalStatusService } from './service/marital-status.service';
import { ReligionService } from './service/religion.service';

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
} from '@coreui/angular';
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { ToastrService } from 'ngx-toastr';
import { AppToastComponent } from '../notifications/toasters/toast-simple/toast.component';

import { SubjectComponent } from './subject/subject.component';
import { GroupComponent } from './group/group.component';
import { BranchComponent } from './branch/branch.component';
import { UnionComponent } from './union/union.component';
import { WardComponent } from './ward/ward.component';
import { ShiftComponent } from './shift/shift.component';
import { TrainingComponent } from './training/training.component'
import { DepartmentComponent } from './department/department.component';
 
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { BasicSetupRoutingModule } from './basic-setup-routing.module';
import { BloodGroupComponent } from './blood-group/blood-group.component';
import { ChildStatusComponent } from './child-status/child-status.component';
import { DesignationComponent } from './designation/designation.component';
import { DistrictComponent } from './district/district.component';
import { EmployeeTypeComponent } from './employee-type/employee-type.component';
import { GenderComponent } from './gender/gender.component';
import { GradeComponent } from './grade/grade.component';
import { MaritalStatusComponent } from './marital-status/marital-status.component';
import { PromotionTypeComponent } from './promotion-type/promotion-type.component';
import { PunishmentComponent } from './punishment/punishment.component';
import { ReligionComponent } from './religion/religion.component';
import { ResultComponent } from './result/result.component';
import { ScaleComponent } from './scale/scale.component';
import { BloodGroupService } from './service/BloodGroup.service';
import { ThanaComponent } from './thana/thana.component';
import { UpazilaComponent } from './upazila/upazila.component';
import { CountryComponent } from './country/country.component';
import { CountryService } from './service/country.service';

@NgModule({
  declarations: [
    NewAccountTypeComponent,
    BloodGroupComponent,
    ScaleComponent,
    DistrictComponent,
    UpazilaComponent,
    ThanaComponent,
    ResultComponent,
    SubjectComponent,
    GroupComponent,
    BranchComponent,
    UnionComponent,
    WardComponent,
    ShiftComponent,
    TrainingComponent,
    DepartmentComponent,
    MaritalStatusComponent,
    EmployeeTypeComponent,
    GenderComponent,
    ReligionComponent,
    ChildStatusComponent,
    DesignationComponent,
    PunishmentComponent,
    PromotionTypeComponent,
    GradeComponent,
    CountryComponent,
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
    ProgressModule,
  ],
  providers: [
    CountryService,
    BloodGroupService,
    ToastrService,
    ChildStatusService,
    EmployeeTypeService,
    MaritalStatusService,
    ReligionService,
    GenderService,
  ],
})
export class BasicSetupModule {}

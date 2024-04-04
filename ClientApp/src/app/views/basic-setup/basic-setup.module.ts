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
import { BloodGroupService } from './service/BloodGroup.service';
import { ToastrService } from 'ngx-toastr';
import { AppToastComponent } from '../notifications/toasters/toast-simple/toast.component';
import { UpazilaComponent } from './upazila/upazila.component';
import { ThanaComponent } from './thana/thana.component';
import { ResultComponent } from './result/result.component';
import { SubjectComponent } from './subject/subject.component';
import { GroupComponent } from './group/group.component';
import { BranchComponent } from './branch/branch.component';
import { UnionComponent } from './union/union.component';
import { WardComponent } from './ward/ward.component';
import { ShiftComponent } from './shift/shift.component';
import { TrainingComponent } from './training/training.component';
import { DepartmentComponent } from './department/department.component';


@NgModule({
  declarations:
  [
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
    ProgressModule
  ],
  // providers:[
  //   BloodGroupService,
  //   ToastrService
  // ]
})
export class BasicSetupModule { }

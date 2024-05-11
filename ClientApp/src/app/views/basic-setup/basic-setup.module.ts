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

import { BranchComponent } from './branch/branch.component';
import { DepartmentComponent } from './department/department.component';
import { GroupComponent } from './group/group.component';
//import{GroupComponent} from './group/group.component'

import { ShiftComponent } from './shift/shift.component';
import { SubjectComponent } from './subject/subject.component';
import { TrainingComponent } from './training/training.component';
import { UnionComponent } from './union/union.component';
import { WardComponent } from './ward/ward.component';

import { SpinnerModule } from '@coreui/angular';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { BankAccountTypeComponent } from './bank-account-type/bank-account-type.component';
import { BasicSetupRoutingModule } from './basic-setup-routing.module';
import { BloodGroupComponent } from './blood-group/blood-group.component';
import { ChildStatusComponent } from './child-status/child-status.component';
import { CountryComponent } from './country/country.component';
import { DesignationComponent } from './designation/designation.component';
import { DistrictComponent } from './district/district.component';
import { DivisionComponent } from './division/division.component';
import { EmployeeTypeComponent } from './employee-type/employee-type.component';
import { EyesColorComponent } from './eyes-color/eyes-color.component';
import { GenderComponent } from './gender/gender.component';
import { GradeClassComponent } from './grade-class/grade-class.component';
import { GradeTypeComponent } from './grade-type/grade-type.component';
import { GradeComponent } from './grade/grade.component';
import { HairColorComponent } from './hair-color/hair-color.component';
import { LanguageComponent } from './language/language.component';
import { LeaveComponent } from './leave/leave.component';
import { MaritalStatusComponent } from './marital-status/marital-status.component';
import { OccupationComponent } from './occupation/occupation.component';
import { OverallEVPromotionComponent } from './overall-ev-promotion/overall-ev-promotion.component';
import { PoolComponent } from './pool/pool.component';
import { PromotionTypeComponent } from './promotion-type/promotion-type.component';
import { PunishmentComponent } from './punishment/punishment.component';
import { RelationComponent } from './relation/relation.component';
import { ReligionComponent } from './religion/religion.component';
import { ResultComponent } from './result/result.component';
import { ScaleComponent } from './scale/scale.component';
import { BloodGroupService } from './service/BloodGroup.service';
import { LeaveService } from './service/Leave.service';
import { OccupationService } from './service/Occupation.service';
import { Overall_EV_PromotionService } from './service/Overall_EV_Promotion.service';
import { BankAccountTypeService } from './service/bank-account-type.service';
import { CountryService } from './service/country.service';
import { EyesColorService } from './service/eyes-color.service';
import { LanguageService } from './service/language.service';
import { PoolService } from './service/pool.service';
import { RelationService } from './service/relation.service';
import { SubDepartmentService } from './service/sub-department.service';
import { SubDepartmentComponent } from './sub-department/sub-department.component';
import { ThanaComponent } from './thana/thana.component';
import { TrainingNameComponent } from './training-name/training-name.component';
import { UpazilaComponent } from './upazila/upazila.component';
import { UserRoleComponent } from './user-role/user-role.component';

import { BankBranchComponent } from './bank-branch/bank-branch.component';
import { BankComponent } from './bank/bank.component';
import { CompetenceComponent } from './competence/competence.component';
import { InstituteComponent } from './institute/institute.component';
import { OfficeAddressComponent } from './office-address/office-address.component';
import { OfficeComponent } from './office/office.component';
import { BankBranchService } from './service/bank-branch.service';
import { ExamTypeComponent } from './exam-type/exam-type.component';
import { ExamTypeService } from './service/exam-type.service';
import { BankService } from './service/bank.service';
import { BoardComponent } from './board/board.component';
import { BoardService } from './service/board.service';
import { SectionComponent } from './section/section.component';
import { SectionService } from './service/section.service';
import { SubBranchComponent } from './sub-branch/sub-branch.component';
import { BranchService } from './service/branch.service';
import { YearSetupService } from './service/year-setup.service';
import { YearSetupComponent } from './year-setup/year-setup.component';


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
    GradeTypeComponent,
    GradeClassComponent,
    DivisionComponent,
    TrainingNameComponent,
    OccupationComponent,
    LeaveComponent,
    OverallEVPromotionComponent,
    HairColorComponent,
    EyesColorComponent,
    RelationComponent,
    PoolComponent,
    SubDepartmentComponent,
    UserRoleComponent,
    BankComponent,
    BankBranchComponent,
    BankAccountTypeComponent,
    LanguageComponent,
    InstituteComponent,
    OfficeComponent,
    OfficeAddressComponent,
    CompetenceComponent,
    BankComponent,
    BankBranchComponent,
    ExamTypeComponent,
    BoardComponent,
    SectionComponent,
    SubBranchComponent,
    YearSetupComponent
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
    SpinnerModule,
  ],
  providers: [
    SectionService,
    BranchService,
    BoardService,
    BankService,
    ExamTypeService,
    BankAccountTypeService,
    BankBranchService,
    LanguageService,
    BankAccountTypeService,
    BankBranchService,
    PoolService,
    SubDepartmentService,
    RelationService,
    EyesColorService,
    Overall_EV_PromotionService,
    LeaveService,
    CountryService,
    OccupationService,
    BloodGroupService,
    ToastrService,
    ChildStatusService,
    EmployeeTypeService,
    MaritalStatusService,
    ReligionService,
    GenderService,
    YearSetupService
  ],
})
export class BasicSetupModule {}

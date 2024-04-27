import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { BloodGroupComponent } from './blood-group/blood-group.component';
import { ScaleComponent } from './scale/scale.component';
import {DistrictComponent} from './district/district.component';
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
import { MaritalStatusComponent } from './marital-status/marital-status.component';
import { EmployeeTypeComponent } from './employee-type/employee-type.component';
import { GenderComponent } from './gender/gender.component';
import { ReligionComponent } from './religion/religion.component';
import { ChildStatusComponent } from './child-status/child-status.component';
import { Designation } from './model/Designation';
import { DesignationComponent } from './designation/designation.component';
import { PunishmentComponent } from './punishment/punishment.component';
import { PromotionTypeComponent } from './promotion-type/promotion-type.component';
import { GradeComponent } from './grade/grade.component';
import { CountryComponent } from './country/country.component';
import { GradeTypeComponent } from './grade-type/grade-type.component';
import { GradeClassComponent } from './grade-class/grade-class.component';
import { DivisionComponent } from './division/division.component';
import { OccupationComponent } from './occupation/occupation.component';
import { LeaveComponent } from './leave/leave.component';
import { OverallEVPromotionComponent } from './overall-ev-promotion/overall-ev-promotion.component';
import { HairColorComponent } from './hair-color/hair-color.component';
import { EyesColorComponent } from './eyes-color/eyes-color.component';
import { RelationComponent } from './relation/relation.component';
import { SubDepartmentComponent } from './sub-department/sub-department.component';
import { PoolComponent } from './pool/pool.component';
import { UserRoleComponent } from './user-role/user-role.component';
import { BankComponent } from './bank/bank.component';
import { BankBranchComponent } from './bank-branch/bank-branch.component';
import { BankAccountTypeComponent } from './bank-account-type/bank-account-type.component';
import { LanguageComponent } from './language/language.component';
import { TrainingNameComponent } from './training-name/training-name.component';
import { InstituteComponent } from './institute/institute.component';
import { OfficeComponent } from './office/office.component';
import { OfficeAddressComponent } from './office-address/office-address.component';
import { CompetenceComponent } from './competence/competence.component';


const routes: Routes = [

  {
    path: 'add-accounttype',
    component: NewAccountTypeComponent,
  },
  {
    path: 'blood-group',
    component: BloodGroupComponent,
  },
  { path: 'update-bloodgroup/:bloodGroupId',
    component: BloodGroupComponent,
  },
  {
    path: 'pool',
    component: PoolComponent,
  },
  { path: 'update-pool/:poolId',
    component: PoolComponent,
  },
  {
    path: 'occupation',
    component: OccupationComponent,
  },
  { path: 'update-occupation/:occupationId',
    component: OccupationComponent,
  },
  {
    path:'marital-status',
    component: MaritalStatusComponent,
  },
  {
    path:'update-marital-status/:maritalStatusId',
    component: MaritalStatusComponent,
  },
  {
    path: 'employee-type',
    component: EmployeeTypeComponent,
  },
  {
    path: 'update-employee-type/:employeeTypeId',
    component: EmployeeTypeComponent,
  },
  {
    path: 'gender',
    component: GenderComponent,
  },
  {
    path: 'update-gender/:genderId',
    component: GenderComponent,
  },
  {
    path: 'religion',
    component: ReligionComponent,
  },
  {
    path: 'update-religion/:religionId',
    component: ReligionComponent,
  },
  {
    path: 'child-status',
    component: ChildStatusComponent
  },
  { path: 'update-child-status/:childStatusId',
  component: ChildStatusComponent,
  },
  {
    path: 'country',
    component: CountryComponent,
  },
  { path: 'update-country/:countryId', 
  component: CountryComponent, 
  },
  {
    path: 'division',
    component: DivisionComponent,
  },
  { path: 'update-division/:divisionId', 
  component: DivisionComponent, 
  },
  {
    path: 'district',
    component: DistrictComponent,
  },
  { path: 'update-district/:districtId', 
  component: DistrictComponent, 
  },
  {
    path: 'upazila',
    component: UpazilaComponent,
  },
  { path: 'update-upazila/:upazilaId', 
  component: UpazilaComponent, 
  },
  {
    path: 'thana',
    component: ThanaComponent,
  },
  { 
    path: 'update-thana/:thanaId', 
    component: ThanaComponent, 
  },
  {
    path: 'result',
    component: ResultComponent,
  },
  {
    path: 'update-result/:resultId',
    component: ResultComponent,
  },
  { 
    path: 'subject',
    component: SubjectComponent,
  },
  { path: 'update-subject/:subjectId', 
  component: SubjectComponent, 
  },
  {
    path: 'group',
    component: GroupComponent,
  },
  { path: 'update-group/:groupId', 
  component: GroupComponent, 
  },
  {
    path: 'branch',
    component: BranchComponent,
  },
  { 
    path: 'update-branch/:branchId', 
    component: BranchComponent, 
  },
  {
    path: 'union',
    component: UnionComponent,
  },
  {
    path: 'update-union/:unionId',
    component: UnionComponent,
  },
  {
    path: 'ward',
    component: WardComponent,
  },
  { 
    path: 'update-ward/:wardId', 
    component: WardComponent, 
  },
  {
    path: 'shift',
    component: ShiftComponent,
  },
  {
    path: 'update-shift/:shiftId',
    component: ShiftComponent,
  },
  {
    path: 'training',
    component: TrainingComponent,
  },
  { 
    path: 'update-trainingType/:trainingTypeId', 
    component: TrainingComponent, 
  },
  {
    path: 'department',
    component: DepartmentComponent,
  },
  {
    path: 'update-department/:departmentId',
    component: DepartmentComponent,
  },
  {
    path: 'designation',
    component: DesignationComponent,
  },
  {
    path: 'update-designation/:designationId',
    component: DesignationComponent,
  },
  {

    path: 'promotionType',
    component: PromotionTypeComponent,
  },
  {
    path: 'update-promotionType/:promotionTypeId',
    component: PromotionTypeComponent,
  },
  {

    path: 'punishment',
    component: PunishmentComponent,
  },
  {
    path: 'update-punishment/:punishmentId',
    component: PunishmentComponent,
  },
  {
    path: 'scale',
    component: ScaleComponent,
  },
  {
    path: 'update-scale/:scaleId',
    component: ScaleComponent,
  },

  {
    path: 'grade',
    component: GradeComponent,
  },
  {
    path: 'update-grade/:gradeId',
    component: GradeComponent,
  },
    {
    path: 'grade-type',
    component: GradeTypeComponent,
  },
  { path: 'update-grade-type/:gradeTypeId',
    component: GradeTypeComponent,
  },
  {
    path: 'grade-class',
    component: GradeClassComponent,
  },
  { path: 'update-grade-class/:gradeClassId',
    component: GradeClassComponent,
  },
  {
    path: 'leave',
    component: LeaveComponent,
  },
  { path: 'update-leave/:leaveId',
    component: LeaveComponent,
  },
  {
    path: 'overall_EV_Promotion',
    component: OverallEVPromotionComponent,
  },
  { path: 'update-overall_EV_Promotion/:overall_EV_PromotionId',
    component: OverallEVPromotionComponent,
  },
  {
    path: 'hairColor',
    component: HairColorComponent,
  },
  { path: 'update-hairColor/:hairColorId',
    component: HairColorComponent,
  },
  {
    path: 'eyesColor',
    component: EyesColorComponent,
  },
  { path: 'update-eyesColor/:eyesColorId',
    component: EyesColorComponent,
  },
  {
    path: 'relation',
    component: RelationComponent,
  },
  { path: 'update-relation/:relationId',
    component: RelationComponent,
  },

  {
    path: 'trainingName',
    component: TrainingNameComponent,
  },
  { path: 'update-trainingName/:trainingNameId',
    component: TrainingNameComponent,
  },
  {
    path: 'subDepartment',
    component: SubDepartmentComponent,
  },
  { path: 'update-subDepartment/:subDepartmentId',
    component: SubDepartmentComponent,
  },
  {
    path: 'subDepartment',
    component: SubDepartmentComponent,
  },
  { path: 'update-subDepartment/:subDepartmentId',
    component: SubDepartmentComponent,
  },
  {
    path: 'userRole',
    component: UserRoleComponent,
  },
  { path: 'update-userRole/:userRoleId',
    component: UserRoleComponent,
  },
  {
    path: 'institute',
    component: InstituteComponent,
  },
  { path: 'update-institute/:instituteId',
    component: InstituteComponent,
  },
  {
    path: 'office',
    component: OfficeComponent,
  },
  { path: 'update-office/:officeId',
    component: OfficeComponent,
  },
  {
    path: 'officeAddress',
    component: OfficeAddressComponent,
  },
  { path: 'update-officeAddress/:officeAddressId',
    component: OfficeAddressComponent,
  },
  {
    path: 'competence',
    component: CompetenceComponent,
  },
  { path: 'update-competence/:competenceId',
    component: CompetenceComponent,
},
  {
    path: 'bank',
    component: BankComponent,
  },
  { path: 'update-bank/:bankId',
    component: BankComponent,
  },
  {
    path: 'bankBranch',
    component: BankBranchComponent,
  },
  { path: 'update-bankBranch/:bankBranchId',
    component: BankBranchComponent,
  },
  {
    path: 'bankAccountType',
    component: BankAccountTypeComponent,
  },
  { path: 'update-bankAccountType/:bankAccountTypeId',
    component: BankAccountTypeComponent,
  },
  { 
    path: 'language',
    component: LanguageComponent,
  },
  { path: 'update-language/:languageId',
    component: LanguageComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }

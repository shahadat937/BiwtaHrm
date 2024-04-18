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
  { path: 'update-country/:countrytId', 
  component: CountryComponent, 
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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { BloodGroupComponent } from './blood-group/blood-group.component';
import { ScaleComponent } from './scale/scale.component';
import {DistrictComponent} from './district/district.component';
import { UpazilaComponent } from './upazila/upazila.component';
import { ThanaComponent } from './thana/thana.component';
import { ResultComponent } from './result/result.component';
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
    path: 'religion',
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
  { path: 'update-upazila/:upazilaid', 
  component: UpazilaComponent, 
  },
  {
    path: 'thana',
    component: ThanaComponent,
  },
  { 
    path: 'update-thana/:thanaid', 
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
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }

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

const routes: Routes = [

  {
    path: 'add-accounttype',
    component: NewAccountTypeComponent,
  },
  {
    path: 'blood-group',
    component: BloodGroupComponent,
  },
  {
    path: 'scale',
    component: ScaleComponent
  },
  { path: 'update-bloodgroup/:bloodGroupId',
    component: BloodGroupComponent,
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
  { path: 'update-group/:groupid', 
  component: GroupComponent, 
  },
  {
    path: 'branch',
    component: BranchComponent,
  },
  { 
    path: 'update-branch/:branchid', 
    component: BranchComponent, 
  },
  {
    path: 'union',
    component: UnionComponent,
  },
  {
    path: 'update-union/:unionid',
    component: UnionComponent,
  },
  {
    path: 'ward',
    component: WardComponent,
  },
  { 
    path: 'update-ward/:wardid', 
    component: WardComponent, 
  },
  {
    path: 'shift',
    component: ShiftComponent,
  },
  {
    path: 'update-shift/:shiftid',
    component: ShiftComponent,
  },
  {
    path: 'training',
    component: TrainingComponent,
  },
  { 
    path: 'update-training/:trainingid', 
    component: TrainingComponent, 
  },
  {
    path: 'department',
    component: DepartmentComponent,
  },
  {
    path: 'update-department/:departmentid',
    component: DepartmentComponent,
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }

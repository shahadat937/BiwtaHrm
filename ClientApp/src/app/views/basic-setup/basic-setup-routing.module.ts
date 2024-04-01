import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { BloodGroupComponent } from './blood-group/blood-group.component';
import { ScaleComponent } from './scale/scale.component';
import {DistrictComponent} from './district/district.component';
import { UpazilaComponent } from './upazila/upazila.component';
import { ThanaComponent } from './thana/thana.component';
import { ResultComponent } from './result/result.component';

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

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }

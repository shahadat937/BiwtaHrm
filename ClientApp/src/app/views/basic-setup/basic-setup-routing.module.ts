import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import { BloodGroupComponent } from './blood-group/blood-group.component';
import {DistrictComponent} from './district/district.component';

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
    path: 'district',
    component: DistrictComponent,
  },
  { path: 'update-district/:districtId', 
  component: DistrictComponent, 
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }

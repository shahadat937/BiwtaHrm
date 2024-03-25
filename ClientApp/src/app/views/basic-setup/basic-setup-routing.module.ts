import { BloodGroupListComponent } from './bloodgroup/bloodgroup-list/bloodgroup-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';

const routes: Routes = [

  {
    path: 'add-accounttype',
    component: NewAccountTypeComponent,
  },
  {
    path: 'bloodgroup-list',
    component: BloodGroupListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }

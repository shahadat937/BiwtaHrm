import { ManageleaveComponent } from './manageleave/manageleave.component';
import { AddleaveComponent } from './addleave/addleave.component';
import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path:'addleave',
    component:AddleaveComponent
  },
  {
    path:'manageleave',
    component:ManageleaveComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeaveRoutingModule { }

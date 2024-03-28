import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';
import{ResultListComponent} from './results/result-list/result-list.component';
import{NewResultComponent} from './results/new-result/new-result.component';
const routes: Routes = [

  {
    path: 'add-accounttype',
    component: NewAccountTypeComponent,
  },
  {
    path:'result-list',
    component:ResultListComponent,
    
  },
  {
    path:'new-result',
    component:NewResultComponent,
  }
  ,
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BasicSetupRoutingModule { }

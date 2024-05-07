import { ReleaseComponent } from './release/release.component';
import { PostingComponent } from './posting/posting.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmpDemoComponent } from './emp-demo/emp-demo.component';

const routes: Routes = [
  {
    path:'postingOrderInfo',
    component:PostingComponent
  },
  { path: 'update-postingOrderInfo/:postingOrderInfoId',
    component: PostingComponent,
  },
  {
    path:'EmpDemo',
    component:EmpDemoComponent
  },
  {
    path:'release',
    component:ReleaseComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransferRoutingModule { }

import { ReleaseComponent } from './release/release.component';
import { PostingComponent } from './posting/posting.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmpModalComponent } from './emp-modal/emp-modal.component';
import { TransferlistComponent } from './transferlist/transferlist.component';
import { getPositionOfLineAndCharacter } from 'typescript';


const routes: Routes = [
  {
    path:'postingOrderInfo',
    component:PostingComponent
  },
  { path: 'update-postingOrderInfo/:postingOrderInfoId',
    component: PostingComponent,
  },
  {
    path:'release',
    component:ReleaseComponent
  },
  {
    path:'EmpModal',
    component:EmpModalComponent
  },
  {
    path:'TransferOrderList',
    component:TransferlistComponent
  },
  {
    path: 'update-TransferOrderList/:postingOrderInfoId',
    component: PostingComponent,
  },
  //  { path: 'TransferOrderList', component: TransferlistComponent },
  // { path: 'update-TransferOrderList/:postingOrderInfoId', component: PostingComponent },
  // { path: '', redirectTo: '/TransferOrderList', pathMatch: 'full' }, // Redirect to the list by default

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransferRoutingModule { }

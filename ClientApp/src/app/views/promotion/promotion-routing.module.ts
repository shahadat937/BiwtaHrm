import { IncrementAndPromotionApprovalComponent } from './increment-and-promotion-approval/increment-and-promotion-approval.component';
import { IncrementAndPromotionComponent } from './increment-and-promotion/increment-and-promotion.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManagePromotionComponent } from './manage-promotion/manage-promotion.component';
import { PromotionApprovalListComponent } from './promotion-approval-list/promotion-approval-list.component';


const routes: Routes = [
  {
    path:'incrementAndPromotion',
    component:IncrementAndPromotionComponent
  },
  {
    path:'update-incrementAndPromotion/:id',
    component:IncrementAndPromotionComponent
  },
  {
    path:'incrementAndPromotionApproval',
    component:PromotionApprovalListComponent
  },
  {
    path:'manage-incrementAndPromotion',
    component:ManagePromotionComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PromotionRoutingModule { }

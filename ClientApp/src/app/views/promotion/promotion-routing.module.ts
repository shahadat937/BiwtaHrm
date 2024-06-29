import { IncrementAndPromotionHistoryComponent } from './increment-and-promotion-history/increment-and-promotion-history.component';
import { IncrementAndPromotionApprovalComponent } from './increment-and-promotion-approval/increment-and-promotion-approval.component';
import { IncrementAndPromotionComponent } from './increment-and-promotion/increment-and-promotion.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {
    path:'incrementAndPromotion',
    component:IncrementAndPromotionComponent
  },
  {
    path:'incrementAndPromotionApproval',
    component:IncrementAndPromotionApprovalComponent
  },
  {
    path:'incrementAndPromotionHistory',
    component:IncrementAndPromotionHistoryComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PromotionRoutingModule { }

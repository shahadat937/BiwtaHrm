import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PromotionRoutingModule } from './promotion-routing.module';
import { PromotionComponent } from './promotion/promotion.component';
import { ManagePromotionComponent } from './manage-promotion/manage-promotion.component';
import { IncrementAndPromotionComponent } from './increment-and-promotion/increment-and-promotion.component';
import { IncrementAndPromotionApprovalComponent } from './increment-and-promotion-approval/increment-and-promotion-approval.component';
import { IncrementAndPromotionHistoryComponent } from './increment-and-promotion-history/increment-and-promotion-history.component';


@NgModule({
  declarations: [
    PromotionComponent,
    ManagePromotionComponent,
    IncrementAndPromotionComponent,
    IncrementAndPromotionApprovalComponent,
    IncrementAndPromotionHistoryComponent
  ],
  imports: [
    CommonModule,
    PromotionRoutingModule
  ]
})
export class PromotionModule { }

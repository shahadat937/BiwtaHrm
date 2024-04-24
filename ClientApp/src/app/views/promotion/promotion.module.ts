import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PromotionRoutingModule } from './promotion-routing.module';
import { PromotionComponent } from './promotion/promotion.component';
import { ManagePromotionComponent } from './manage-promotion/manage-promotion.component';


@NgModule({
  declarations: [
    PromotionComponent,
    ManagePromotionComponent
  ],
  imports: [
    CommonModule,
    PromotionRoutingModule
  ]
})
export class PromotionModule { }

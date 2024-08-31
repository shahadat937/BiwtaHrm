import { IncrementAndPromotionApprovalComponent } from './increment-and-promotion-approval/increment-and-promotion-approval.component';
import { IncrementAndPromotionComponent } from './increment-and-promotion/increment-and-promotion.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManagePromotionComponent } from './manage-promotion/manage-promotion.component';
import { PromotionApprovalListComponent } from './promotion-approval-list/promotion-approval-list.component';
import { RewardPunishmentListComponent } from './reward-punishment-list/reward-punishment-list.component';


const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Increment and Promotion',
    },
    children: [
      {
        path:'incrementAndPromotion',
        component:IncrementAndPromotionComponent,
        data: {
          title: 'Increment and Promotion Application',
        },
      },
      {
        path:'update-incrementAndPromotion/:id',
        component:IncrementAndPromotionComponent,
        data: {
          title: 'Update Increment and Promotion',
        },
      },
      {
        path:'incrementAndPromotionApproval',
        component:PromotionApprovalListComponent,
        data: {
          title: 'Increment and Promotion Approval',
        },
      },
      {
        path:'manage-incrementAndPromotion',
        component:ManagePromotionComponent,
        data: {
          title: 'Manage Increment and Promotion',
        },
      },
      {
        path:'rewardPunishment',
        component:RewardPunishmentListComponent,
        data: {
          title: 'Reward/Punishment',
        },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PromotionRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrainsferPostingListComponent } from './trainsfer-posting-list/trainsfer-posting-list.component';
import { TransferPostingApplicationComponent } from './transfer-posting-application/transfer-posting-application.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Transfer and Posting',
    },
    children: [
      {
        path: 'transferPostingList',
        component: TrainsferPostingListComponent,
        data: {
          title: 'Transfer and Posting List',
        },
      },
      {
        path: 'transferPostingApplication',
        component: TransferPostingApplicationComponent,
        data: {
          title: 'Transfer and Posting Application',
        },
      },
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransferPostingRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrainsferPostingListComponent } from './trainsfer-posting-list/trainsfer-posting-list.component';

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
          title: 'Update User',
        },
      }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransferPostingRoutingModule { }

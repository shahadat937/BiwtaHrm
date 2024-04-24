import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TransferRoutingModule } from './transfer-routing.module';
import { PostingComponent } from './posting/posting.component';
import { ReleaseComponent } from './release/release.component';


@NgModule({
  declarations: [
    PostingComponent,
    ReleaseComponent
  ],
  imports: [
    CommonModule,
    TransferRoutingModule
  ]
})
export class TransferModule { }

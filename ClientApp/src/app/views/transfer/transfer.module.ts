import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransferRoutingModule } from './transfer-routing.module';
import { PostingComponent } from './posting/posting.component';
import { ReleaseComponent } from './release/release.component';
import { SpinnerModule } from '@coreui/angular';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import {
  ButtonGroupModule,ButtonModule,CardModule,DropdownModule,FormModule, GridModule, ListGroupModule, 
  ProgressModule,SharedModule,} from '@coreui/angular';
import { PostingOrderInfoService } from '../basic-setup/service/posting-order-info.service';
import { FormsModule } from '@angular/forms';
@NgModule({
  declarations: [

    PostingComponent,
    ReleaseComponent
  ],
  imports: [
    SpinnerModule,
    DropdownModule,
    FormsModule,
    FormModule,
    FormModule,
    GridModule,
    ListGroupModule,
    ProgressModule,
    SharedModule,
    ButtonGroupModule,
    ButtonModule,
    CardModule,
    CommonModule,
    TransferRoutingModule,
    MatFormFieldModule,
    MatTableModule,
    FormsModule,
    MatPaginatorModule,
    MatInputModule,
  ],
  providers:[
    PostingOrderInfoService
  ]
})
export class TransferModule { }

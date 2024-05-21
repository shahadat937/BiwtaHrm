import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransferRoutingModule } from './transfer-routing.module';
import { PostingComponent } from './posting/posting.component';
import { ReleaseComponent } from './release/release.component';
import { ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, SpinnerModule } from '@coreui/angular';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import {CollapseDirective} from '@coreui/angular';

import {
  ButtonGroupModule,ButtonModule,CardModule,DropdownModule,FormModule, GridModule, ListGroupModule, 
  ProgressModule,SharedModule,} from '@coreui/angular';
import { PostingOrderInfoService } from '../basic-setup/service/posting-order-info.service';
import { FormsModule } from '@angular/forms';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { EmpModalComponent } from './emp-modal/emp-modal.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ApproveEmpModalComponent } from './approve-emp-modal/approve-emp-modal.component';
import { TransferlistComponent } from './transferlist/transferlist.component';

@NgModule({
  declarations: [

    PostingComponent,
    ReleaseComponent,
    EmpModalComponent,
    ApproveEmpModalComponent,
    TransferlistComponent,
  ],
  imports: [
    MatCardModule,
    SpinnerModule,
    DropdownModule,
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
    CollapseDirective,
    SharedCustomModule,
    ModalBodyComponent,
    CommonModule,
    ModalComponent,
    ModalFooterComponent,
  ModalHeaderComponent,
  MatDialogModule,
  ],
  providers:[
    PostingOrderInfoService
  ],
  bootstrap: [EmpModalComponent]
})
export class TransferModule {
  
 }

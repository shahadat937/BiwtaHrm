import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
import { FormsModule } from '@angular/forms';
import { TransferPostingRoutingModule } from './transfer-routing.module';
import { TrainsferPostingListComponent } from './trainsfer-posting-list/trainsfer-posting-list.component';
import { TransferPostingApplicationComponent } from './transfer-posting-application/transfer-posting-application.component';

@NgModule({
  declarations: [
    TrainsferPostingListComponent,
    TransferPostingApplicationComponent,

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
    TransferPostingRoutingModule,
    MatFormFieldModule,
    MatTableModule,
    FormsModule,
    MatPaginatorModule,
    MatInputModule,
    CollapseDirective,
    ModalBodyComponent,
    CommonModule,
    ModalComponent,
    ModalFooterComponent,
    ModalHeaderComponent,
  ],
  providers:[

  ],
  bootstrap: []
})
export class TransferPostingModule {
  
 }

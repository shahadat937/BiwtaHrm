import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PromotionRoutingModule } from './promotion-routing.module';
import { PromotionComponent } from './promotion/promotion.component';
import { ManagePromotionComponent } from './manage-promotion/manage-promotion.component';
import { IncrementAndPromotionComponent } from './increment-and-promotion/increment-and-promotion.component';
import { IncrementAndPromotionApprovalComponent } from './increment-and-promotion-approval/increment-and-promotion-approval.component';
import {
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseDirective,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ProgressModule,
  SharedModule,
  TableModule, 
  UtilitiesModule,
  PaginationModule,
  AlertComponent,
  ModalBodyComponent,
  ModalComponent,
  ModalFooterComponent,
  ModalHeaderComponent,
  PopoverModule,
  SpinnerModule,
  TooltipModule,
} from '@coreui/angular';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { IconModule } from '@coreui/icons-angular';
import { TransferPostingRoutingModule } from '../transferPosting/transfer-routing.module';
import { PromotionIncrementInfoComponent } from './promotion-increment-info/promotion-increment-info.component';
import { PromotionApprovalListComponent } from './promotion-approval-list/promotion-approval-list.component';


@NgModule({
  declarations: [
    PromotionComponent,
    ManagePromotionComponent,
    IncrementAndPromotionComponent,
    IncrementAndPromotionApprovalComponent,
    PromotionIncrementInfoComponent,
    PromotionApprovalListComponent,
  ],
  imports: [
    CommonModule,
    PromotionRoutingModule,
    CommonModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    FormModule,
    ButtonModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    ProgressModule,
    CollapseDirective,
    TableModule,
    UtilitiesModule,
    PaginationModule,
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
    IconModule,
    MatIconModule,
    MatButtonModule,
    TableModule,
    PopoverModule,
    AlertComponent,
    TooltipModule,
  ]
})
export class PromotionModule { }

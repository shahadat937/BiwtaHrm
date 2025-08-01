import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonToggleModule } from '@angular/material/button-toggle';

import {
  AlertComponent,
  AlertModule,
  BadgeComponent,
  BadgeModule,
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseDirective,
  DropdownModule,
  FormModule,
  GridModule,
  ListGroupModule,
  ModalBodyComponent,
  ModalComponent,
  ModalFooterComponent,
  ModalHeaderComponent,
  ModalModule,
  PopoverModule,
  ProgressModule,
  SharedModule,
  SpinnerModule,
  TableModule,
  ToastModule,
  TooltipModule,
  UtilitiesModule
} from '@coreui/angular';

import { IconModule } from '@coreui/icons-angular';

import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { NotificationsRoutingModule } from './notifications-routing.module';

import { AlertsComponent } from './alerts/alerts.component';
import { BadgesComponent } from './badges/badges.component';
import { ModalsComponent } from './modals/modals.component';
// import { ToastsComponent } from './toasts/toasts.component';
import { ToastersComponent } from './toasters/toasters.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppToastComponent } from './toasters/toast-simple/toast.component';
import { NotificationListComponent } from './notification-list/notification-list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { TransferPostingRoutingModule } from '../transferPosting/transfer-routing.module';
import { NoticeListComponent } from './notice-list/notice-list.component';
import { AddNoticeComponent } from './add-notice/add-notice.component';
import { OfficeOrderComponent } from './office-order/office-order.component';
import { MatTabsModule } from '@angular/material/tabs';
import { CalendarModule } from 'primeng/calendar';
import { OfficeOrderModalComponent } from './office-order-modal/office-order-modal.component';
import { DepartmentService } from '../basic-setup/service/department.service';
import { DesignationService } from '../basic-setup/service/designation.service';
import { OrderTypeService } from '../basic-setup/service/order-type.service';
import { SectionService } from '../basic-setup/service/section.service';
import { OfficeOrderService } from './service/office-order.service';
import { RoleFeatureService } from '../featureManagement/service/role-feature.service';
import {MatBadgeModule} from '@angular/material/badge';

@NgModule({
  declarations: [
    BadgesComponent,
    AlertsComponent,
    ModalsComponent,
    // ToastsComponent,
    ToastersComponent,
    AppToastComponent,
    NotificationListComponent,
    NoticeListComponent,
    AddNoticeComponent,
    OfficeOrderComponent,
    OfficeOrderModalComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NotificationsRoutingModule,
    DocsComponentsModule,
    AlertModule,
    GridModule,
    CardModule,
    BadgeModule,
    ButtonModule,
    FormModule,
    ModalModule,
    ToastModule,
    SharedModule,
    UtilitiesModule,
    TooltipModule,
    PopoverModule,
    ProgressModule,
    IconModule,
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
    BadgeComponent,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    MatButtonToggleModule,
    MatTabsModule,
    CalendarModule,
    MatBadgeModule
  ],
  providers: [
    
  ],
})
export class NotificationsModule {
}

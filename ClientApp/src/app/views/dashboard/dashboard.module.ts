import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import {
  AvatarModule,
  ButtonDirective,
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  ColComponent,
  DropdownComponent,
  DropdownItemDirective,
  DropdownMenuDirective,
  DropdownToggleDirective,
  FormModule,
  GridModule,
  NavModule,
  ProgressModule,
  RowComponent,
  TableModule,
  TabsModule,
  TemplateIdDirective,
  WidgetStatAComponent
} from '@coreui/angular';
import { IconDirective, IconModule } from '@coreui/icons-angular';
import { ChartjsComponent, ChartjsModule } from '@coreui/angular-chartjs';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';

import { WidgetsModule } from '../widgets/widgets.module';
import { TransferWidgetsComponent } from './DashboardWidgets/transfer-widgets/transfer-widgets.component';
import { RouterLink } from '@angular/router';
import { UsersWidgetsComponent } from './DashboardWidgets/users-widgets/users-widgets.component';
import { PromotionWidgetsComponent } from './DashboardWidgets/promotion-widgets/promotion-widgets.component';
import { ChartWidgetsComponent } from './DashboardWidgets/chart-widgets/chart-widgets.component';

@NgModule({
  imports: [
    DashboardRoutingModule,
    CardModule,
    NavModule,
    IconModule,
    TabsModule,
    CommonModule,
    GridModule,
    ProgressModule,
    ReactiveFormsModule,
    ButtonModule,
    FormModule,
    ButtonModule,
    ButtonGroupModule,
    ChartjsModule,
    AvatarModule,
    TableModule,
    WidgetsModule,
    ChartjsComponent,
    WidgetStatAComponent,
    RowComponent,
    ColComponent,
    WidgetStatAComponent,
    TemplateIdDirective,
    IconDirective,
    DropdownComponent,
    ButtonDirective,
    DropdownToggleDirective,
    DropdownMenuDirective,
    DropdownItemDirective,
    RouterLink,
  ],
  declarations: [DashboardComponent, TransferWidgetsComponent, UsersWidgetsComponent, PromotionWidgetsComponent, ChartWidgetsComponent]
})
export class DashboardModule {
}

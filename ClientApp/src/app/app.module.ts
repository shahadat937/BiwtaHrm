import { DATE_PIPE_DEFAULT_OPTIONS, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { CommonModule } from '@angular/common'; 
import { CalendarModule } from 'primeng/calendar';

import { ConfirmDialogComponent } from 'src/app/modals/confirm-dialog/confirm-dialog.component';

// Import routing module
import { AppRoutingModule } from './app-routing.module';
import { CoreModule } from './core/core.module';
// Import app component
import { AppComponent } from './app.component';
import { OffcanvasModule, SpinnerModule, ToasterService } from '@coreui/angular';

// Import containers
import {
  DefaultFooterComponent,
  DefaultHeaderComponent,
  DefaultLayoutComponent,
} from './containers';

import {
  AvatarModule,
  BadgeModule,
  BreadcrumbModule,
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  DropdownModule,
  FooterModule,
  FormModule,
  GridModule,
  HeaderModule,
  ListGroupModule,
  NavModule,
  ProgressModule,
  SharedModule,
  SidebarModule,
  TabsModule,
  UtilitiesModule,
} from '@coreui/angular';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { IconModule, IconSetService } from '@coreui/icons-angular';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ErrorInterceptor } from './core/interceptor/error.interceptor';
import { JwtInterceptor } from './core/interceptor/jwt.interceptor';
import {AuthInterceptor} from './core/interceptor/auth.interceptor';
import { AuthService } from './core/service/auth.service';
import { SharedCustomModule } from './shared/shared.module';
import { FormRecordService } from './views/appraisal/services/form-record.service';
import { YearSetupService } from './views/basic-setup/service/year-setup.service';
import { NavbarSettingService } from './views/featureManagement/service/navbar-setting.service';
import { DepartmentService } from './views/basic-setup/service/department.service';
import { SectionService } from './views/basic-setup/service/section.service';
import { RoleFeatureService } from './views/featureManagement/service/role-feature.service';
import { DesignationService } from './views/basic-setup/service/designation.service';
import { OrderTypeService } from './views/basic-setup/service/order-type.service';
import { OfficeOrderService } from './views/notifications/service/office-order.service';
const APP_CONTAINERS = [
  DefaultFooterComponent,
  DefaultHeaderComponent,
  DefaultLayoutComponent,
  ConfirmDialogComponent,
];

@NgModule({
  declarations: [AppComponent, ...APP_CONTAINERS],
  imports: [
    CalendarModule,
    MatTableModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AvatarModule,
    BreadcrumbModule,
    FooterModule,
    DropdownModule,
    GridModule,
    HeaderModule,
    SidebarModule,
    IconModule,
    NavModule,
    ButtonModule,
    FormModule,
    UtilitiesModule,
    ButtonGroupModule,
    ReactiveFormsModule,
    SidebarModule,
    SharedModule,
    TabsModule,
    ListGroupModule,
    ProgressModule,
    BadgeModule,
    ListGroupModule,
    CardModule,
    NgScrollbarModule,
    CoreModule,
    SharedCustomModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    CommonModule,
    SpinnerModule,
    OffcanvasModule,
  ],
  providers: [
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy,
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: DATE_PIPE_DEFAULT_OPTIONS, useValue: {dateFormat:"mediumDate"}},
    //{provide: DATE_PIPE_DEFAULT_OPTIONS, useValue: {dateFormat:"dd/MM/YYYY"}},
    IconSetService,
    Title,
    AuthService,
    FormRecordService ,
    YearSetupService,
    ToastrService,
    NavbarSettingService,
    DepartmentService,
    SectionService,
    RoleFeatureService,
    DesignationService,
    OfficeOrderService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

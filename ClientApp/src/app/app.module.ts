import { DATE_PIPE_DEFAULT_OPTIONS, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { CommonModule } from '@angular/common'; 

import { ConfirmDialogComponent } from 'src/app/modals/confirm-dialog/confirm-dialog.component';

// Import routing module
import { AppRoutingModule } from './app-routing.module';
import { CoreModule } from './core/core.module';
// Import app component
import { AppComponent } from './app.component';
import { OffcanvasModule, SpinnerModule } from '@coreui/angular';

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
import { ToastrModule } from 'ngx-toastr';
import { ErrorInterceptor } from './core/interceptor/error.interceptor';
import { JwtInterceptor } from './core/interceptor/jwt.interceptor';
import { AuthService } from './core/service/auth.service';
import { SharedCustomModule } from './shared/shared.module';
import { FormRecordService } from './views/appraisal/services/form-record.service';
import { YearSetupService } from './views/basic-setup/service/year-setup.service';
const APP_CONTAINERS = [
  DefaultFooterComponent,
  DefaultHeaderComponent,
  DefaultLayoutComponent,
  ConfirmDialogComponent,
];

@NgModule({
  declarations: [AppComponent, ...APP_CONTAINERS],
  imports: [
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
    OffcanvasModule
  ],
  providers: [
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy,
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    {provide: DATE_PIPE_DEFAULT_OPTIONS, useValue: {dateFormat:"mediumDate"}},
    //{provide: DATE_PIPE_DEFAULT_OPTIONS, useValue: {dateFormat:"dd/MM/YYYY"}},
    IconSetService,
    Title,
    AuthService,
    FormRecordService ,
    YearSetupService
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  AlertComponent,
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
  PopoverModule,
  ProgressModule,
  SharedModule,
  SidebarModule,
  SpinnerModule,
  TableModule,
  TooltipModule,
} from '@coreui/angular';
import { ToastrService } from 'ngx-toastr';
import { FeatureManagementRoutingModule } from './featureManagement-routing.module';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { ModuleListComponent } from './module-list/module-list.component';
import { CreateModuleComponent } from './create-module/create-module.component';
import { CreateFeatureComponent } from './create-feature/create-feature.component';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { IconModule } from '@coreui/icons-angular';
import { SiteSettingComponent } from './site-setting/site-setting.component';
import { CreateSiteSettingComponent } from './create-site-setting/create-site-setting.component';
import { NavbarThemListComponent } from './navbar-them-list/navbar-them-list.component';
import { CreateNavbarThemComponent } from './create-navbar-them/create-navbar-them.component';
import { ColorPickerModule } from 'primeng/colorpicker';


@NgModule({
  declarations: [
    FeatureListComponent,
    ModuleListComponent,
    CreateModuleComponent,
    CreateFeatureComponent,
    SiteSettingComponent,
    CreateSiteSettingComponent,
    NavbarThemListComponent,
    CreateNavbarThemComponent,
  ],
  imports: [
    CommonModule,
    FeatureManagementRoutingModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
    FormModule,
    ButtonModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    SharedModule,
    ProgressModule,
    SpinnerModule,
    CollapseDirective,
    MatFormFieldModule,
    MatTableModule,
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
    BadgeModule,
    ColorPickerModule,
    SidebarModule,
  ],
  providers: [ 
    ToastrService,
    // UserService,
  ],
})
export class FeatureManagementModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
  SpinnerModule,
} from '@coreui/angular';
import { ToastrService } from 'ngx-toastr';
import { FeatureManagementRoutingModule } from './featureManagement-routing.module';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { ModuleListComponent } from './module-list/module-list.component';
import { CreateModuleComponent } from './create-module/create-module.component';
import { CreateFeatureComponent } from './create-feature/create-feature.component';


@NgModule({
  declarations: [
    FeatureListComponent,
    ModuleListComponent,
    CreateModuleComponent,
    CreateFeatureComponent
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
  ],
  providers: [ 
    ToastrService,
    // UserService,
  ],
})
export class FeatureManagementModule { }

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
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { ToastrService } from 'ngx-toastr';

import { UsermanagementRoutingModule } from './usermanagement-routing.module';
import { UserComponent } from './user/user.component';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { UserService } from './service/user.service';


@NgModule({
  declarations: [
    UserComponent
  ],
  imports: [
    CommonModule,
    UsermanagementRoutingModule,
    DocsComponentsModule,
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
    SharedCustomModule,
    ProgressModule,
    SpinnerModule,
    CollapseDirective,
  ],
  providers: [ 
    ToastrService,
    UserService,
  ],
})
export class UsermanagementModule { }

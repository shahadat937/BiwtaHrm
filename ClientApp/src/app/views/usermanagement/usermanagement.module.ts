import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  AlertComponent,
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
  SpinnerModule,
  TableModule,
  TooltipModule,
} from '@coreui/angular';
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import { ToastrService } from 'ngx-toastr';

import { UsermanagementRoutingModule } from './usermanagement-routing.module';
import { UserComponent } from './user/user.component';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { UserService } from './service/user.service';
import { UpdateUserComponent } from './update-user/update-user.component';
import { UpdateRoleComponent } from './update-role/update-role.component';
import { RoleFeatureComponent } from './role-feature/role-feature.component';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { IconModule } from '@coreui/icons-angular';
import { RolesComponent } from './roles/roles.component';
import { RoleDashboardComponent } from './role-dashboard/role-dashboard.component';


@NgModule({
  declarations: [
    UserComponent,
    UpdateUserComponent,
    UpdateRoleComponent,
    RoleFeatureComponent,
    RolesComponent,
    RoleDashboardComponent
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
    TooltipModule,
    FormModule,
    ReactiveFormsModule,
    FormModule,
    MatFormFieldModule,
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
    ModalBodyComponent,
    ModalComponent,
    ModalFooterComponent,
    ModalHeaderComponent,
    IconModule,
    MatIconModule,
    MatButtonModule,
    TableModule,
    PopoverModule,
    AlertComponent,
  ],
  providers: [ 
    ToastrService,
    UserService,
  ],
})
export class UsermanagementModule { }

import { UserComponent } from './user/user.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleFeatureComponent } from './role-feature/role-feature.component';
import { RolesComponent } from './roles/roles.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: $localize`User Management`
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'user',
      },
      {
        path: 'user',
        component: UserComponent,
        data: {
          title: 'User',
        },
      },

      {
        path: 'update-user/:id',
        component: UserComponent,
        data: {
          title: 'Update User',
        },
      },
      {
        path: 'rolePermission',
        component: RoleFeatureComponent,
        data: {
          title: 'Role Permission',
        },
      },
      {
        path: 'roles',
        component: RolesComponent,
        data: {
          title: 'Roles',
        },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsermanagementRoutingModule { }

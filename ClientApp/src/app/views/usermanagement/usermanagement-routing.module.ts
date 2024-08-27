import { UserComponent } from './user/user.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleFeatureComponent } from './role-feature/role-feature.component';

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
          title: 'User',
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

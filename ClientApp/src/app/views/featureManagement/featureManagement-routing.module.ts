import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModuleListComponent } from './module-list/module-list.component';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { RoleFeatureComponent } from './role-feature/role-feature.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Menu Setup',
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'user',
      },
      {
        path: 'module',
        component: ModuleListComponent,
        data: {
          title: 'Module List',
        },
      },
      {
        path: 'feature',
        component: FeatureListComponent,
        data: {
          title: 'Feature List',
        },
      },
      {
        path: 'roleFeature',
        component: RoleFeatureComponent,
        data: {
          title: 'Role Feature',
        },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeatureManagementRoutingModule { }

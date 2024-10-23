import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModuleListComponent } from './module-list/module-list.component';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { SiteSettingComponent } from './site-setting/site-setting.component';
import { NavbarThemListComponent } from './navbar-them-list/navbar-them-list.component';

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
        path: 'siteSetting',
        component: SiteSettingComponent,
        data: {
          title: 'Site Setting',
        },
      },
      {
        path: 'navbar-them',
        component: NavbarThemListComponent,
        data: {
          title: 'Navbar Them',
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

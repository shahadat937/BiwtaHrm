import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DefaultLayoutComponent } from './containers';
import { Page404Component } from './views/pages/page404/page404.component';
import { Page500Component } from './views/pages/page500/page500.component';
import { LoginComponent } from './views/pages/login/login.component';
import { RegisterComponent } from './views/pages/register/register.component';
import { AuthGuard } from './core/guard/auth.guard';
import { Role } from './core/models/role';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        
        path: 'dashboard',
       canActivate: [AuthGuard],
        // data: {
        //   role: [Role.MasterAdmin]
        // },
        loadChildren: () =>
          import('./views/dashboard/dashboard.module').then((m) => m.DashboardModule)
      },
      {
        path: 'profile',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/profile/profile.module').then((m) => m.ProfileModule)
      },
      {
        path: 'theme',
        loadChildren: () =>
          import('./views/theme/theme.module').then((m) => m.ThemeModule)
      },
      {
        path: 'base',
        loadChildren: () =>
          import('./views/base/base.module').then((m) => m.BaseModule)
      },
      {
        path: 'buttons',
        loadChildren: () =>
          import('./views/buttons/buttons.module').then((m) => m.ButtonsModule)
      },
      {
        path: 'forms',
        loadChildren: () =>
          import('./views/forms/forms.module').then((m) => m.CoreUIFormsModule)
      },
      {
        path: 'charts',
        loadChildren: () =>
          import('./views/charts/charts.module').then((m) => m.ChartsModule)
      },
      {
        path: 'icons',
        loadChildren: () =>
          import('./views/icons/icons.module').then((m) => m.IconsModule)
      },
      {
        path: 'notifications',
        loadChildren: () =>
          import('./views/notifications/notifications.module').then((m) => m.NotificationsModule)
      },
      {
        path: 'widgets',
        loadChildren: () =>
          import('./views/widgets/widgets.module').then((m) => m.WidgetsModule)
      },
      {
        path: 'pages',
        loadChildren: () =>
          import('./views/pages/pages.module').then((m) => m.PagesModule)
      },
      {
        path: 'personalInfoSetup',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/basic-setup/basic-setup.module').then((m) => m.BasicSetupModule)
      },
      {
        path: 'addressSetup',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/basic-setup/basic-setup.module').then((m) => m.BasicSetupModule)
      },
      {
        path: 'educationSetup',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/basic-setup/basic-setup.module').then((m) => m.BasicSetupModule)
      },
      {
        path: 'trainingSetup',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/basic-setup/basic-setup.module').then((m) => m.BasicSetupModule)
      },
      {
        path: 'bankInfoSetup',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/basic-setup/basic-setup.module').then((m) => m.BasicSetupModule)
      },
      {
        path: 'languageSetup',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/basic-setup/basic-setup.module').then((m) => m.BasicSetupModule)
      },
      {
        path: 'officeSetup',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./views/basic-setup/basic-setup.module').then((m) => m.BasicSetupModule)
      },
      {
        path: 'usermanagement',
        loadChildren: () =>
          import('./views/usermanagement/usermanagement.module').then((m) => m.UsermanagementModule)
      },
      {
        path: 'employee',
        loadChildren: () =>
          import('./views/employee/employee.module').then((m) => m.EmployeeModule)
      },
      {
        path: 'attendance',
        loadChildren: () =>
          import('./views/attendance/attendance.module').then((m) => m.AttendanceModule)
      }
      ,
      {
        path: 'leave',
        loadChildren: () =>
          import('./views/Leave/leave.module').then((m) => m.LeaveModule)
      }
      ,
      {
        path: 'transferPosting',
        loadChildren: () =>
          import('./views/transferPosting/transfer.module').then((m) => m.TransferPostingModule)
      }
      ,
      {
        path: 'promotion',
        loadChildren: () =>
          import('./views/promotion/promotion.module').then((m) => m.PromotionModule)
      }
      ,
      {
        path: 'promotion',
        loadChildren: () =>
          import('./views/promotion/promotion.module').then((m) => m.PromotionModule)
      }
      ,
      {
        path: 'appraisal',
        loadChildren: () =>
          import('./views/appraisal/appraisal.module').then((m) => m.AppraisalModule)
      }
      ,
      {
        path: 'navbarsetup',
        loadChildren: () =>
          import('./views/featureManagement/featureManagement.module').then((m) => m.FeatureManagementModule)
      },
      {
        path: 'device',
        loadChildren: () => 
          import('./views/device/device.module').then((m) => m.DeviceModule)
      },
      {
        path: 'report',
        loadChildren: () =>
          import('./views/reporting/reporting.module').then((m) => m.ReportingModule)
      },

    ]
  },
  {
    path: '404',
    component: Page404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: Page500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
   
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {path: '**', redirectTo: 'dashboard'}


];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking'
      // relativeLinkResolution: 'legacy'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

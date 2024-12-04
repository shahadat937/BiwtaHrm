import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewUsersComponent } from './add-employee/view-users/view-users.component';
import { ViewInformationListComponent } from './add-employee/view-information-list/view-information-list.component';
import { EmployeeListComponent } from './manage-employee/employee-list/employee-list.component';
import { EmployeeInformationComponent } from './manage-employee/employee-information/employee-information.component';
import { ViewEmployeeComponent } from './add-employee/view-employee/view-employee.component';
import { EmpIdCardGenerateComponent } from './manage-employee/emp-id-card-generate/emp-id-card-generate.component';
import { EmpShiftListComponent } from './assignShift/emp-shift-list/emp-shift-list.component';
import { ViewEmployeePrimeNgComponent } from './add-employee/view-employee-prime-ng/view-employee-prime-ng.component';
import { EmpProfileComponent } from './manage-employee/emp-profile/emp-profile.component';
import { UploadFingerPrintComponent } from './add-employee/employee-informations/upload-finger-print/upload-finger-print.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Employee Management',
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'addEmployee',
      },
      {
        path: 'employeeList',
        component: ViewEmployeeComponent,
        data: {
          title: 'Employee List',
        },
      },
      {
        path: 'allEmployeeList',
        component: ViewEmployeePrimeNgComponent,
        data: {
          title: 'Employee List',
        },
      },
      {
        path: 'manageEmployee',
        component: EmployeeListComponent,
        data: {
          title: 'Manage Employee',
        },
      },
      {
        path: 'create-new-employee',
        component: ViewInformationListComponent,
        data: {
          title: 'Create New Employee',
        },
      },
      {
        path: 'update-employee-information/:id',
        component: ViewInformationListComponent,
        data: {
          title: 'Update Employee Information',
        },
      },
      
      {
        path: 'employee-information/:id',
        component: EmployeeInformationComponent,
        data: {
          title: 'View Employee Information',
        },
      },
      {
        path: 'employee-id-card/:id',
        component: EmpIdCardGenerateComponent,
        data: {
          title: 'Employee ID Card',
        },
      },
      {
        path: 'shiftAssign',
        component: EmpShiftListComponent,
        data: {
          title: 'Employee Assigned Shift',
        },
      },
      {
        path: 'profile',
        component: EmpProfileComponent,
        data: {
          title: 'Employee Profile',
        },
      },
      {
        path: 'upload-fingerPrint',
        component: UploadFingerPrintComponent,
        data: {
          title: 'Employee Profile',
        },
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }

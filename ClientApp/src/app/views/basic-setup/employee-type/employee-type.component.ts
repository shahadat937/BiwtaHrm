import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmployeeTypeService } from '../service/employee-type.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { FeatureManagementService } from '../../featureManagement/service/feature-management.service';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';

@Component({
  selector: 'app-employee-type',
  templateUrl: './employee-type.component.html',
  styleUrl: './employee-type.component.scss',
})
export class EmployeeTypeComponent {
  btnText: string | undefined;
  headerText: string | undefined;
  featurePermission : FeaturePermission = new FeaturePermission;
  @ViewChild('employeeTypeForm', { static: true }) employeeTypeForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'employeeTypeName',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public employeeTypeService: EmployeeTypeService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public featureManagementService: FeatureManagementService,
    public roleFeatureService: RoleFeatureService,
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
   this. getPermission()
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('employeeTypeId');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Employee Type';
        this.employeeTypeService.find(+id).subscribe((res) => {
          this.employeeTypeForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Employee Type';
        this.btnText = 'Submit';
      }
    });
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }
  getPermission(){
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    var roleName = currentUserJSON.role;

    this.roleFeatureService.getFeaturePermission(roleName, 'employee-type').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.getEmployeeTypes();
        this.handleRouteParams();
      }
      else{
        this.unauthorizeAccress()
        this.router.navigate(['/dashboard']);
      }
    });
  }
  unauthorizeAccress(){
    this.toastr.warning('Unauthorized Access', ` `, {
      positionClass: 'toast-top-right',
    });
  }
  initaialBloodGroup(form?: NgForm) {
    if (form != null) form.resetForm();
    this.employeeTypeService.employeeType = {
      employeeTypeId: 0,
      employeeTypeName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.employeeTypeForm?.form != null) {
      this.employeeTypeForm.form.reset();
      this.employeeTypeForm.form.patchValue({
        employeeTypeId: 0,
        employeeTypeName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/personalInfoSetup/employee-type']);
  }

  getEmployeeTypes() {
    this.subscription = this.employeeTypeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  //  onSubmit(form:NgForm){
  //   this.employeeTypeService.cachedData = [];
  //   const id = this.employeeTypeForm.form.get('employeeTypeId')?.value;
  //   if (id) {
  //     this.employeeTypeService.update(+id,this.employeeTypeForm.value).subscribe((response:any) => {

  //       if(response.success){
  //         this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
  //         this.employeeType()
  //         this.resetForm();
  //         this.router.navigate(["/bascisetup/employee-type"]);
  //       }else{
  //         this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
  //       }

  //     }, err => {
  //       console.log(err)
  //     })
  //   }else{
  //  this.subscription=this.employeeTypeService.submit(form?.value).subscribe((response:any)=>{
  //   if(response.success){
  //     this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
  //     this.employeeType()
  //     this.resetForm();
  //   }else{
  //     this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
  //   }

  //  },err=>{
  //    console.log(err);
  //  })
  //   }

  // }
  onSubmit(form: NgForm): void {
    if(this.featurePermission.add == true && !form.value.employeeTypeId || this.featurePermission.update == true && form.value.employeeTypeId){
      this.employeeTypeService.cachedData = [];
    const id = form.value.employeeTypeId;
    const action$ = id
      ? this.employeeTypeService.update(id, form.value)
      : this.employeeTypeService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getEmployeeTypes();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/personalInfoSetup/employee-type']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });
    }
    else {
      this.unauthorizeAccress();
    }
  }
  delete(element: any) {
    if(this.featurePermission.delete == true){
      this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.employeeTypeService.delete(element.employeeTypeId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
              console.log(err);
            }
          );
        }
      });
    }
    else {
      this.unauthorizeAccress();
    }
  }
}

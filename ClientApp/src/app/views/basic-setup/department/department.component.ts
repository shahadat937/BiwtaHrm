import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { DepartmentService } from './../service/department.service';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { OfficeService } from '../service/office.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrl: './department.component.scss',
})
export class DepartmentComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  BtnText: string | undefined;
  btnText: string | undefined;
  headerText: string | undefined;
  buttonIcon: string | undefined;
  offices: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  upperDepartmentView = false;
  loading = false;
  @ViewChild('DepartmentForm', { static: true }) DepartmentForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo', 
    'officeName',
    'upperDepartmentName',
    'departmentName', 
    // 'departmentNameBangla',
    'isActive', 
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public departmentService: DepartmentService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public officeService : OfficeService,
  ) {
  }
  ngOnInit(): void {
    this.getALlDepartments();
    this.handleRouteParams();
    this.loadOffice();
  }

  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('departmentId');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.headerText = "Update Department";
        this.BtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.departmentService.getById(+id).subscribe((res) => {
          this.onOfficeSelect(res.officeId);
          this.DepartmentForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.btnText = 'Submit';
        this.visible = false;
        this.headerText = "Department List"
        this.buttonIcon = "cilPencil";
        this.BtnText = " Add Department";
        this.departmentService.departments.officeId = null;
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
  
  UserFormView() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('departmentId');
      if(id){
        if (this.BtnText == " View Form") {
          this.BtnText = " Hide Form";
          this.buttonIcon = "cilTrash";
          this.headerText = "Update Department";
          this.visible = true;
        }
        else {
          this.BtnText = " View Form";
          this.buttonIcon = "cilPencil";
          this.headerText = "Department List";
          this.visible = false;
        }
      }
      else {
        if (this.BtnText == " Add Department") {
          this.BtnText = " Hide Form";
          this.buttonIcon = "cilTrash";
          this.headerText = "Add New Department";
          this.visible = true;
        }
        else {
          this.BtnText = " Add Department";
          this.buttonIcon = "cilPencil";
          this.headerText = "Department List";
          this.visible = false;
        }
      }
    });
  }

  toggleCollapse() {
    this.handleRouteParams();
    this.visible = true;
  }

  cancelUpdate() {
    this.resetForm();
    this.router.navigate(['/officeSetup/department']);
  }

  initaialDepartment(form?: NgForm) {
    if (form != null) form.resetForm();
    this.departmentService.departments = {
      departmentId: 0,
      departmentName: "",
      departmentNameBangla: "",
      departmentCode: "",
      officeId: null,
      upperDepartmentId: null,
      phone: "",
      mobile: "",
      fax: "",
      email: "",
      sequence: null,
      remark: "",
      menuPosition: 0,
      isActive: true,
      officeName: "",
      upperDepartmentName: "",
    };
    
  }
  resetForm() {
    if (this.DepartmentForm?.form != null) {
      this.DepartmentForm.form.reset();
      this.DepartmentForm.form.patchValue({
        departmentId: 0,
        departmentName: "",
        departmentNameBangla: "",
        departmentCode: "",
        officeId: null,
        upperDepartmentId: null,
        phone: "",
        mobile: "",
        fax: "",
        email: "",
        sequence: null,
        remark: "",
        menuPosition: 0,
        isActive: true,
      });
    }
    // this.router.navigate(['/officeSetup/department']);
  }

  getALlDepartments() {
    this.subscription = this.departmentService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  
  loadOffice() { 
    this.subscription=this.officeService.selectGetoffice().subscribe((data) => { 
      this.offices = data;
    });
  }

  onOfficeSelect(officeId : number){
    this.departmentService.departments.upperDepartmentId = null;
    this.departmentService.getSelectedDepartmentByOfficeId(+officeId).subscribe((res) => {
      this.departments = res;
      if(res.length>0){
        this.upperDepartmentView = true;
      }
      else{
        this.upperDepartmentView = false;
      }
    });
  }
  onOfficeSelectGetDepartment(officeId : number){
    if(officeId == null){
      this.getALlDepartments();
    }
    else {
      this.departmentService.onOfficeSelectGetDepartment(+officeId).subscribe((res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      });
    }
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.departmentService.cachedData = [];
    const id = form.value.departmentId;
    const action$ = id
      ? this.departmentService.update(id, form.value)
      : this.departmentService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlDepartments();
        this.resetForm();
        this.router.navigate(['/officeSetup/department']);
        this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      this.loading = false;
    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.departmentService.delete(element.departmentId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
  }
}

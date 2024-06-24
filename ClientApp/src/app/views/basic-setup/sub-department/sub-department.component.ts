import { Country } from './../model/Country';
import { SubDepartmentService } from './../service/sub-department.service';
import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { DepartmentService } from '../service/department.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-sub-department',
  templateUrl: './sub-department.component.html',
  styleUrl: './sub-department.component.scss'
})
export class SubDepartmentComponent {
  editMode: boolean = false;
  departments: any = [];
  subDepartments:SelectedModel[]=[];

  btnText: string | undefined;
  loading = false;
  @ViewChild('SubDepartmentForm', { static: true }) SubDepartmentForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'subDepartmentName',
    'departmentName',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public subDepartmentService: SubDepartmentService,
    private departmentService: DepartmentService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getAllSubDepartments();
    this.SelectModelDepartment();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('subDepartmentId');
      if (id) {
        this.btnText = 'Update';
        this.subDepartmentService.find(+id).subscribe((res) => {
          this.onSubDepartmentNamesChangeByDepartmentId(res.departmentId);
          this.SubDepartmentForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  SelectModelDepartment() {
    this.departmentService.getSelectDepartments().subscribe((data) => {
      this.departments = data;
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

  initaialSubDepartment(form?: NgForm) {
    if (form != null) form.resetForm();
    this.subDepartmentService.subDepartments = {
      subDepartmentId: 0,
      subDepartmentName: '',
      departmentId: 0,
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    if (this.SubDepartmentForm?.form != null) {
      this.SubDepartmentForm.form.reset();
      this.SubDepartmentForm.form.patchValue({
        subDepartmentId: 0,
        subDepartmentName: '',
        departmentId: 0,
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/subDepartment']);
  }
  getAllSubDepartments() {
    this.subscription = this.subDepartmentService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
 
  onSubDepartmentNamesChangeByDepartmentId(departmentId:number){
    this.subscription=this.subDepartmentService.getSubDepartmentByDepartmentId(departmentId).subscribe((data) => { 
        this.subDepartments = data;
      });
  }

  // loadDepartments() { 
  //   this.subscription=this.departmentService.getSelectDepartments().subscribe((data) => { 
  //     this.departments = data;
  //   });
  // }

 
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.subDepartmentService.cachedData = [];
    const id = form.value.subDepartmentId;
    const action$ = id
      ? this.subDepartmentService.update(id, form.value)
      : this.subDepartmentService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllSubDepartments();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/subDepartment']);
        }
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
          this.subDepartmentService.delete(element.subDepartmentId).subscribe(
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
}

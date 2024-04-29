import { SubjectService } from './../service/subject.service';
import { SubBranchService } from './../service/sub-branch.service';
import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { BranchService } from '../service/branch.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sub-branch',
  templateUrl: './sub-branch.component.html',
  styleUrl: './sub-branch.component.scss'
})
export class SubBranchComponent {
  editMode: boolean = false;
  branchs: any = [];

  btnText: string | undefined;
  loading = false;
  @ViewChild('SubBranchForm', { static: true }) SubBranchForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'subBranchName',
    'branchName',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public subBranchsService: SubBranchService,
    private branchService: BranchService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) { }
  ngOnInit(): void {
    this.getAllSubBranchs();
    this.SelectModelBranchs();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('subBranchId');
      if (id) {
        this.btnText = 'Update';
        this.subBranchsService.find(+id).subscribe((res) => {
          this.SubBranchForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  SelectModelBranchs() {
    this.branchService.getSelectBranch().subscribe((data) => {
      this.branchs = data;
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

  initaialSubBranch(form?: NgForm) {
    if (form != null) form.resetForm();
    this.subBranchsService.subBranchs = {
      subBranchId: 0,
      subBranchName: '',
      branchId: 0,
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    if (this.SubBranchForm?.form != null) {
      this.SubBranchForm.form.reset();
      this.SubBranchForm.form.patchValue({
        subBranchId: 0,
        subBranchName: '',
        branchId: 0,
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/SubBranch']);
  }
  getAllSubBranchs() {
    this.subscription = this.subBranchsService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }


  onSubmit(form: NgForm): void {
    this.loading = true;
    this.subBranchsService.cachedData = [];
    const id = form.value.subBranchId;
    const action$ = id
      ? this.subBranchsService.update(id, form.value)
      : this.subBranchsService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllSubBranchs();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/subBranch']);
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
          this.subBranchsService.delete(element.subBranchId).subscribe(
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
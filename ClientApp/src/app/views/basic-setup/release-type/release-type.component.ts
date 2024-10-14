import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ReleaseTypeService } from '../service/release-type.service';

@Component({
  selector: 'app-release-type',
  templateUrl: './release-type.component.html',
  styleUrl: './release-type.component.scss'
})
export class ReleaseTypeComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('ReleaseTypeForm', { static: true }) ReleaseTypeForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'releaseTypeName', 'isDeptRelease','isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public releaseTypeService: ReleaseTypeService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getALlReleaseTypes();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('releaseTypeId');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Blood Group';
        this.releaseTypeService.getById(+id).subscribe((res) => {
          this.ReleaseTypeForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Release Type';
        this.btnText = 'Submit';
        this.initaialReleaseType();
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

  initaialReleaseType(form?: NgForm) {
    if (form != null) form.resetForm();
    this.releaseTypeService.releaseType = {
      releaseTypeId: 0,
      releaseTypeName: '',
      isDeptRelease: true,
      remark: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.ReleaseTypeForm?.form != null) {
      this.ReleaseTypeForm.form.reset();
      this.ReleaseTypeForm.form.patchValue({
        releaseTypeId: 0,
        releaseTypeName: '',
        isDeptRelease: true,
        remark: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/personalInfoSetup/releaseType']);
  }

  getALlReleaseTypes() {
    this.subscription = this.releaseTypeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.releaseTypeService.cachedData = [];
    const id = form.value.releaseTypeId;
    const action$ = id
      ? this.releaseTypeService.update(id, form.value)
      : this.releaseTypeService.submit(form.value);
    
    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlReleaseTypes();
        this.resetForm();
        this.router.navigate(['/personalInfoSetup/releaseType']);
      this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      this.loading = false;
      }
    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.releaseTypeService.delete(element.releaseTypeId).subscribe(
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
              console.log(err);
            }
          );
        }
      });
  }


}

import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PoolService } from './../service/pool.service';
@Component({
  selector: 'app-pool',
  templateUrl: './pool.component.html',
  styleUrl: './pool.component.scss',
})
export class PoolComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  
  @ViewChild('PoolForm', { static: true }) PoolForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'poolName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public poolService: PoolService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getALlPools();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('poolId');
      if (id) {
        this.btnText = 'Update';
        this.poolService.find(+id).subscribe((res) => {
          this.PoolForm?.form.patchValue(res);
        });
      } else {
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

  initaialPool(form?: NgForm) {
    if (form != null) form.resetForm();
    this.poolService.pools = {
      poolId: 0,
      poolName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.PoolForm?.form != null) {
      this.PoolForm.form.reset();
      this.PoolForm.form.patchValue({
        poolId: 0,
        poolName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/pool']);
  }

  getALlPools() {
    this.subscription = this.poolService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.poolService.cachedData = [];
    const id = form.value.poolId;
    const action$ = id
      ? this.poolService.update(id, form.value)
      : this.poolService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlPools();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/pool']);
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
          this.poolService.delete(element.poolId).subscribe(
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

import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';

@Component({
  selector: 'app-release',
  templateUrl: './release.component.html',
  styleUrl: './release.component.scss'
})
export class ReleaseComponent implements OnInit, OnDestroy, AfterViewInit {
  postingOrderInfoId: number | null = null;
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('postingOrderForm', { static: true }) postingOrderForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'officeOrderNo', 'officeOrderDate','transferSection','releaseType','isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public postingOrderInfoService: PostingOrderInfoService,
    public transferApproveInfoService: TransferApproveInfoService,

    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('postingOrderInfoId');
      if (id) {
        this.btnText = 'Update';
        this.postingOrderInfoService.find(+id).subscribe((res) => {
          this.postingOrderForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getTransferEmployes();
    this.getTransferApproved();
    this.route.paramMap.subscribe(params => {
      this.postingOrderInfoId = +params.get('postingOrderInfoId')!;
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
  toggleToast() {
    this.visible = !this.visible;
  }

  onVisibleChange($event: boolean) {
    this.visible = $event;
    this.percentage = !this.visible ? 0 : this.percentage;
  }

  onTimerChange($event: number) {
    this.percentage = $event * 25;
  }
  initaialWard(form?: NgForm) {
    if (form != null) form.resetForm();
    this.postingOrderInfoService.postingOrderInfos = {
      postingOrderInfoId:   0,
      empId:0,
      departmentId:null,
      subBranchId:0,
      subDepartmentId:0,
      designationId:null,
      officeId: null,
      officeOrderNo:"",
      officeOrderDate:new Date(),
      orderOfficeBy:"",
      transferSection:"",
      releaseType:"",
      menuPosition:  0,
      isActive:  true

    };
  }
  resetForm() {

    this.btnText = 'Submit';
    if (this.postingOrderForm?.form != null) {
  
      this.postingOrderForm.form.reset();
      this.postingOrderForm.form.patchValue({
        postingOrderInfoId:   0,
        empId:0,
        departmentId:null,
        subBranchId:0,
        subDepartmentId:0,
        designationId:null,
        officeId: null,
        officeOrderNo:"",
        officeOrderDate:new Date(),
        orderOfficeBy:"",
        transferSection:"",
        releaseType:"",
        menuPosition:  0,
        isActive:  true
      });
    }
  }

  getTransferEmployes() {
    this.subscription = this.postingOrderInfoService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  getTransferApproved() {
this.subscription= this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((res)=>{
console.log(res)
})
  }
  onSubmit(form: NgForm): void {
    this.postingOrderInfoService.cachedData = [];
    const id = form.value.postingOrderInfoId;
    //console.log(form.value)
    const action$ = id
      ? this.postingOrderInfoService.update(id, form.value)
      : this.postingOrderInfoService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getTransferEmployes();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/transfer/TransferOrderList']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          console.log(result)
          this.postingOrderInfoService.delete(element.postingOrderInfoId).subscribe(
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
              // console.log(err);

              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
  }
}
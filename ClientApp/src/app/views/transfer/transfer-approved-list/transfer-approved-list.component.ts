import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
import { EmployeesService } from '../service/employees.service';
import { TransferApproveInfo } from '../../basic-setup/model/transfer-approve-info';

@Component({
  selector: 'app-transfer-approved-list',
  templateUrl: './transfer-approved-list.component.html',
  styleUrl: './transfer-approved-list.component.scss'
})
export class TransferApprovedListComponent  implements OnInit, OnDestroy, AfterViewInit {
  visible1: boolean | undefined;
  userBtnText: string | undefined;
  postingOrderInfoId: number | null = null;
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('PostingAndTrnsForm', { static: true }) PostingAndTrnsForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo',"departmentName" ,"officeName","designationName",'officeOrderDate','transferSection','releaseType', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  transferApproveInfo:TransferApproveInfo[]=[];
  constructor(
    public postingOrderInfoService: PostingOrderInfoService,
    public transferApproveInfoService: TransferApproveInfoService,
    public employeeService: EmployeesService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
             this.btnText = 'Submit';
      
    });
  }

  UserFormView(): void {
    if (this.userBtnText == " Add Value") {
      this.userBtnText = " Hide Form";
      this.visible1 = true;
    } else {
      this.userBtnText = " Add Value";
      this.visible1 = false;
    }
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


  initaialtransferApproveInfo(form?: NgForm) {
    if (form != null) form.resetForm();
    this.transferApproveInfoService.transferApproveInfos = {
      transferApproveInfoId: 0,
      postingOrderInfoId:0,
      empId: 0,
      approveStatus: true,
      approveByName: "",
      approveBy:0,
      approveDate: new Date(),
      remarks: "",
      menuPosition: 0,
      isActive: true
    };
  }
  
  resetFormTransfer() {
    this.btnText = 'Submit';
    if (this.PostingAndTrnsForm?.form != null) {
      this.PostingAndTrnsForm.form.reset();
      this.PostingAndTrnsForm.form.patchValue({
        transferApproveInfoId: 0,
        postingOrderInfoId:0,
        empId: 0,
        approveByName: "",
        approveBy:0,
        approveStatus: true,
        approveDate: new Date(),
        remarks: "",
        menuPosition: 0,
        isActive: true
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
this.transferApproveInfo=res
})
  }
  onSubmit(form: NgForm): void {
    this.transferApproveInfoService.cachedData = [];
    const id = form.value.transferApproveInfoId;
    //console.log(form.value)
    const action$ = id
      ? this.transferApproveInfoService.update(id, form.value)
      : this.transferApproveInfoService.submit(form.value);
console.log(form.value)
    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getTransferEmployes();
        this.resetFormTransfer();
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

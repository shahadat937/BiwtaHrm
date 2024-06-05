import { get } from 'lodash-es';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { EmployeesService } from '../service/employees.service';
import { TransferApproveInfo } from '../../basic-setup/model/transfer-approve-info';
import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';
import { Employee } from '../../basic-setup/model/employees';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EmpModalComponent } from '../emp-modal/emp-modal.component';

@Component({
  selector: 'app-transfer-approved',
  templateUrl: './transfer-approved.component.html',
  styleUrl: './transfer-approved.component.scss'
})
export class TransferApprovedComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('TransferApproveInfoForm', { static: true }) TransferApproveInfoForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'wardName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  transferApproveInfos: TransferApproveInfo[] = [];
  postingOrderInfo: PostingOrderInfo[] = [];
  @Input() employeeSelected = new EventEmitter<Employee>();
  btnTextApproved: string | undefined;
  constructor(
    private modalService: BsModalService,
    public postingOrderInfoService: PostingOrderInfoService,
    public employeeService: EmployeesService,
    public transferApproveInfoService: TransferApproveInfoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const postingOrderInfoId = params.get('postingOrderInfoId');
      if (postingOrderInfoId) {
        this.loadPostingOrderInfo(Number(postingOrderInfoId));
        this.btnText='submit'
      } 
      const id = params.get('transferApproveInfoId');
      if (id) {
        this.btnTextApproved = 'Update';
        this.transferApproveInfoService.find(+id).subscribe((res) => {
          this.TransferApproveInfoForm?.form.patchValue(res);
          console.log('Form Values after patching:', this.TransferApproveInfoForm.form.value); // Debugging: Verify form values
        });
      } else {
        this.btnTextApproved = 'Submit';
      }
    });
  }
  loadPostingOrderInfo(postingOrderInfoId: number) {
    this.postingOrderInfoService.find(postingOrderInfoId).subscribe(data => {
      this.postingOrderInfoService.postingOrderInfos = data;
      // Ensure the form model is updated with the fetched data
      this.transferApproveInfoService.transferApproveInfos.postingOrderInfoId = data.postingOrderInfoId;
      // Optionally, load other necessary data into the form
    });
  }

  //Employee/Transfer
  openApproveEmpTransferJoin(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
      this.handleApproveEmpTransferJoin(selectedEmployee);
    });
  }
  handleApproveEmpTransferJoin(employee: Employee) {
    this.transferApproveInfoService.transferApproveInfos.approveBy= employee.empId,
    this.transferApproveInfoService.transferApproveInfos.approveByName= employee.employeeName
  }
  ngOnInit(): void {
    this.getAllTransferApproveInfo();
    this.loadTransferApproveInfos();
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

  // transferApproveInfos
  initaialtransferApproveInfo(form?: NgForm) {
    if (form != null) form.resetForm();
    this.transferApproveInfoService.transferApproveInfos = {
      transferApproveInfoId: 0,
      postingOrderInfoId: 0,
      empId: 0,
      approveStatus: true,
      approveByName: "",
      approveBy: 0,
      approveDate: new Date(),
      remarks: "",
      menuPosition: 0,
      isActive: true
    };
  }

  resetFormTransfer() {
    this.btnText = 'Submit';
    if (this.TransferApproveInfoForm?.form != null) {
      this.TransferApproveInfoForm.form.reset();
      this.TransferApproveInfoForm.form.patchValue({
        transferApproveInfoId: 0,
        postingOrderInfoId: 0,
        empId: 0,
        approveByName: "",
        approveBy: 0,
        approveStatus: true,
        approveDate: new Date(),
        remarks: "",
        menuPosition: 0,
        isActive: true
      });
    }
  }
  //
  loadTransferApproveInfos() {
    this.subscription = this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((h) => {
      this.transferApproveInfos= h;

    });
  }
  getAllTransferApproveInfo() {
    this.subscription = this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }


  onSubmit(form: NgForm): void {

    if (form.valid) {
      this.transferApproveInfoService.cachedData = [];
      const id = form.value.transferApproveInfoId;
      const action$ = id
        ? this.transferApproveInfoService.update(id, form.value)
        : this.transferApproveInfoService.submitApproved(form.value);
      this.subscription = action$.subscribe((response: any) => {
        if (response.success) {

          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.getAllTransferApproveInfo();
          this.resetFormTransfer();
          if (!id) {
            this.router.navigate(['/transfer/transferApproveInfoList']);
          }
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
        }
      });
    } else {
      this.toastr.error('Form is invalid');
    }

  }

  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.transferApproveInfoService.delete(element.wardId).subscribe(
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

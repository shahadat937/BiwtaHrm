import { DeptReleaseInfoService } from './../../basic-setup/service/dept-release-info.service';
import { DeptReleaseInfo } from './../../basic-setup/model/dept-release-info';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';
import { TransferApproveInfo } from '../../basic-setup/model/transfer-approve-info';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
import { EmployeesService } from '../service/employees.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EmpModalComponent } from '../emp-modal/emp-modal.component';
import { Employee } from '../../basic-setup/model/employees';

@Component({
  selector: 'app-departmetn-release',
  templateUrl: './departmetn-release.component.html',
  styleUrl: './departmetn-release.component.scss'
})
export class DepartmetnReleaseComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('DeptReleaseInfoForm', { static: true }) DeptReleaseInfoForm!: NgForm;
  subscription: Subscription = new Subscription();
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  transferApproveInfos: TransferApproveInfo[] = [];
  postingOrderInfo: PostingOrderInfo[] = [];
  deptReleaseInfo:DeptReleaseInfo[]=[];
  btnTextApproved: string | undefined;
  @Input() employeeSelected = new EventEmitter<Employee>();
  constructor(
    private modalService: BsModalService,
    public postingOrderInfoService: PostingOrderInfoService,
    public employeeService: EmployeesService,
    public transferApproveInfoService: TransferApproveInfoService,
    public deptReleaseInfoService:DeptReleaseInfoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const transferApproveInfoId = params.get('transferApproveInfoId');
      if (transferApproveInfoId) {
        this.loadTransferApproveInfo(Number(transferApproveInfoId));
        this.btnText='submit'
      } 
      const id = params.get('depReleaseInfoId');
      if (id) {
        this.btnTextApproved = 'Update';
        this.deptReleaseInfoService.find(+id).subscribe((res) => {
          this.DeptReleaseInfoForm?.form.patchValue(res);
           console.log('Form Values after patching:', this.DeptReleaseInfoForm.form.value); // Debugging: Verify form values
        });
      } else {
        this.btnTextApproved = 'Submit';
      }
    });
  }

loadTransferApproveInfo(transferApproveInfoId: number) {
  this.transferApproveInfoService.find(transferApproveInfoId).subscribe(data => {
    // Patch form with the fetched data
    this.DeptReleaseInfoForm.form.patchValue({
      transferApproveInfoId: data.transferApproveInfoId,
      postingOrderInfoId: data.postingOrderInfoId,
      empId: data.empId,
      approveStatus: data.approveStatus,
      approveDate: data.approveDate,
      approveBy: data.approveBy,
      approveByName: data.approveByName || '',  // Handle missing values
      remarks: data.remarks,
      menuPosition: data.menuPosition,
      isActive: data.isActive,
      employeeName: data.approveByName || ''  // Handle missing values
    });
  });
}
  //Employee/Transfer
  openApproveDepartmentRelease(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
      this.handleApproveDepartmentRelease(selectedEmployee);
    });
  }
  handleApproveDepartmentRelease(employee: Employee) {
    this.deptReleaseInfoService.deptReleaseInfo.approveBy= employee.empId,
    this.deptReleaseInfoService.deptReleaseInfo.approveByName= employee.employeeName
  }

  ngOnInit(): void {
    this.getAllDepartmentReleases();
    this.loadDepartmentalReleaseInfos();

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

  //Departmental Release Information
  initaialDepartmentalReleaseInfo(form?: NgForm) {
    if (form != null) form.resetForm();
    this.deptReleaseInfoService.deptReleaseInfo = {
      depReleaseInfoId: 0,
      postingOrderInfoId: 0,
      transferApproveInfoId: 0,
      empId: 0,
      approveByName: "",
      approveBy: 0,
      approveStatus: true,
      officeOrderNo: "",
      releaseDate: new Date(),
      orderOfficeBy: "",
      referenceNo: "",
      depClearance: "",
      releaseType: "",
      remarks: "",
      menuPosition: 0,
      isActive: true
    };
  }
  resetFormDepartmentalReleaseInfo() {
    this.btnText = 'Submit';
    if (this.DeptReleaseInfoForm?.form != null) {
      this.DeptReleaseInfoForm.form.reset();
      this.DeptReleaseInfoForm.form.patchValue({
        depReleaseInfoId: 0,
        postingOrderInfoId: 0,
        transferApproveInfoId: 0,
        approveByName: "",
        approveBy: 0,
        approveStatus: true,
        empId: 0,
        officeOrderNo: "",
        releaseDate: new Date(),
        orderOfficeBy: "",
        referenceNo: "",
        depClearance: "",
        releaseType: "",
        remarks: "",
        menuPosition: 0,
        isActive: true
      });
    }
  }
  loadDepartmentalReleaseInfos() {
    this.deptReleaseInfoService.getdeptReleaseInfoAll().subscribe((h) => {
      this.deptReleaseInfo = h;
    });
  }
  getAllDepartmentReleases() {
    this.subscription = this.deptReleaseInfoService.getdeptReleaseInfoAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm): void {
    console.log(form.value)
    if (form.valid) {
      this.deptReleaseInfoService.cachedData = [];
      const id = form.value.depReleaseInfoId;
      const action$ = id
        ? this.deptReleaseInfoService.update(id, form.value)
        : this.deptReleaseInfoService.submitDepRelease(form.value);
      this.subscription = action$.subscribe((response: any) => {
        if (response.success) {
          
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.getAllDepartmentReleases();
          this.resetFormDepartmentalReleaseInfo();
          if (!id) {
            this.router.navigate(['transfer/departmetnReleaseList']);
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
          this.deptReleaseInfoService.delete(element.depReleaseInfoId).subscribe(
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

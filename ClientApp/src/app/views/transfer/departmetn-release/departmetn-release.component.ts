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
    });
  }
  loadTransferApproveInfo(transferApproveInfoId: number) {
    this.transferApproveInfoService.find(transferApproveInfoId).subscribe(data => {
      this.transferApproveInfoService.transferApproveInfos = data;
      // Ensure the form model is updated with the fetched data
      console.log(data)
      this.deptReleaseInfoService.deptReleaseInfo.transferApproveInfoId = data.transferApproveInfoId;
      // this.deptReleaseInfoService.deptReleaseInfo.postingOrderInfoId = data.postingOrderInfoId;
      // this.deptReleaseInfoService.deptReleaseInfo.empId = data.empId;
      // Optionally, load other necessary data into the form
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
    this.loadApproveDepartmentRelease();
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
  resetdeptReleaseInfoForm() {
    this.btnText = 'Submit';
    if (this.DeptReleaseInfoForm?.form != null) {
      this.DeptReleaseInfoForm.form.reset();
      this.DeptReleaseInfoForm.form.patchValue({
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
  loadApproveDepartmentRelease() {
    this.subscription = this.deptReleaseInfoService.getdeptReleaseInfoAll().subscribe((h) => {
      this.getAllDepartmentReleases();

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
          this.resetdeptReleaseInfoForm();
          if (!id) {
            this.router.navigate(['/transfer/departmetnRelease']);
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

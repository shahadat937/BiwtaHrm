import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { DeptReleaseInfo } from '../../basic-setup/model/dept-release-info';
import { Employee } from '../../basic-setup/model/employees';
import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';
import { TransferApproveInfo } from '../../basic-setup/model/transfer-approve-info';
import { DeptReleaseInfoService } from '../../basic-setup/service/dept-release-info.service';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
import { EmpModalComponent } from '../emp-modal/emp-modal.component';
import { EmployeesService } from '../service/employees.service';
import { EmpTnsferPostingJoin } from '../../basic-setup/model/emp-tnsfer-posting-join';
import { EmpTnsferPostingJoinService } from '../../basic-setup/service/emp-tnsfer-posting-join.service';
import { EmployeesModule } from '../../employee/model/employees.module';

@Component({
  selector: 'app-employe-posting-join-approve',
  templateUrl: './employe-posting-join-approve.component.html',
  styleUrl: './employe-posting-join-approve.component.scss'
})
export class EmployePostingJoinApproveComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('EmpTransferPostingJoinForm', { static: true }) EmpTransferPostingJoinForm!: NgForm;
  subscription: Subscription = new Subscription();
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  transferApproveInfos: TransferApproveInfo[] = [];
  postingOrderInfo: PostingOrderInfo[] = [];
  deptReleaseInfo:DeptReleaseInfo[]=[];
  empTnsferPostingJoin: EmpTnsferPostingJoin[] = [];
  @Input() employeeSelected = new EventEmitter<EmployeesModule>();
  constructor(
    public empTnsferPostingJoinService: EmpTnsferPostingJoinService,
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
      const depReleaseInfoId = params.get('depReleaseInfoId');
      if (depReleaseInfoId) {
        this.loadEmpTnsferPostingJoin(Number(depReleaseInfoId));
        this.btnText='submit'
      } 
      const id = params.get('empTnsferPostingJoinId');
      if (id) {
        this.btnText = 'Update';
        this.empTnsferPostingJoinService.find(+id).subscribe((res) => {
          this.EmpTransferPostingJoinForm?.form.patchValue(res);
          // console.log('Form Values after patching:', this.DeptReleaseInfoForm.form.value); // Debugging: Verify form values
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }

  loadEmpTnsferPostingJoin(depReleaseInfoId: number) {
    this.deptReleaseInfoService.find(depReleaseInfoId).subscribe(data => {
      this.deptReleaseInfoService.deptReleaseInfo = data;
      // Ensure the form model is updated with the fetched data
      this.empTnsferPostingJoinService.empTnsferPostingJoin.depReleaseInfoId = data.depReleaseInfoId;

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
  

  //EmployeeJoin/Transfer
  openApproveEmpTransferJoin(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: EmployeesModule) => {
      this.handleApproveEmpTransferJoin(selectedEmployee);
    });
  }
  handleApproveEmpTransferJoin(employee: EmployeesModule) {
    this.empTnsferPostingJoinService.empTnsferPostingJoin.approveBy= employee.empId,
    this.empTnsferPostingJoinService.empTnsferPostingJoin.approveByName= employee.empEngName
  }

  ngOnInit(): void {
    this.getAllEmpTnsferPostingJoins();
    this.loadEmpTnsferPostingJoins();
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

  //EmpTnsferPostingJoin
  initaialEmpTnsferPostingJoin(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empTnsferPostingJoinService.empTnsferPostingJoin = {
      empTnsferPostingJoinId: 0,
      postingOrderInfoId:0,
      depReleaseInfoId:0,
      transferApproveInfoId:0,
      approveByName:"",
      approveBy: 0,
      approveStatus:true,
      empId: 0,
      joinDate: new Date(),
      remarks: "",
      menuPosition: 0,
      isActive: true
    };
  }
  resetFormEmpTnsferPostingJoin() {
    this.btnText = 'Submit';
    if (this.EmpTransferPostingJoinForm?.form != null) {
      this.EmpTransferPostingJoinForm.form.reset();
      this.EmpTransferPostingJoinForm.form.patchValue({
        empTnsferPostingJoinId: 0,
        postingOrderInfoId:0,
        depReleaseInfoId:0,
        transferApproveInfoId:0,
        approveByName:"",
        approveBy: 0,
        approveStatus:true,
        empId: 0,
        joinDate: new Date(),
        remarks: "",
        menuPosition: 0,
        isActive: true
      });
    }
  }
  loadEmpTnsferPostingJoins() {
    this.subscription = this.empTnsferPostingJoinService.getempTnsferPostingJoinAll().subscribe((h) => {
      this.empTnsferPostingJoin = h;
    });
  }
  getAllEmpTnsferPostingJoins() {
    this.subscription = this.empTnsferPostingJoinService.getempTnsferPostingJoinAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }


  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.empTnsferPostingJoinService.cachedData = [];
      const id = form.value.empTnsferPostingJoinId;
      const action$ = id
        ? this.empTnsferPostingJoinService.update(id, form.value)
        : this.empTnsferPostingJoinService.submit(form.value);
        console.log(id)
      this.subscription = action$.subscribe((response: any) => {
        if (response.success) {

          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.getAllEmpTnsferPostingJoins();
          this.resetFormEmpTnsferPostingJoin();
          if (!id) {
            this.router.navigate(['transfer/employePostingJoin']);
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
          this.empTnsferPostingJoinService.delete(element.empTnsferPostingJoinId).subscribe(
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

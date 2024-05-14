import { AfterViewInit, ChangeDetectorRef, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Employee, EmployeesService } from './../service/employees.service';
import { EmpModalComponent } from '../emp-modal/emp-modal.component';
import { DeptReleaseInfoService } from './../../basic-setup/service/dept-release-info.service';
import { DeptReleaseInfo } from './../../basic-setup/model/dept-release-info';
import { TransferApproveInfoService } from './../../basic-setup/service/transfer-approve-info.service';
import { TransferApproveInfo } from './../../basic-setup/model/transfer-approve-info';
import { DepartmentService } from './../../basic-setup/service/department.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { SubDepartmentService } from '../../basic-setup/service/sub-department.service';
import { BranchService } from '../../basic-setup/service/branch.service';
import { SubBranchService } from '../../basic-setup/service/sub-branch.service';
import { EmpTnsferPostingJoin } from './../../basic-setup/model/emp-tnsfer-posting-join';
import { EmpTnsferPostingJoinService } from './../../basic-setup/service/emp-tnsfer-posting-join.service';

@Component({
  selector: 'app-posting',
  templateUrl: './posting.component.html',
  styleUrls: ['./posting.component.scss']
})
export class PostingComponent implements OnInit, OnDestroy, AfterViewInit {

  editMode: boolean = false;
  departments: SelectedModel[] = [];
  subDepartments: SelectedModel[] = [];
  officeBranchs: SelectedModel[] = [];
  subBranchs: SelectedModel[] = [];
  transferApproveInfos: TransferApproveInfo[] = [];
  deptReleaseInfo: DeptReleaseInfo[] = [];
  empTnsferPostingJoin: EmpTnsferPostingJoin[] = [];
  employees: any[] = [];
  select: any[] = [];
  userBtnText: string | undefined;
  userHeaderText: string | undefined;
  buttonIcon: string = '';
  visible: boolean | undefined;

  btnText: string | undefined;
  loading = false;
  @ViewChild('EmployeeForm', { static: true }) EmployeeForm!: NgForm;
  @ViewChild('PostingAndTrnsForm', { static: true }) PostingAndTrnsForm!: NgForm;
  @ViewChild('TransferApproveInfoForm', { static: true }) TransferApproveInfoForm!: NgForm;
  @ViewChild('DeptReleaseInfoForm', { static: true }) DeptReleaseInfoForm!: NgForm;
  @ViewChild('EmpTransferPostingJoinForm', { static: true }) EmpTransferPostingJoinForm!: NgForm;

  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'officeOrderNo', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  @Input() employeeSelected = new EventEmitter<Employee>();

  constructor(

    public postingOrderInfoService: PostingOrderInfoService,
    private branchServices: BranchService,
    private departmentService: DepartmentService,
    private subDepartmentService: SubDepartmentService,
    private subBranchService: SubBranchService,
    public transferApproveInfoService: TransferApproveInfoService,
    public deptReleaseInfoService: DeptReleaseInfoService,
    public empTnsferPostingJoinService: EmpTnsferPostingJoinService,
    public employeeService: EmployeesService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public dialog: MatDialog,
    private _dialog: MatDialog,
    private modalService: BsModalService,
    private cdRef: ChangeDetectorRef,
    private formBuilder: FormBuilder,
    private fb: FormBuilder // Inject FormBuilder here
    
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('postingOrderInfoId');
      const id4 = params.get('depReleaseInfoId');
      const id5= params.get('empTnsferPostingJoinId')
      const id3= params.get('transferApproveInfoId')
      //transferApproveInfoId
      console.log(params)
      if (id3) {
        this.btnText = 'Submit'
      }
      //empTnsferPostingJoinId
      console.log(params)
      if (id4) {
        this.btnText = 'Submit'
      }
      //depreleseInfo
      console.log(params)
      if (id5) {
        this.btnText = 'Submit'
      }
      if (id) {
        this.btnText = 'Update';
        this.postingOrderInfoService.find(+id).subscribe((res) => {

          this.PostingAndTrnsForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  

  ngOnInit(): void {
    this.getAllPostingOrderInfos();
    this.loadTransferApproveInfos();
    this.loadEmpTnsferPostingJoins();
    this.loadDepartmentalReleaseInfos();
    this.SelectDepartments();
    this.SelectsubDepartments();
    this.SelectBranchs();
    this.SelectSubBranchs();
    this.handleRouteParams();
    this.buttonIcon = "cilPencil";
    //console.log(this.employeeSelected)
  }

  selectEmployee(employee: Employee) {
    this.employeeSelected.emit(employee);
    //console.log(employee)
  }

  SelectDepartments() {
    this.departmentService.getSelectDepartments().subscribe((data) => {
      this.departments = data;
    });
  }

  SelectsubDepartments() {
    this.subDepartmentService.getSelectSubDepartment().subscribe((res) => {
      this.subDepartments = res;
    });
  }

  SelectBranchs() {
    this.branchServices.getSelectBranch().subscribe((res) => {
      this.officeBranchs = res;
    });
  }

  SelectSubBranchs() {
    this.subBranchService.getSelectSubBranchs().subscribe((res) => {
      this.subBranchs = res;
    });
  }

  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('postingOrderInfoId');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.userHeaderText = "Update User";
        this.userBtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.postingOrderInfoService.find(+id).subscribe((res) => {
          this.onSubDepartmentNamesChangeByDepartmentId(res.departmentId);
          this.onSubBranchNamesChangeByOfficeBranchId(res.officeBranchId);

          this.PostingAndTrnsForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
        this.visible = false;
        this.userHeaderText = "Value List"
        this.userBtnText = " Add Value";
      }
    });
  }

  UserFormView(): void {
    if (this.userBtnText == " Add Value") {
      this.userBtnText = " Hide Form";
      this.buttonIcon = "cilTrash";
      this.userHeaderText = "Add New Value";
      this.visible = true;
    } else {
      this.userBtnText = " Add Value";
      this.buttonIcon = "cilPencil";
      this.userHeaderText = "Value List";
      this.visible = false;
    }
  }

  toggleCollapse() {
    this.handleRouteParams();
    this.userHeaderText = "Update Value";
    this.visible = true;
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

  initaialPostingOrderInfo(form?: NgForm) {
    if (form != null) form.resetForm();
    this.postingOrderInfoService.postingOrderInfos = {
      postingOrderInfoId: 0,
      empId: 0,
      departmentId: 0,
      subBranchId: 0,
      subDepartmentId: 0,
      officeBranchId: 0,
      officeOrderNo: "",
      officeOrderDate: new Date(),
      orderOfficeBy: "",
      transferSection: "",
      releaseType: "",
      menuPosition: 0,
      isActive: true
    };
  }

  resetForm() {
    if (this.PostingAndTrnsForm?.form != null) {
      this.PostingAndTrnsForm.form.reset();
      this.PostingAndTrnsForm.form.patchValue({
        postingOrderInfoId: 0,
        empId: 0,
        departmentId: 0,
        subBranchId: 0,
        subDepartmentId: 0,
        officeBranchId: 0,
        officeOrderNo: "",
        officeOrderDate: new Date(),
        orderOfficeBy: "",
        transferSection: "",
        releaseType: "",
        menuPosition: 0,
        isActive: true
      });
    }
    this.router.navigate(['/transfer/postingOrderInfo']);
  }

  getAllPostingOrderInfos() {
    this.subscription = this.postingOrderInfoService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubDepartmentNamesChangeByDepartmentId(departmentId: number) {
    this.subscription = this.subDepartmentService.getSubDepartmentByDepartmentId(departmentId).subscribe((data) => {
      this.subDepartments = data;
    });
  }

  onSubBranchNamesChangeByOfficeBranchId(officeBranchId: number) {
    this.subscription = this.subBranchService.getSubBranchByOfficeBranchId(officeBranchId).subscribe((data) => {
      this.subBranchs = data;
    });
  }

  onSubmit(form: any): void {
    // Clear cached data
    this.employeeService.cachedData = [];
    this.postingOrderInfoService.cachedData = [];
    this.transferApproveInfoService.cachedData = [];
    this.deptReleaseInfoService.cachedData = [];
    this.empTnsferPostingJoinService.cachedData = [];
    // Extract IDs from form value
    const id1 = form.value.empId;
    const id2 = form.value.postingOrderInfoId;
    const id3 = form.value.transferApproveInfoId;
    const id4 = form.value.depReleaseInfoId;
    const id5 = form.value.empTnsferPostingJoinId;
    // Submit or update Employee Transfer/Posting Joining
    const empTnsferPostingJoinId$ = id5
      ? this.empTnsferPostingJoinService.update(id5, form.value)
      : this.empTnsferPostingJoinService.submit(form.value);
      const depReleaseInfoId$ = id4
      ? this.deptReleaseInfoService.update(id1, form.value)
      : this.deptReleaseInfoService.submit(form.value);
      const transferApproveInfoId$ = id3
      ? this.transferApproveInfoService.update(id3, form.value)
      : this.transferApproveInfoService.submit(form.value);
      const postingOrderInfoId$ = id2
      ? this.postingOrderInfoService.update(id2, form.value)
      : this.postingOrderInfoService.submit(form.value);
      //console
      console.log(form.value)
    this.subscription = empTnsferPostingJoinId$.subscribe((response: any) => {
      this.subscription = depReleaseInfoId$.subscribe(response.depReleaseInfoId)
      this.subscription = transferApproveInfoId$.subscribe(response.transferApproveInfoId)
      this.subscription = postingOrderInfoId$.subscribe(response.postingOrderInfoId)
      if (response.success) {
        //console
        console.log(response);
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllPostingOrderInfos();
        this.resetForm();
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });

  } 
  
  // onSubmit(form: NgForm): void {
  //   this.employeeService.cachedData = [];
  //   this.postingOrderInfoService.cachedData = [];
  //   this.transferApproveInfoService.cachedData = [];
  //   this.deptReleaseInfoService.cachedData = [];
  //   this.empTnsferPostingJoinService.cachedData = [];

  //   const id1 = form.value.empId;
  //   const id2 = form.value.postingOrderInfoId;
  //   const id3 = form.value.transferApproveInfoId;
  //   const id4 = form.value.depReleaseInfoId;
  //   const id5 = form.value.empTnsferPostingJoinId;


  //   //EmployeeTransfer/Posting Joining
  //   const empTnsferPostingJoinId$ = id5
  //     ? this.empTnsferPostingJoinService.update(id5, form.value)
  //     : this.empTnsferPostingJoinService.submit(form.value);
  //   this.subscription = empTnsferPostingJoinId$.subscribe((response: any) => {
  //     console.log(response)
  //     if (response.success) {
  //       this.toastr.success('', `${response.message}`, {
  //         positionClass: 'toast-top-right',
  //       });
  //     }
  //   })
  //   //depReaseInfo
  //   const depReleaseInfoId$ = id4
  //     ? this.deptReleaseInfoService.update(id1, form.value)
  //     : this.deptReleaseInfoService.submit(form.value);
  //   this.subscription = depReleaseInfoId$.subscribe((response: any) => {
  //     console.log(response)
  //     if (response.success) {
  //       this.toastr.success('', `${response.message}`, {
  //         positionClass: 'toast-top-right',
  //       });
  //     }
  //   })

  //   //Transfer Approve Information
  //   const transferApproveInfoId$ = id3
  //     ? this.transferApproveInfoService.update(id3, form.value)
  //     : this.transferApproveInfoService.submit(form.value);
  //   //console.log(form.value)
  //   this.subscription = transferApproveInfoId$.subscribe((response: any) => {
  //     console.log(response)
  //     if (response.success) {
  //       this.toastr.success('', `${response.message}`, {
  //         positionClass: 'toast-top-right',
  //       });
  //     }
  //   })

  //   //Posting and Transfer
  //   const postingOrderInfoId$ = id2
  //     ? this.postingOrderInfoService.update(id2, form.value)
  //     : this.postingOrderInfoService.submit(form.value);
  //   // console.log(form.value)
  //   this.subscription = postingOrderInfoId$.subscribe((response: any) => {
  //     if (response.success) {
  //       console.log(response)
  //       //  const successMessage = id ? 'Update' : 'Successfully';
  //       this.toastr.success('', `${response.message}`, {
  //         positionClass: 'toast-top-right',
  //       });
  //       this.getAllPostingOrderInfos();
  //       this.resetForm();
  //       if (!id2) {
  //         // this.router.navigate(['/bascisetup/ward']);
  //       }
  //     } else {
  //       this.toastr.warning('', `${response.message}`, {
  //         positionClass: 'toast-top-right',
  //       });
  //     }
  //   });
  // }

  delete(element: any) {
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item').subscribe((result) => {
      if (result) {
        this.postingOrderInfoService.delete(element.postingOrderInfoId).subscribe(
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

  // transferApproveInfos

  initaialtransferApproveInfo(form?: NgForm) {
    if (form != null) form.resetForm();
    this.transferApproveInfoService.transferApproveInfos = {
      transferApproveInfoId: 0,
      empId: 0,
      approveStatus: "",
      approveBy: "",
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
        empId: 0,
        approveStatus: "",
        approveBy: "",
        approveDate: new Date(),
        remarks: "",
        menuPosition: 0,
        isActive: true
      });
    }
  }

  loadTransferApproveInfos() {
    this.subscription = this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((h) => {
      this.transferApproveInfos = h;
    });
  }

  //Departmental Release Information

  initaialDepartmentalReleaseInfo(form?: NgForm) {
    if (form != null) form.resetForm();
    this.deptReleaseInfoService.deptReleaseInfo = {
      depReleaseInfoId: 0,
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
    };
  }

  resetFormDepartmentalReleaseInfo() {
    this.btnText = 'Submit';
    if (this.DeptReleaseInfoForm?.form != null) {
      this.DeptReleaseInfoForm.form.reset();
      this.DeptReleaseInfoForm.form.patchValue({
        depReleaseInfoId: 0,
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

  //EmpTnsferPostingJoin
  initaialEmpTnsferPostingJoin(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empTnsferPostingJoinService.empTnsferPostingJoin = {
      empTnsferPostingJoinId: 0,
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

  bsModelRef!: BsModalRef;

  openModal(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
      this.handleEmployeeSelection(selectedEmployee);
    });
  }

  handleEmployeeSelection(employee: Employee) {
    this.employeeService.demoEmployee = employee;
  }
}

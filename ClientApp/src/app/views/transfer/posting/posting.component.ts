import { Employee } from './../../basic-setup/model/employees';
import { AfterViewInit, ChangeDetectorRef, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import {FormBuilder, NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import {Observable, Subscription, forkJoin, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmployeesService } from './../service/employees.service';
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
import { ApproveEmpModalComponent } from '../approve-emp-modal/approve-emp-modal.component';

@Component({
  selector: 'app-posting',
  templateUrl: './posting.component.html',
  styleUrls: ['./posting.component.scss']
})
export class PostingComponent implements OnInit, OnDestroy, AfterViewInit {
  bsModelRef!: BsModalRef;
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
  visible2: boolean | undefined;  
  visible3: boolean | undefined; 
  visible4: boolean | undefined; 
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
  // @Input()employeeApprove = new EventEmitter<Employee>();
  // @Input()ApprovedeptReleaseInfo = new EventEmitter<Employee>();

  constructor(
    public bsModalRef: BsModalRef,
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
      // 
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
  }

  GetEmployees(): void {
    this.employeeService.getEmployees().subscribe(res => {
      //console.log(res)
        this.employees = res;

      });
  }
  

  SelectDepartments() {
    this.departmentService.getSelectDepartments().subscribe((data) => {
      this.departments = data;
      //console.log(data)
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
  UserFormView2(): void {
    if (this.userBtnText == " Add Value") {
      this.userBtnText = " Hide Form";
      this.buttonIcon = "cilTrash";
      this.userHeaderText = "Add New Value";
      this.visible2 = true;
    } else {
      this.userBtnText = " Add Value";
      this.buttonIcon = "cilPencil";
      this.userHeaderText = "Value List";
      this.visible2 = false;
    }
  }
  UserFormView3(): void {
    if (this.userBtnText == " Add Value") {
      this.userBtnText = " Hide Form";
      this.buttonIcon = "cilTrash";
      this.userHeaderText = "Add New Value";
      this.visible3 = true;
    } else {
      this.userBtnText = " Add Value";
      this.buttonIcon = "cilPencil";
      this.userHeaderText = "Value List";
      this.visible3 = false;
    }
  }
  UserFormView4(): void {
    if (this.userBtnText == " Add Value") {
      this.userBtnText = " Hide Form";
      this.buttonIcon = "cilTrash";
      this.userHeaderText = "Add New Value";
      this.visible4 = true;
    } else {
      this.userBtnText = " Add Value";
      this.buttonIcon = "cilPencil";
      this.userHeaderText = "Value List";
      this.visible4 = false;
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


  // onSubmitAllForms(): void {
  //   // Call onSubmit for each form
  //   this.onSubmit(this.EmployeeForm);
  //   this.onSubmit(this.PostingAndTrnsForm);
  //   this.onSubmit(this.TransferApproveInfoForm);
  //   this.onSubmit(this.DeptReleaseInfoForm);
  //   this.onSubmit(this.EmpTransferPostingJoinForm);
  // }
  // //start
  // clearCachedData(): void {
  //   this.employeeService.cachedData = [];
  //   this.postingOrderInfoService.cachedData = [];
  //   this.transferApproveInfoService.cachedData = [];
  //   this.deptReleaseInfoService.cachedData = [];
  //   this.empTnsferPostingJoinService.cachedData = [];
  // }
  // handleResponse(response: any): void {
  //   if (response.success) {
  //     this.toastr.success('', `${response.message}`, {
  //       positionClass: 'toast-top-right',
  //     });
  //     this.getAllPostingOrderInfos();
  //     this.resetForm();
  //   } else {
  //     this.toastr.warning('', `${response.message}`, {
  //       positionClass: 'toast-top-right',
  //     });
  //   }
  // }
  // //valid 1
  // onSubmit(form: any): void {
  //   // Clear cached data
  //   this.clearCachedData();

  //   // Extract IDs from form value
  //   const id1 = form.value.empId;
  //   const id2 = form.value.postingOrderInfoId;
  //   const id3 = form.value.transferApproveInfoId;
  //   const id4 = form.value.depReleaseInfoId;
  //   const id5 = form.value.empTnsferPostingJoinId;

  //   // Submit or update based on the form
  //   if (form === this.EmployeeForm) {
  //     // Handle EmployeeForm submission
  //     const empTnsferPostingJoinId$ = id5
  //       ? this.empTnsferPostingJoinService.update(id5, form.value)
  //       : this.empTnsferPostingJoinService.submit(form.value);
  //     console.log(form.value)

  //     // Subscribe to the response and handle accordingly
  //     this.subscription = empTnsferPostingJoinId$.subscribe((response: any) => {
  //       if (response.success.empTnsferPostingJoinId) {
  //         this.handleResponse(response);
  //       } else {
  //         this.toastr.warning('', `${response.message}`, {
  //           positionClass: 'toast-top-right',
  //         });
  //       }
  //     });
  //   } else if (form === this.PostingAndTrnsForm) {
  //     // Handle PostingAndTrnsForm submission
  //     const postingOrderInfoId$ = id2
  //       ? this.postingOrderInfoService.update(id2, form.value)
  //       : this.postingOrderInfoService.submit(form.value);
  //     // No action needed, do not submit data
  //     this.subscription = postingOrderInfoId$.subscribe((response: any) => {
  //       if (response.success.postingOrderInfoId) {
  //         this.handleResponse(response);
  //       } else {
  //         this.toastr.warning('', `${response.message}`, {
  //           positionClass: 'toast-top-right',
  //         });
  //       }
  //     });
  //     console.log('PostingAndTrnsForm submitted, no action taken');
  //   } else if (form === this.TransferApproveInfoForm) {
  //     // Handle TransferApproveInfoForm submission
  //     const transferApproveInfoId$ = id3
  //       ? this.transferApproveInfoService.update(id3, form.value)
  //       : this.transferApproveInfoService.submit(form.value);

  //     // Subscribe to the response and handle accordingly
  //     this.subscription = transferApproveInfoId$.subscribe((response: any) => {
  //       if (response.success.transferApproveInfoId) {
  //         this.handleResponse(response);
  //       } else {
  //         this.toastr.warning('', `${response.message}`, {
  //           positionClass: 'toast-top-right',
  //         });
  //       }
  //     });
  //   } else if (form === this.EmpTransferPostingJoinForm) {
  //     // Handle EmpTransferPostingJoinForm submission
  //     const empTransferPostingJoinId$ = id5
  //       ? this.empTnsferPostingJoinService.update(id5, form.value)
  //       : this.empTnsferPostingJoinService.submit(form.value);

  //     // Subscribe to the response and handle accordingly
  //     this.subscription = empTransferPostingJoinId$.subscribe((response: any) => {
  //       if (response.success.empTnsferPostingJoinId) {
  //         this.handleResponse(response);
  //       } else {
  //         this.toastr.warning('', `${response.message}`, {
  //           positionClass: 'toast-top-right',
  //         });
  //       }
  //     });
  //   } else if (form === this.DeptReleaseInfoForm) {
  //     // Handle DeptReleaseInfoForm submission
  //     const deptReleaseInfoId$ = id4
  //       ? this.deptReleaseInfoService.update(id4, form.value)
  //       : this.deptReleaseInfoService.submit(form.value);

  //     // Subscribe to the response and handle accordingly
  //     this.subscription = deptReleaseInfoId$.subscribe((response: any) => {
  //       if (response.success.deptReleaseInfoId) {
  //         this.handleResponse(response);
  //       } else {
  //         this.toastr.warning('', `${response.message}`, {
  //           positionClass: 'toast-top-right',
  //         });
  //       }
  //     });
  //   }
  //   // Add similar conditions for other forms

  //   console.log('Submitting form:', form.value);
  // }





  onSubmitAllForms(): void {
    const formSubmissionObservables: Observable<any>[] = [];
  
    // Collect all form submission observables
    if (this.EmployeeForm.dirty) {
      formSubmissionObservables.push(this.onSubmit(this.EmployeeForm));
    }
    if (this.PostingAndTrnsForm.dirty) {
      formSubmissionObservables.push(this.onSubmit(this.PostingAndTrnsForm));
    }
    if (this.TransferApproveInfoForm.dirty) {
      formSubmissionObservables.push(this.onSubmit(this.TransferApproveInfoForm));
    }
    if (this.DeptReleaseInfoForm.dirty) {
      formSubmissionObservables.push(this.onSubmit(this.DeptReleaseInfoForm));
    }
    if (this.EmpTransferPostingJoinForm.dirty) {
      formSubmissionObservables.push(this.onSubmit(this.EmpTransferPostingJoinForm));
    }
  
    // Aggregate the responses and show a single toast message
    forkJoin(formSubmissionObservables).subscribe(
      (      responses: { success: any; }[]) => {
        const success = responses.some((response: { success: any; }) => response.success);
        if (success) {
          this.toastr.success('', 'Data saved successfully', {
            positionClass: 'toast-top-right',
          });
        } else {
          this.toastr.warning('', 'Failed to save data', {
            positionClass: 'toast-top-right',
          });
        }
      },
      error => {
        this.toastr.error('', 'Error occurred while saving data', {
          positionClass: 'toast-top-right',
        });
      }
    );
  }
  
  onSubmit(form: any): Observable<any> {
    // Clear cached data
    this.clearCachedData();
  
    // Extract IDs from form value
    const id1 = form.value.empId;
    const id2 = form.value.postingOrderInfoId;
    const id3 = form.value.transferApproveInfoId;
    const id4 = form.value.depReleaseInfoId;
    const id5 = form.value.empTnsferPostingJoinId;
  
    // Return the corresponding observable based on the form
    if (form === this.EmployeeForm) {
      return id5
        ? this.empTnsferPostingJoinService.update(id5, form.value)
        : this.empTnsferPostingJoinService.submit(form.value);
    } else if (form === this.PostingAndTrnsForm) {
      return id2
        ? this.postingOrderInfoService.update(id2, form.value)
        : this.postingOrderInfoService.submit(form.value);
    } else if (form === this.TransferApproveInfoForm) {
      return id3
        ? this.transferApproveInfoService.update(id3, form.value)
        : this.transferApproveInfoService.submit(form.value);
    } else if (form === this.EmpTransferPostingJoinForm) {
      return id5
        ? this.empTnsferPostingJoinService.update(id5, form.value)
        : this.empTnsferPostingJoinService.submit(form.value);
    } else if (form === this.DeptReleaseInfoForm) {
      return id4
        ? this.deptReleaseInfoService.update(id4, form.value)
        : this.deptReleaseInfoService.submit(form.value);
    } else {
      return of({ success: false, message: 'Invalid form ID' });
    }
  }
  
  clearCachedData(): void {
    this.employeeService.cachedData = [];
    this.postingOrderInfoService.cachedData = [];
    this.transferApproveInfoService.cachedData = [];
    this.deptReleaseInfoService.cachedData = [];
    this.empTnsferPostingJoinService.cachedData = [];
  }
  
  handleResponse(response: any): void {
    // This method is no longer needed, handling responses directly in the observable chain
  }
  
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
      PostingOrderInfoId:0,
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
        PostingOrderInfoId:0,
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
      transferApproveInfoId:0,
      empId: 0,
      approveByName:"",
      approveBy: 0,
      approveStatus:true,
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
        transferApproveInfoId:0,
        approveByName:"",
        approveBy: 0,
        approveStatus:true,
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
      depReleaseInfoId:0,
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
        depReleaseInfoId:0,
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

  //Employee
  selectEmployee(employee: Employee) {
    this.employeeSelected.emit(employee);
  }
  openModal(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
      this.handleEmployeeSelection(selectedEmployee);
    });
  }
  handleEmployeeSelection(employee: Employee) {
    this.employeeService.demoEmployee = employee;


  }


  //EmployeeJoin/Transfer
  openApproveEmpTransferJoin(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
      this.handleApproveEmpTransferJoin(selectedEmployee);
    });
  }
  handleApproveEmpTransferJoin(employee: Employee) {
    this.empTnsferPostingJoinService.empTnsferPostingJoin.approveBy= employee.empId,
    this.empTnsferPostingJoinService.empTnsferPostingJoin.approveByName= employee.employeeName
  }

  //DeptReleaseInfo
  openApprovDeptReleaseInfo(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
      this.handleApprovedeptReleaseInfo(selectedEmployee);
    });
  }
  handleApprovedeptReleaseInfo(employee: Employee) {
    this.deptReleaseInfoService.deptReleaseInfo.approveBy = employee.empId,
    this.deptReleaseInfoService.deptReleaseInfo.approveByName = employee.employeeName
  }
  //transferApproveInfos
  openApprovtransferApproveInfos(): void {
    const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
    modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
      this.handletransferApproveInfos(selectedEmployee);
    });
  }
  handletransferApproveInfos(employee: Employee) {
    this.transferApproveInfoService.transferApproveInfos.approveBy = employee.empId,
    this.transferApproveInfoService.transferApproveInfos.approveByName = employee.employeeName
  }
}

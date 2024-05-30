import { DesignationService } from './../../basic-setup/service/designation.service';
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
import { EmpTnsferPostingJoin } from './../../basic-setup/model/emp-tnsfer-posting-join';
import { EmpTnsferPostingJoinService } from './../../basic-setup/service/emp-tnsfer-posting-join.service';
import { OfficeService } from '../../basic-setup/service/office.service';

@Component({
  selector: 'app-posting',
  templateUrl: './posting.component.html',
  styleUrls: ['./posting.component.scss']
})
export class PostingComponent implements OnInit, OnDestroy, AfterViewInit {
  formattedDate!: string;
  bsModelRef!: BsModalRef;
  editMode: boolean = false;
  departments: SelectedModel[] = [];
  officeBranchs: SelectedModel[] = [];
  subBranchs: SelectedModel[] = [];
  transferApproveInfos: TransferApproveInfo[] = [];
  deptReleaseInfo: DeptReleaseInfo[] = [];
  empTnsferPostingJoin: EmpTnsferPostingJoin[] = [];
  employees: any[] = [];
  select: any[] = [];
  offices: SelectedModel[] = [];
  designations: SelectedModel[] = [];
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

  constructor(
    public designationService: DesignationService,
    public officeService : OfficeService,
    public bsModalRef: BsModalRef,
    public postingOrderInfoService: PostingOrderInfoService,
    private departmentService: DepartmentService,
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
    const id2 = params.get("postingOrderInfoId");
    const id3 = params.get("transferApproveInfoId");
    const id4 = params.get("depReleaseInfoId");
    const id5 = params.get("empTnsferPostingJoinId");

    // Posting Order Info
    if (id2) {
      this.visible3=false
      this.btnText = 'Update';
    
      this.postingOrderInfoService.find(+id2).subscribe((res) => {
        this.PostingAndTrnsForm?.form.patchValue(res);
      });
    } else {
      this.btnText = 'Submit';
    }

    // Transfer Approve Info
    if (id3) {
      this.btnText = 'Update';
      this.visible3=true
      this.transferApproveInfoService.find(+id3).subscribe((res) => {
        this.TransferApproveInfoForm?.form.patchValue(res);  
              
      });
    } else {
      this.btnText = 'Submit';
    }
    // Dept Release Info
    if (id4) {
      this.btnText = 'Update';
      this.deptReleaseInfoService.find(+id4).subscribe((res) => {
        this.DeptReleaseInfoForm?.form.patchValue(res);
      });
    } else {
      this.btnText = 'Submit';
    }

    // Emp Transfer Posting Join
    if (id5) {
      this.btnText = 'Update';
      this.empTnsferPostingJoinService.find(+id5).subscribe((res) => {
        this.EmpTransferPostingJoinForm?.form.patchValue(res);
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
    this.handleRouteParams();
    this.buttonIcon = "cilPencil";
    this.loadOffice();
    this.loadDepartment();
    this.loadDesignation();
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
          this.onOfficeSelect(res.officeId)
          this.onDepartmentSelect(res.departmentId)
          this.onDesignationSelect(res.designationId)
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
      this.visible = true;
    } else {
      this.userBtnText = " Add Value";
      this.visible = false;
    }
  }
  UserFormView2(): void {
    if (this.userBtnText == " Add Value") {
      this.userBtnText = " Hide Form";
      this.visible2 = true;
    } else {
      this.userBtnText = " Add Value";
      this.visible2 = false;
    }
  }
  UserFormView3(): void {
    if (this.userBtnText == " Add Value") {
      this.userBtnText = " Hide Form";
      this.visible3 = true;
    } else {
      this.userBtnText = " Add Value";
      this.visible3 = false;
    }
  }
  UserFormView4(): void {
    if (this.userBtnText == " Add Value") {
      this.userBtnText = " Hide Form";
      this.visible4 = true;
    } else {
      this.userBtnText = " Add Value";
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
      departmentId: null,
      subBranchId: 0,
      subDepartmentId: 0,
      designationId:null,
      officeId: null,
      designationName:"",
    officeName:"",
    departmentName:"",
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
        departmentId: null,
        subBranchId: 0,
        subDepartmentId: null,
        designationId:null,
      officeId: null,
      designationName:"",
    officeName:"",
    departmentName:"",
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
  loadOffice() { 
    this.subscription=this.officeService.selectGetoffice().subscribe((data) => { 
      this.offices = data;
    });
  }

  onOfficeSelect(officeId : number){
    this.departmentService.getDepartmentByOfficeId(+officeId).subscribe((res) => {
      this.departments = res;

    });
  }
  loadDepartment() { 
    this.subscription=this.departmentService.getSelectDepartments().subscribe((data) => { 
      this.departments = data;
    });
  }

  onDepartmentSelect(departmentId : number){
    this.designationService.getDesignationByDepartmentId(+departmentId).subscribe((res) => {
      this.designations = res;

    });
  }
  loadDesignation() { 
    this.subscription=this.designationService.selectDesignation().subscribe((data) => { 
      this.designations = data;
    });
  }
  onDesignationSelect(designationId:number){
    this.designationService.getDesignationByDepartmentId(+designationId).subscribe((res)=>{
      this.designations
    })
  }

///

onSubmitAllForms(): void {
  // Gather all form submission observables
  const submissions: Observable<any>[] = [
    this.onSubmit(this.EmployeeForm),
    this.onSubmit(this.PostingAndTrnsForm),
    this.onSubmit(this.TransferApproveInfoForm),
    this.onSubmit(this.DeptReleaseInfoForm),
    this.onSubmit(this.EmpTransferPostingJoinForm)    
  ];

  // Use forkJoin to wait for all form submissions
  forkJoin(submissions).subscribe(
    responses => {
      // Check if any form submission was successful
      const anySuccess = responses.some(response => response.success);

      // Show success or failure message based on the responses
      if (anySuccess) {
        this.toastr.success('', 'Data saved successfully', {
          positionClass: 'toast-top-right',
        });
      } else {
        this.toastr.warning('', 'Failed to save data', {
          positionClass: 'toast-top-right',
        });
      }
      this.getAllPostingOrderInfos();
      this.resetForm();
    },
    error => {
      this.toastr.error('', 'Error occurred during submission', {
        positionClass: 'toast-top-right',
      });
    }
  );
}



onSubmit(form: any): Observable<any> {
  this.clearCachedData();
  // Identify which form is being submitted and extract relevant IDs
  let id: string;
  let service: any;
if (form === this.PostingAndTrnsForm) {
    id = form.value.postingOrderInfoId;
    service = this.postingOrderInfoService;
    console.log(form.Value)
  } else if (form === this.TransferApproveInfoForm) {
    id = form.value.transferApproveInfoId;
    service = this.transferApproveInfoService;
  } else if (form === this.DeptReleaseInfoForm) {
    id = form.value.depReleaseInfoId;
    service = this.deptReleaseInfoService;
    console.log(form.value)
  } else if (form === this.EmpTransferPostingJoinForm) {
    id = form.value.empTnsferPostingJoinId;
    service = this.empTnsferPostingJoinService;
  } else {
    // Return an empty observable if no valid form is provided
    return new Observable(observer => {
      observer.next({ success: false, message: 'Invalid form ID' });
      observer.complete();
    });
  }

  // Submit or update based on the presence of the ID
  if (id) {

    return service.update(id, form.value);
    
  } else {
    console.log(id)
    return service.submit(form.value);
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
  if (response.success) {
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
    if (this.TransferApproveInfoForm?.form != null) {
      this.TransferApproveInfoForm.form.reset();
      this.TransferApproveInfoForm.form.patchValue({
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
      postingOrderInfoId:0,
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
        postingOrderInfoId:0,
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
      postingOrderInfoId:0,
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
        postingOrderInfoId:0,
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


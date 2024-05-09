import { Employee, EmployeesService } from './../service/employees.service';
import { EmpTnsferPostingJoin } from './../../basic-setup/model/emp-tnsfer-posting-join';
import { EmpTnsferPostingJoinService } from './../../basic-setup/service/emp-tnsfer-posting-join.service';
import { DeptReleaseInfoService } from './../../basic-setup/service/dept-release-info.service';
import { DeptReleaseInfo } from './../../basic-setup/model/dept-release-info';
import { TransferApproveInfoService } from './../../basic-setup/service/transfer-approve-info.service';
import { TransferApproveInfo } from './../../basic-setup/model/transfer-approve-info';
import { SubBranch } from './../../basic-setup/model/sub-branch';
import { DepartmentService } from './../../basic-setup/service/department.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { SubDepartmentService } from '../../basic-setup/service/sub-department.service';
import { BranchService } from '../../basic-setup/service/branch.service';
import { SubBranchService } from '../../basic-setup/service/sub-branch.service';
import { MatDialog } from '@angular/material/dialog';
import { EmpDemoComponent } from '../emp-demo/emp-demo.component';


@Component({
  selector: 'app-posting',
  templateUrl: './posting.component.html',
  styleUrl: './posting.component.scss'
})
export class PostingComponent implements OnInit, OnDestroy, AfterViewInit {
  //grades: any[] = [];
  editMode: boolean = false;
  departments: SelectedModel[] = [];
  subDepartments: SelectedModel[] = [];
  officeBranchs: SelectedModel[] = [];
  subBranchs: SelectedModel[] = [];
  transferApproveInfos: TransferApproveInfo[] = [];
  deptReleaseInfo: DeptReleaseInfo[] = [];
  empTnsferPostingJoin: EmpTnsferPostingJoin[] = [];
  employees: any[] = [];
  userBtnText: string | undefined;
  userHeaderText: string | undefined;
  buttonIcon: string = '';
  visible: boolean | undefined;

  btnText: string | undefined;
  loading = false;
  @ViewChild('PostingAndTrnsForm', { static: true }) PostingAndTrnsForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'officeOrderNo', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
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
  ) {
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(EmpDemoComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
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
    }
    else {
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

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.postingOrderInfoService.cachedData = [];
    const id = form.value.divisionId;
    const action$ = id
      ? this.postingOrderInfoService.update(id, form.value)
      : this.postingOrderInfoService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllPostingOrderInfos();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/transfer/postingOrderInfo']);
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

  initaialDepartmentalReleaseInfo
    (form?: NgForm) {
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
    if (this.PostingAndTrnsForm?.form != null) {

      this.PostingAndTrnsForm.form.reset();
      this.PostingAndTrnsForm.form.patchValue({
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
    if (this.PostingAndTrnsForm?.form != null) {

      this.PostingAndTrnsForm.form.reset();
      this.PostingAndTrnsForm.form.patchValue({
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

  //employees
  saveEmployeeData() {
    const newEmployee: Employee = {
      Id:0,
      firstName: '',
      lastName: '',
      email: ''
    };

    this.employeeService.saveEmployee(newEmployee).subscribe(response => {
      console.log('Employee data saved:', response);
    });
  }

  EmployeeSubmit(element: any) {
    this.employeeService
      .confirm('Employee List')
      .subscribe((result) => {
        if (result) {
          this.employeeService.saveEmployee(element.Id).subscribe(
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
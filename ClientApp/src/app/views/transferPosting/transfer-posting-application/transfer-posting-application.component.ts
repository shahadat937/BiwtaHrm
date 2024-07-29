import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { from, Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentService } from '../../basic-setup/service/department.service';
import { OfficeService } from '../../basic-setup/service/office.service';
import { EmpTransferPostingService } from '../service/emp-transfer-posting.service';
import { cilArrowLeft } from '@coreui/icons';
import { ActivatedRoute } from '@angular/router';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';

@Component({
  selector: 'app-transfer-posting-application',
  templateUrl: './transfer-posting-application.component.html',
  styleUrl: './transfer-posting-application.component.scss'
})
export class TransferPostingApplicationComponent implements OnInit, OnDestroy {

  id!: number;
  headerText: string = '';
  btnText: string = '';
  offices: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  releaseTypes: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  isValidEmp: boolean = false;
  isValidOrderByEmp: boolean = false;
  isApproveByEmp: boolean = false;
  loginEmpId: number = 0;
  @ViewChild('EmpTransferPostingForm', { static: true }) EmpTransferPostingForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
    public empJobDetailsService: EmpJobDetailsService,
    public officeService: OfficeService,
    public departmentService: DepartmentService,
    private route: ActivatedRoute,
  ) {

  }

  icons = { cilArrowLeft };

  ngOnInit(): void {
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId;
    this.getEmployeeByEmpId();
    this.loadOffice();
    this.getAllDepartment();
    this.getSelectedSection();
    this.getSelectedReleaseType();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getEmployeeByEmpId() {
    this.route.paramMap.subscribe((params) => {
      this.id = Number(params.get('id'));
    });
    this.subscription = this.empTransferPostingService.findByEmpId(this.id).subscribe((res) => {
      if (res) {
        console.log(res)
        this.EmpTransferPostingForm?.form.patchValue(res);
        this.headerText = 'Update Transfer and Posting Information';
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add New Transfer and Posting Order';
        this.btnText = 'Submit';
        this.initaialForm();
      }
    })
  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empTransferPostingService.empTransferPosting = {
      id: 0,
      empId: null,
      idCardNo: null,
      empName: null,
      departmentName: null,
      designationName: null,
      sectionName: null,
      orderByIdCardNo: null,
      orderByEmpName: null,
      orderByDepartmentName: null,
      orderByDesignationName: null,
      orderBySectionName: null,
      applicationById: null,
      currentOfficeId: null,
      currentDepartmentId: null,
      currentDesignationId: null,
      currentSectionId: null,
      officeOrderNo: null,
      officeOrderDate: null,
      orderOfficeById: null,
      releaseTypeId: null,
      transferOfficeId: null,
      transferDepartmentId: null,
      transferDesignationId: null,
      transferSectionId: null,
      isTransferApprove: true,
      provideTransferApproveInfo: false,
      transferApproveById: null,
      approveByIdCardNo: null,
      approveByEmpName: null,
      approveByDepartmentName: null,
      approveByDesignationName: null,
      approveBySectionName: null,
      transferApproveDate: null,
      approveRemark: null,
      transferApproveStatus: null,
      isDepartmentApprove: true,
      provideDepartmentApproveInfo: false,
      deptReleaseTypeId: null,
      deptReleaseById: null,
      deptReleaseDate: null,
      referenceNo: null,
      deptClearance: true,
      deptRemark: null,
      deptApproveStatus: null,
      isJoining: true,
      provideJoiningInfo: false,
      joiningReportingById: null,
      joiningDate: null,
      joiningRemark: null,
      joiningStatus: null,
      applicationStatus: null,
      remark: null,
      menuPosition: null,
      isActive: null,
    };
  }

  resetForm() {
    this.EmpTransferPostingForm.form.reset();
    this.EmpTransferPostingForm.form.patchValue({
      id: 0,
      empId: null,
      idCardNo: null,
      empName: null,
      departmentName: null,
      designationName: null,
      sectionName: null,
      orderByIdCardNo: null,
      orderByEmpName: null,
      orderByDepartmentName: null,
      orderByDesignationName: null,
      orderBySectionName: null,
      applicationById: null,
      currentOfficeId: null,
      currentDepartmentId: null,
      currentDesignationId: null,
      currentSectionId: null,
      officeOrderNo: null,
      officeOrderDate: null,
      orderOfficeById: null,
      releaseTypeId: null,
      transferOfficeId: null,
      transferDepartmentId: null,
      transferDesignationId: null,
      transferSectionId: null,
      isTransferApprove: true,
      provideTransferApproveInfo: false,
      transferApproveById: null,
      transferApproveDate: null,
      approveRemark: null,
      transferApproveStatus: null,
      isDepartmentApprove: true,
      provideDepartmentApproveInfo: false,
      deptReleaseTypeId: null,
      deptReleaseById: null,
      deptReleaseDate: null,
      referenceNo: null,
      deptClearance: true,
      deptRemark: null,
      deptApproveStatus: null,
      isJoining: true,
      provideJoiningInfo: false,
      joiningReportingById: null,
      joiningDate: null,
      joiningRemark: null,
      joiningStatus: null,
      applicationStatus: null,
      remark: null,
      menuPosition: null,
      isActive: null,
    });
  }

  getEmpInfoByIdCardNo(idCardNo: string) {
    this.subscription = this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
      if (res) {
        this.isValidEmp = true;
        this.empTransferPostingService.empTransferPosting.empName = res.firstName + " " + res.lastName;
        this.empTransferPostingService.empTransferPosting.empId = res.id;
        this.getEmpJobDetailsByEmpId(res.id);
      }
      else {
        this.isValidEmp = false;
        this.toastr.warning('', 'Invalid Employee PMS No', {
                positionClass: 'toast-top-right',
        });
      }
    })
  }

  getEmpJobDetailsByEmpId(id: number){
    this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empTransferPostingService.empTransferPosting.sectionName = res.sectionName;
        this.empTransferPostingService.empTransferPosting.departmentName = res.departmentName;
        this.empTransferPostingService.empTransferPosting.designationName = res.designationName;
        this.empTransferPostingService.empTransferPosting.currentDepartmentId = res.departmentId;
        this.empTransferPostingService.empTransferPosting.currentDesignationId = res.designationId;
        this.empTransferPostingService.empTransferPosting.currentSectionId = res.sectionId;
        this.empTransferPostingService.empTransferPosting.currentOfficeId = res.officeId;
      }
    })
  }

  getOrderByInfoByIdCardNo(idCardNo: string){
    this.subscription = this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
      if (res) {
        this.empTransferPostingService.empTransferPosting.applicationById = this.loginEmpId;
        this.isValidOrderByEmp = true;
        this.empTransferPostingService.empTransferPosting.orderByEmpName = res.firstName + " " + res.lastName;
        this.empTransferPostingService.empTransferPosting.orderOfficeById = res.id;
        this.getEmpJobDetailsByEmpIdOfOrderOfficeBy(res.id);
      }
      else {
        this.isValidOrderByEmp = false;
        this.toastr.warning('', 'Invalid Order By No', {
                positionClass: 'toast-top-right',
        });
      }
    })
  }

  getEmpJobDetailsByEmpIdOfOrderOfficeBy(id: number){
    this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empTransferPostingService.empTransferPosting.orderByDepartmentName = res.departmentName;
        this.empTransferPostingService.empTransferPosting.orderByDesignationName = res.designationName;
        this.empTransferPostingService.empTransferPosting.orderBySectionName = res.sectionName;
      }
    })
  }

  getApproveByInfoByIdCardNo(idCardNo: string){
    this.subscription = this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
      if (res) {
        this.isApproveByEmp = true;
        this.empTransferPostingService.empTransferPosting.approveByEmpName = res.firstName + " " + res.lastName;
        this.empTransferPostingService.empTransferPosting.transferApproveById = res.id;
        this.getEmpJobDetailsByEmpIdOfApproveBy(res.id);
      }
      else {
        this.isApproveByEmp = false;
        this.toastr.warning('', 'Invalid Approve By No', {
                positionClass: 'toast-top-right',
        });
      }
    })
  }

  getEmpJobDetailsByEmpIdOfApproveBy(id: number){
    this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empTransferPostingService.empTransferPosting.approveByDepartmentName = res.departmentName;
        this.empTransferPostingService.empTransferPosting.approveByDesignationName = res.designationName;
        this.empTransferPostingService.empTransferPosting.approveBySectionName = res.sectionName;
      }
    })
  }
  isTransferApproveNeed(status: boolean){
    if(!status){
      this.empTransferPostingService.empTransferPosting.provideTransferApproveInfo = false;
    }
    this.provideTransferApproveInfo(status);
  }
  provideTransferApproveInfo(status: boolean){
    if(!status){
      this.empTransferPostingService.empTransferPosting.approveByEmpName = '';
      this.empTransferPostingService.empTransferPosting.transferApproveById = null;
      this.empTransferPostingService.empTransferPosting.approveByDepartmentName = '';
      this.empTransferPostingService.empTransferPosting.approveByDesignationName = '';
      this.empTransferPostingService.empTransferPosting.approveBySectionName = '';
      this.empTransferPostingService.empTransferPosting.approveByIdCardNo = '';
      this.empTransferPostingService.empTransferPosting.transferApproveStatus = null;
      this.empTransferPostingService.empTransferPosting.transferApproveDate = null;
      this.empTransferPostingService.empTransferPosting.approveRemark = '';
      this.empTransferPostingService.empTransferPosting.deptReleaseById = null;
      this.isApproveByEmp = false;
    }
    else{
      this.empTransferPostingService.empTransferPosting.deptReleaseById = this.loginEmpId;
    }
  }
  
  isDeptApproveNeed(status: boolean){
    if(!status){
      this.empTransferPostingService.empTransferPosting.provideDepartmentApproveInfo = false;
    }
    this.provideDeptApproveInfo(status);
  }
  provideDeptApproveInfo(status: boolean){
    if(!status){
      this.empTransferPostingService.empTransferPosting.deptReleaseTypeId = null;
      this.empTransferPostingService.empTransferPosting.deptReleaseDate = null;
      this.empTransferPostingService.empTransferPosting.deptClearance = true;
      this.empTransferPostingService.empTransferPosting.referenceNo = '';
      this.empTransferPostingService.empTransferPosting.deptRemark = '';
      this.empTransferPostingService.empTransferPosting.joiningReportingById = null;
    }
    else {
      this.empTransferPostingService.empTransferPosting.joiningReportingById = this.loginEmpId;
    }
  }
  
  isJoiningApproveNeed(status: boolean){
    if(!status){
      this.empTransferPostingService.empTransferPosting.provideJoiningInfo = false;
      this.provideJoiningApproveInfo(false);
    }
  }
  provideJoiningApproveInfo(status: boolean){
    if(!status){
      this.empTransferPostingService.empTransferPosting.joiningDate = null;
      this.empTransferPostingService.empTransferPosting.joiningRemark = '';
    }
  }

  loadOffice() {
    this.subscription = this.officeService.selectGetoffice().subscribe((data) => {
      this.offices = data;
    });
  }
  
  getAllDepartment() {
    this.empJobDetailsService.getAllDepartment().subscribe((res) => {
      this.departments = res;
    });
  }
  getTransferDesignationByDepartment(departmentId: number | null ){
    this.designations = [];
    this.empTransferPostingService.empTransferPosting.transferDesignationId = null;
    this.empTransferPostingService.getDesignationByDepartment(departmentId).subscribe((res) => {
      this.designations = res;
    });
  }

  getSelectedSection(){
    this.subscription = this.empJobDetailsService.getSelectedSection().subscribe((data) => {
      this.sections = data;
    });
  }
  getSelectedReleaseType(){
    this.subscription = this.empTransferPostingService.getSelectedReleaseType().subscribe((data) => {
      this.releaseTypes = data;
    });
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.empTransferPostingService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.empTransferPostingService.updateEmpTransferPosting(id, form.value)
      : this.empTransferPostingService.saveEmpTransferPosting(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    });
  }
}

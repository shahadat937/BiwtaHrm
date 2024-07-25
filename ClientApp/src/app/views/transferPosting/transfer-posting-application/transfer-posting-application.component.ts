import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
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
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  isValidEmp: boolean = false;
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
    this.getEmployeeByEmpId();
    this.loadOffice();
    // this.getAllDepartment();
    // this.getSelectedSection();
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
      id: null,
      empId: null,

      idCardNo: null,
      empName: null,
      departmentName: null,
      designationName: null,
      sectionName: null,

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
      isTransferApprove: null,
      transferApproveById: null,
      transferApproveDate: null,
      approveRemark: null,
      transferApproveStatus: null,
      isDepartmentApprove: null,
      deptReleaseTypeId: null,
      deptReleaseById: null,
      deptReleaseDate: null,
      referenceNo: null,
      deptClearance: null,
      deptRemark: null,
      deptApproveStatus: null,
      isJoining: null,
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
      id: null,
      empId: null,

      idCardNo: null,
      empName: null,
      departmentName: null,
      designationName: null,
      sectionName: null,

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
      isTransferApprove: null,
      transferApproveById: null,
      transferApproveDate: null,
      approveRemark: null,
      transferApproveStatus: null,
      isDepartmentApprove: null,
      deptReleaseTypeId: null,
      deptReleaseById: null,
      deptReleaseDate: null,
      referenceNo: null,
      deptClearance: null,
      deptRemark: null,
      deptApproveStatus: null,
      isJoining: null,
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
      }
    })
  }

  loadOffice() {
    this.subscription = this.officeService.selectGetoffice().subscribe((data) => {
      this.offices = data;
    });
  }

  // getSelectedSection(){
  //   this.subscription = this.empTransferPostingService.getSelectedSection().subscribe((data) => {
  //     this.sections = data;
  //   });
  // }

  // onOfficeSelect(officeId: number) {
  //   this.empTransferPostingService.empTransferPosting.departmentId = null;
  //   this.departmentService.getSelectedDepartmentByOfficeId(+officeId).subscribe((res) => {
  //     this.departments = res;
  //   });
  // }
  // onOfficeSelectGetDesignation(officeId: number, empTransferPostingId: number) {
  //   this.empTransferPostingService.empTransferPosting.designationId = null;
  //   this.empTransferPostingService.getDesignationByOfficeId(officeId, empTransferPostingId).subscribe((res) => {
  //     this.designations = res;
  //   });
  // }

  // onDepartmentSelectGetDesignation(departmentId: number, empTransferPostingId: number) {
  //   if (departmentId == null) {
  //     this.onOfficeSelectGetDesignation(this.empTransferPostingService.empTransferPosting.officeId, this.empTransferPostingId);
  //   }
  //   this.empTransferPostingService.empTransferPosting.designationId = null;
  //   this.empTransferPostingService.getDesignationByDepartmentId(departmentId, empTransferPostingId).subscribe((res) => {
  //     this.designations = res;
  //   });
  // }

  // getAllDepartment() {
  //   this.empTransferPostingService.getAllDepartment().subscribe((res) => {
  //     this.firstDepartments = res;
  //   });
  // }
  // getOldDesignationByDepartment(departmentId: number) {
  //   this.empTransferPostingService.empTransferPosting.firstDesignationId = null;
  //   this.empTransferPostingService.getOldDesignationByDepartment(departmentId).subscribe((res) => {
  //     this.firstDesignations = res;
  //   });
  // }

  onSubmit(form: NgForm): void {
    // this.loading = true;
    // this.empTransferPostingService.cachedData = [];
    // const id = form.value.id;
    // const action$ = id
    //   ? this.empTransferPostingService.updateEmpTransferPosting(id, form.value)
    //   : this.empTransferPostingService.saveEmpTransferPosting(form.value);

    // this.subscription = action$.subscribe((response: any) => {
    //   if (response.success) {
    //     this.toastr.success('', `${response.message}`, {
    //       positionClass: 'toast-top-right',
    //     });
    //     this.loading = false;
    //   } else {
    //     this.toastr.warning('', `${response.message}`, {
    //       positionClass: 'toast-top-right',
    //     });
    //     this.loading = false;
    //   }
    //   this.loading = false;
    // });
  }
}

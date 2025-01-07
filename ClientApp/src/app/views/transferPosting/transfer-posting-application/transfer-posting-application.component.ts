import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { from, Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentService } from '../../basic-setup/service/department.service';
import { OfficeService } from '../../basic-setup/service/office.service';
import { EmpTransferPostingService } from '../service/emp-transfer-posting.service';
import { cilArrowLeft, cilSearch } from '@coreui/icons';
import { ActivatedRoute, Router } from '@angular/router';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';
import { EmpTransferPosting } from '../model/emp-transfer-posting';
import { SectionService } from '../../basic-setup/service/section.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { TransferPostingInfoComponent } from '../transfer-posting-info/transfer-posting-info.component';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { ReleaseTypeService } from '../../basic-setup/service/release-type.service';
import { GradeService } from '../../basic-setup/service/Grade.service';
import { UserNotification } from '../../notifications/models/user-notification';
import { NotificationService } from '../../notifications/service/notification.service';

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
  grades: SelectedModel[] = [];
  scales: SelectedModel[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;
  isValidEmp: boolean = false;
  isValidOrderByEmp: boolean = false;
  isApproveByEmp: boolean = false;
  loginEmpId: number = 0;
  empJobDetailsId: number = 0;
  empTransferPosting: EmpTransferPosting = new EmpTransferPosting;
  @ViewChild('EmpTransferPostingForm', { static: true }) EmpTransferPostingForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
    public empJobDetailsService: EmpJobDetailsService,
    public officeService: OfficeService,
    public departmentService: DepartmentService,
    private route: ActivatedRoute,
    private router: Router,
    public sectionService: SectionService,
    private modalService: BsModalService,
    public releaseTypeService: ReleaseTypeService,
    private gradeService: GradeService,
    public notificationService: NotificationService,
  ) {

  }

  icons = { cilArrowLeft, cilSearch };

  ngOnInit(): void {
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId;
    this.initaialForm();
    this.getEmployeeByEmpId();
    this.loadOffice();
    this.getAllDepartment();
    // this.getSelectedSection();
    this.getSelectedReleaseType();
    this.SelectModelGrade();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  getEmployeeByEmpId() {
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      this.id = Number(params.get('id'));
    })
    )
    this.subscription.push(
      this.empTransferPostingService.findById(this.id).subscribe((res) => {
      if (res) {
        this.empTransferPosting = res;
        if(res.transferSectionId){
          this.onSectionSelectGetDesignation(res.transferSectionId, this.empJobDetailsId);
        }
        else {
          this.getEmpJobDetailsInfo(res.empId, res.transferDepartmentId);
        }
        this.onOfficeAndDepartmentSelect(res.transferDepartmentId);
        // if(res.transferApproveStatus == true){
        //   this.empTransferPostingService.empTransferPosting.provideTransferApproveInfo = true;
        //   this.empTransferPostingService.empTransferPosting.transferApproveStatus = true;
        //   this.empTransferPostingService.empTransferPosting.transferApproveDate = res.transferApproveDate;
        //   this.empTransferPostingService.empTransferPosting.approveByIdCardNo = res.approveByIdCardNo;
        //   this.empTransferPostingService.empTransferPosting.remark = res.remark;
        //   this.getApproveByInfoByIdCardNo(res.approveByIdCardNo || '');
        // }
        if(res.withPromotion == true){
          this.getEmpJobDetailsByEmpId(res.empId || 0);
          this.empTransferPostingService.empTransferPosting.updateGradeId = res.updateGradeId;
          this.onChangeGradeGetScale(res.updateGradeId || 0);
          this.empTransferPostingService.empTransferPosting.updateScaleId = res.updateScaleId;
          this.empTransferPostingService.empTransferPosting.updateBasicPay = res.updateBasicPay;
        }
        if(res.deptApproveStatus == true && res.isDepartmentApprove == true){
          this.empTransferPostingService.empTransferPosting.provideDepartmentApproveInfo = true;
          this.empTransferPostingService.empTransferPosting.deptReleaseTypeId = res.deptReleaseTypeId;
          this.empTransferPostingService.empTransferPosting.deptReleaseDate = res.deptReleaseDate;
          this.empTransferPostingService.empTransferPosting.referenceNo = res.referenceNo;
          this.empTransferPostingService.empTransferPosting.deptClearance = res.deptClearance;
          this.empTransferPostingService.empTransferPosting.deptRemark = res.deptRemark;
        }
        if(res.joiningStatus == true && res.isJoining == true){
          this.empTransferPostingService.empTransferPosting.provideJoiningInfo = true;
          this.empTransferPostingService.empTransferPosting.joiningDate = res.joiningDate;
          this.empTransferPostingService.empTransferPosting.joiningRemark = res.joiningRemark;
        }
        this.EmpTransferPostingForm?.form.patchValue(res);
        this.headerText = 'Update Transfer and Posting Information';
        this.btnText = 'Update';
        if(res.empIdCardNo){
          this.isValidEmp = true;
          this.empTransferPostingService.empTransferPosting.empName = res.empName;
          this.empTransferPostingService.empTransferPosting.departmentName = res.departmentName;
          this.empTransferPostingService.empTransferPosting.designationName = res.designationName;
          this.empTransferPostingService.empTransferPosting.sectionName = res.sectionName;
          this.empTransferPostingService.empTransferPosting.currentDeptJoinDate = res.currentDeptJoinDate;
        }
        // if(res.orderByIdCardNo){
        //   this.getOrderByInfoByIdCardNo(res.orderByIdCardNo);
        // }
      }
      else {
        this.headerText = 'Add New Transfer and Posting Order';
        this.btnText = 'Submit';
      }
    })
    )
    
  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empTransferPostingService.empTransferPosting = {
      id: 0,
      empId: null,
      empIdCardNo: null,
      empName: null,
      departmentName: null,
      designationName: null,
      sectionName: null,
      orderByOffice: '',
      applicationById: this.loginEmpId,
      currentOfficeId: null,
      currentDeptJoinDate: null,
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
      isTransferApprove: false,
      provideTransferApproveInfo: false,
      transferApproveById: null,
      approveByIdCardNo: null,
      approveByEmpName: null,
      approveByDepartmentName: null,
      approveByDesignationName: null,
      approveBySectionName: null,
      deptReleaseByIdCardNo: null,
      deptReleaseByEmpName: null,
      deptReleaseByDepartmentName: null,
      deptReleaseByDesignationName: null,
      deptReleaseBySectionName: null,
      joiningReportingByIdCardNo: null,
      joiningReportingByEmpName: null,
      joiningReportingByDepartmentName: null,
      joiningReportingByDesignationName: null,
      joiningReportingBySectionName: null,
      releaseTypeName: null,
      deptReleaseTypeName: null,
      transferDepartmentName:  null,
      transferDesignationName:  null,
      transferSectionName:  null,
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
      
      withPromotion: false,
      currentGradeId: null,
      currentScaleId: null,
      currentBasicPay: null,
      updateGradeId: null,
      updateScaleId: null,
      updateBasicPay: null,

      currentGradeName: '',
      currentScaleName: '',
      updateGradeName: '',
      updateScaleName: '',
    };
  }

  resetForm() {
    this.EmpTransferPostingForm.form.reset();
    this.EmpTransferPostingForm.form.patchValue({
      id: 0,
      empId: null,
      empIdCardNo: null,
      empName: null,
      departmentName: null,
      designationName: null,
      sectionName: null,
      orderByOffice: '',
      applicationById: this.loginEmpId,
      currentOfficeId: null,
      currentDeptJoinDate: null,
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
      isTransferApprove: false,
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
      
      currentGradeId: null,
      currentScaleId: null,
      currentBasicPay: null,
      updateGradeId: null,
      updateScaleId: null,
      updateBasicPay: null,

      currentGradeName: '',
      currentScaleName: '',
      updateGradeName: '',
      updateScaleName: '',
    });
  }

  getEmpInfoByIdCardNo(idCardNo: string) {
    if(idCardNo){
      this.subscription.push(
        this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
        if (res) {
          this.subscription.push(
          this.empTransferPostingService.findByEmpId(res.id).subscribe((response) => {
            if(response){
              this.isValidEmp = false;
              this.toastr.warning('', 'Employee have pending Application', {
                      positionClass: 'toast-top-right',
              });
            }
            else {
              this.isValidEmp = true;
              this.empTransferPostingService.empTransferPosting.empName = res.firstName + " " + res.lastName;
              this.empTransferPostingService.empTransferPosting.empId = res.id;
              this.getEmpJobDetailsByEmpId(res.id);
              this.subscription.push(
              this.empTransferPostingService.CurrentDeptJoinDateByEmpId(res.id).subscribe((res:any) => {
                if(res){
                  this.empTransferPostingService.empTransferPosting.currentDeptJoinDate = res;
                }
              })
              )
              
            }
          })
          )
          
        }
        else {
          this.isValidEmp = false;
          this.toastr.warning('', 'Invalid Employee PMS No', {
                  positionClass: 'toast-top-right',
          });
        }
      })
      )
    
    }
  }

  getEmpJobDetailsByEmpId(id: number){
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empJobDetailsId = res.id;
        this.empTransferPostingService.empTransferPosting.sectionName = res.sectionName;
        this.empTransferPostingService.empTransferPosting.departmentName = res.departmentName;
        this.empTransferPostingService.empTransferPosting.designationName = res.designationName;
        this.empTransferPostingService.empTransferPosting.currentDepartmentId = res.departmentId;
        this.empTransferPostingService.empTransferPosting.currentDesignationId = res.designationId;
        this.empTransferPostingService.empTransferPosting.currentSectionId = res.sectionId;
        this.empTransferPostingService.empTransferPosting.currentOfficeId = res.officeId;
        this.empTransferPostingService.empTransferPosting.currentGradeId = res.presentGradeId;
        this.empTransferPostingService.empTransferPosting.currentScaleId = res.presentScaleId;
        this.empTransferPostingService.empTransferPosting.currentBasicPay = res.basicPay;
        this.empTransferPostingService.empTransferPosting.currentGradeName = res.presentGradeName;
        this.empTransferPostingService.empTransferPosting.currentScaleName = res.presentScaleName;
      }
    })
    )
    
  }

  getEmpJobDetailsInfo(id: number | null, departmentId : number | null){
   this.subscription.push(
    this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empJobDetailsId = res.id;
        this.getTransferDesignationByDepartment(departmentId, res.id);
      }
    })
   )
    
    
  }

  gerReleaseTypeInfo(id: number){
    this.subscription.push(
    this.releaseTypeService.getById(id).subscribe((res) => {
      if(res.isDeptRelease == false){
        this.empTransferPostingService.empTransferPosting.isDepartmentApprove = false;
        this.isDeptApproveNeed(false);
      }
      else {
        this.empTransferPostingService.empTransferPosting.isDepartmentApprove = true;
        this.isDeptApproveNeed(true);
      }
      this.empTransferPostingService.empTransferPosting.deptReleaseTypeName = res.releaseTypeName;
      this.empTransferPostingService.empTransferPosting.deptReleaseTypeId = id;
    })
    )
    
  }

  // getOrderByInfoByIdCardNo(idCardNo: string){
  //   this.subscription = this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
  //     if (res) {
  //       this.isValidOrderByEmp = true;
  //       this.empTransferPostingService.empTransferPosting.applicationById = this.loginEmpId;
  //       this.empTransferPostingService.empTransferPosting.orderByEmpName = res.firstName + " " + res.lastName;
  //       this.empTransferPostingService.empTransferPosting.orderOfficeById = res.id;
  //       this.getEmpJobDetailsByEmpIdOfOrderOfficeBy(res.id);
  //     }
  //     else {
  //       this.isValidOrderByEmp = false;
  //       this.toastr.warning('', 'Invalid Order By No', {
  //               positionClass: 'toast-top-right',
  //       });
  //     }
  //   })
  // }

  // getEmpJobDetailsByEmpIdOfOrderOfficeBy(id: number){
  //   this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
  //     if(res){
  //       this.empTransferPostingService.empTransferPosting.orderByDepartmentName = res.departmentName;
  //       this.empTransferPostingService.empTransferPosting.orderByDesignationName = res.designationName;
  //       this.empTransferPostingService.empTransferPosting.orderBySectionName = res.sectionName;
  //     }
  //   })
  // }

  getApproveByInfoByIdCardNo(idCardNo: string){
  
    this.subscription.push(
    this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
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
    )
    
  }

  getEmpJobDetailsByEmpIdOfApproveBy(id: number){
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empTransferPostingService.empTransferPosting.approveByDepartmentName = res.departmentName;
        this.empTransferPostingService.empTransferPosting.approveByDesignationName = res.designationName;
        this.empTransferPostingService.empTransferPosting.approveBySectionName = res.sectionName;
      }
    })
    )
    
  }
  // isTransferApproveNeed(status: boolean){
  //   if(!status){
  //     this.empTransferPostingService.empTransferPosting.provideTransferApproveInfo = false;
  //   }
  //   this.provideTransferApproveInfo(status);
  // }
  // provideTransferApproveInfo(status: boolean){
  //   if(!status){
  //     this.empTransferPostingService.empTransferPosting.approveByEmpName = '';
  //     this.empTransferPostingService.empTransferPosting.transferApproveById = null;
  //     this.empTransferPostingService.empTransferPosting.approveByDepartmentName = '';
  //     this.empTransferPostingService.empTransferPosting.approveByDesignationName = '';
  //     this.empTransferPostingService.empTransferPosting.approveBySectionName = '';
  //     this.empTransferPostingService.empTransferPosting.approveByIdCardNo = '';
  //     this.empTransferPostingService.empTransferPosting.transferApproveStatus = null;
  //     this.empTransferPostingService.empTransferPosting.transferApproveDate = null;
  //     this.empTransferPostingService.empTransferPosting.approveRemark = '';
  //     this.isApproveByEmp = false;
  //   }
  //   else{
  //     this.empTransferPostingService.empTransferPosting.transferApproveById = this.loginEmpId;
  //     if(this.empTransferPosting.transferApproveById){
  //       this.empTransferPostingService.empTransferPosting.approveByEmpName = this.empTransferPosting.approveByEmpName;
  //       this.empTransferPostingService.empTransferPosting.transferApproveById = this.empTransferPosting.transferApproveById;
  //       this.empTransferPostingService.empTransferPosting.approveByIdCardNo = this.empTransferPosting.approveByIdCardNo;
  //       this.empTransferPostingService.empTransferPosting.transferApproveStatus = this.empTransferPosting.transferApproveStatus;
  //       this.empTransferPostingService.empTransferPosting.transferApproveDate = null;
  //       this.empTransferPostingService.empTransferPosting.approveRemark = this.empTransferPosting.approveRemark;
  //       this.isApproveByEmp = true;
  //       this.getApproveByInfoByIdCardNo(this.empTransferPosting.approveByIdCardNo || '');
  //     }
  //   }
  // }

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
      this.empTransferPostingService.empTransferPosting.deptReleaseById = null;
    }
    else {
      this.empTransferPostingService.empTransferPosting.deptReleaseById = this.loginEmpId;
      if(this.empTransferPosting.deptReleaseById){
        this.empTransferPostingService.empTransferPosting.deptReleaseTypeId = this.empTransferPosting.deptReleaseTypeId;
        this.empTransferPostingService.empTransferPosting.deptReleaseDate = this.empTransferPosting.deptReleaseDate;
        this.empTransferPostingService.empTransferPosting.deptClearance = this.empTransferPosting.deptClearance;
        this.empTransferPostingService.empTransferPosting.referenceNo = this.empTransferPosting.referenceNo;
        this.empTransferPostingService.empTransferPosting.deptRemark = this.empTransferPosting.deptRemark;
        this.empTransferPostingService.empTransferPosting.deptReleaseById = this.empTransferPosting.deptReleaseById;
      }
    }
  }

  isJoiningApproveNeed(status: boolean){
    if(!status){
      this.empTransferPostingService.empTransferPosting.provideJoiningInfo = false;
    }
    this.provideJoiningApproveInfo(status);
  }
  provideJoiningApproveInfo(status: boolean){
    if(!status){
      this.empTransferPostingService.empTransferPosting.joiningDate = null;
      this.empTransferPostingService.empTransferPosting.joiningRemark = '';
      this.empTransferPostingService.empTransferPosting.joiningReportingById = null;
    }
    else {
      this.empTransferPostingService.empTransferPosting.joiningReportingById = this.loginEmpId;
      if(this.empTransferPosting.joiningReportingById){
        this.empTransferPostingService.empTransferPosting.joiningDate = this.empTransferPosting.joiningDate;
        this.empTransferPostingService.empTransferPosting.joiningRemark = this.empTransferPosting.joiningRemark;
        this.empTransferPostingService.empTransferPosting.joiningReportingById = this.empTransferPosting.joiningReportingById;
      }
    }
  }

  EmployeeListModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl'  });

    modalRef.content.employeeSelected.subscribe((idCardNo: string) => {
      if(idCardNo){
        this.getEmpInfoByIdCardNo(idCardNo);
        this.empTransferPostingService.empTransferPosting.empIdCardNo = idCardNo;
      }
    });
  }
  withPromotion(){
    this.getEmpJobDetailsByEmpId(this.empTransferPostingService.empTransferPosting.empId || 0);
  }

  loadOffice() {
    this.subscription.push(
    this.officeService.selectGetoffice().subscribe((data) => {
      this.offices = data;
    })
    )
    
    
  }

  getAllDepartment() {
    this.subscription.push(
    this.empJobDetailsService.getAllDepartment().subscribe((res) => {
      this.departments = res;
    })
    )
    
  }
  
  getTransferDesignationByDepartment(departmentId: number | null, empJobDetailsId: number) {
    this.designations = [];
    this.empTransferPostingService.empTransferPosting.transferDesignationId = null;
    this.subscription.push(
    this.empJobDetailsService.getDesignationByDepartmentId(departmentId, empJobDetailsId).subscribe((res) => {
      this.designations = res;
    })
    )
    
    this.empTransferPostingService.empTransferPosting.transferDesignationId = this.empTransferPosting.transferDesignationId;
  }
  
  getTransferDesignationByDepartmentOnChange(departmentId: number | null, empJobDetailsId: number) {
    this.designations = [];
    this.empTransferPostingService.empTransferPosting.transferDesignationId = null;
    this.subscription.push(
    this.empJobDetailsService.getDesignationByDepartmentId(departmentId, empJobDetailsId).subscribe((res) => {
      this.designations = res;
    })
    )
    
  }

  getSelectedSection(){
    this.subscription.push(
    this.empJobDetailsService.getSelectedSection().subscribe((data) => {
      this.sections = data;
    })
    )
    
  }
  getSelectedReleaseType(){
    this.subscription.push(
    this.empTransferPostingService.getSelectedReleaseType().subscribe((data) => {
      this.releaseTypes = data;
    })
    )
    
  }

  
  onOfficeAndDepartmentSelect(departmentId : any){
    this.empTransferPostingService.empTransferPosting.transferSectionId = null;
    this.subscription.push(
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.sections = res;
    })
    )
    
  }
  
  onSectionSelectGetDesignation(sectionId: any, empJobDetailsId: number) {
    this.designations = [];
    this.empTransferPostingService.empTransferPosting.transferDesignationId = null;
    if (sectionId == null) {
      this.getTransferDesignationByDepartmentOnChange(this.empTransferPostingService.empTransferPosting.transferDepartmentId, this.empJobDetailsId);
    }
    else {
      this.subscription.push(
      this.empJobDetailsService.getDesignationBySectionId(+sectionId, +empJobDetailsId).subscribe((res) => {
      this.designations = res;
    })
      )
      
    }
  }

  SelectModelGrade() {
    this.subscription.push(
   this.gradeService.selectModelGrade().subscribe((data) => {
      this.grades = data;
    })
    )
   
  }

  onChangeGradeGetScale(gradeId: number) {
    this.empJobDetailsService.empJobDetails.presentScaleId = null;
    this.subscription.push(
      this.empJobDetailsService.getScaleByGradeId(+gradeId).subscribe((res) => {
      this.scales = res;
    })
    )
    
  }
  onChangeScaleGetBasicPay(scaleId: number) {
    this.subscription.push(
      this.empJobDetailsService.getBasicPayByScale(scaleId).subscribe((res) => {
      this.empTransferPostingService.empTransferPosting.updateBasicPay = res.basicPay;
    })
    )
    
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.empTransferPostingService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.empTransferPostingService.updateEmpTransferPosting(id, form.value)
      : this.empTransferPostingService.saveEmpTransferPosting(form.value);

      this.subscription.push(
      action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });

        // Notification Start
        if(id == 0) {
          if(this.empTransferPostingService.empTransferPosting.isDepartmentApprove && !this.empTransferPostingService.empTransferPosting.provideDepartmentApproveInfo){
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empTransferPostingService.empTransferPosting.empId
            userNotification.toDeptId = this.empTransferPostingService.empTransferPosting.currentDepartmentId;
            userNotification.featurePath = 'departmentApprovalList';
            userNotification.nevigateLink = '/transferPosting/departmentApprovalList';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Transfer and Posting';
            userNotification.message = 'new Transfer & and Posting application, Department Release Pending.';
            this.notificationService.submit(userNotification).subscribe((res) => {});
          }
          else if(this.empTransferPostingService.empTransferPosting.isJoining && !this.empTransferPostingService.empTransferPosting.provideJoiningInfo){
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empTransferPostingService.empTransferPosting.empId
            userNotification.toDeptId = this.empTransferPostingService.empTransferPosting.transferDepartmentId;
            userNotification.featurePath = 'joiningReportingList';
            userNotification.nevigateLink = '/transferPosting/joiningReportingList';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Transfer and Posting';
            userNotification.message = ' Transfer & and Posting application, Joining Info Pending.';
            this.notificationService.submit(userNotification).subscribe((res) => {});
          }
        }
        // Notification End

        this.loading = false;
        this.router.navigate(['/transferPosting/transferPostingList']);
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    })
      )
     
  }
}

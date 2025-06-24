import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilSearch } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentService } from '../../basic-setup/service/department.service';
import { OfficeService } from '../../basic-setup/service/office.service';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';
import { EmpPromotionIncrement } from '../model/emp-promotion-increment';
import { EmpPromotionIncrementService } from '../service/emp-promotion-increment.service';
import { EmpTransferPostingService } from '../../transferPosting/service/emp-transfer-posting.service';
import { GradeService } from '../../basic-setup/service/Grade.service';
import { EmpRewardPunishmentService } from '../../employee/service/emp-reward-punishment.service';
import { EmpRewardPunishment } from '../../employee/model/emp-reward-punishment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { AuthService } from '../../../core/service/auth.service';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { NotificationService } from '../../notifications/service/notification.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { UserNotification } from '../../notifications/models/user-notification';

@Component({
  selector: 'app-increment-and-promotion',
  templateUrl: './increment-and-promotion.component.html',
  styleUrl: './increment-and-promotion.component.scss'
})
export class IncrementAndPromotionComponent  implements OnInit, OnDestroy {

  id!: number;
  headerText: string = '';
  btnText: string = '';
  offices: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  grades: SelectedModel[] = [];
  scales: SelectedModel[] = [];
  empRewardPunishments: EmpRewardPunishment[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;
  isValidEmp: boolean = false;
  isValidOrderByEmp: boolean = false;
  isApproveByEmp: boolean = false;
  loginEmpId: number = 0;
  empJobDetailsId: number = 0;
  empPromotionIncrement: EmpPromotionIncrement = new EmpPromotionIncrement;
  @ViewChild('EmpPromotionIncrementForm', { static: true }) EmpPromotionIncrementForm!: NgForm;
  featurePermission : FeaturePermission = new FeaturePermission;

  constructor(
    private toastr: ToastrService,
    public empPromotionIncrementService: EmpPromotionIncrementService,
    public empTransferPostingService: EmpTransferPostingService,
    public empJobDetailsService: EmpJobDetailsService,
    private gradeService: GradeService,
    public officeService: OfficeService,
    public departmentService: DepartmentService,
    private route: ActivatedRoute,
    private router: Router,
    public empRewardPunishmentService: EmpRewardPunishmentService,
    private modalService: BsModalService,
    public notificationService: NotificationService,
    private authService: AuthService,
    public roleFeatureService: RoleFeatureService,
  ) {

  }

  icons = { cilArrowLeft, cilSearch };

  ngOnInit(): void {
    if(this.authService.userInformation != null){
      this.getPermission();
    }
    else {
      this.toastr.warning('', 'Please Login First', {
        positionClass: 'toast-top-right',
      });
    }
  }

  
  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('transferPostingApplication').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId;;
        this.initaialForm();
        this.SelectedModelGrade();
        this.getEmployeeByEmpId();
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  
  EmployeeListModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl'  });

    modalRef.content.employeeSelected.subscribe((idCardNo: string) => {
      if(idCardNo){
        this.getEmpInfoByIdCardNo(idCardNo);
        this.empPromotionIncrementService.empPromotionIncrement.empIdCardNo = idCardNo;
      }
    });
  }

  getEmployeeByEmpId() {
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      this.id = Number(params.get('id'));
    })
    )
    this.subscription.push(
       this.empPromotionIncrementService.findById(this.id).subscribe((res) => {
      if (res) {
        this.empPromotionIncrement = res;
        this.patchEmpInfo();
        this.getEmpRewardPunishmentByEmpId(res.empId || 0);
        this.getEmpJobDetailsByEmpId(res.empId || 0);
        this.onChangeGradeGetScale(res.updateGradeId || 0);
        if(res.approveDate){
          this.empPromotionIncrementService.empPromotionIncrement.provideApprovalInfo = true;
          this.pathApproveStatusInfo();
        }
        this.EmpPromotionIncrementForm?.form.patchValue(res);
        this.headerText = 'Update Promotion and Increment Information';
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add New Promotion and Increment Order';
        this.btnText = 'Submit';
      }
    })
    )
   
  }


  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empPromotionIncrementService.empPromotionIncrement = {
      id : 0,
      empId : null,
      empName : null,
      empIdCardNo : null,
      currentDepartmentId : null,
      currentDeptJoinDate : null,
      currentDesignationId : null,
      currentGradeId : null,
      currentSectionId : null,
      currentScaleId : null,
      currentDepartmentName : null,
      currentDesignationName : null,
      currentSectionName : null,
      currentGradeName : null,
      currentScaleName : null,
      currentBasicPay : null,
      updateDesignationId : null,
      updateGradeId : null,
      updateScaleId : null,
      updateDesignationName : null,
      updateGradeName : null,
      updateScaleName : null,
      updateBasicPay : null,
      promotionIncrementType : 'Increment & Promotion',
      orderById : null,
      orderDate : null,
      orderNo: "",
      effectiveDate : null,
      applicationById : this.loginEmpId,
      isApproval : true,
      provideApprovalInfo: false,
      approveByIdCardNo: null,
      approveById : null,
      approveByName : null,
      approveByDepartmentName : null,
      approveByDesignationName : null,
      approveDate : null,
      approveStatus : null,
      approveRemark : null,
      applicationStatus : null,
      remark : null,
      menuPosition : null,
      isActive : true,
    };
  }

  resetForm() {
    this.EmpPromotionIncrementForm.form.reset();
    this.EmpPromotionIncrementForm.form.patchValue({
      id : 0,
      empId : null,
      empName : null,
      empIdCardNo : null,
      currentDepartmentId : null,
      currentDeptJoinDate : null,
      currentSectionId : null,
      currentDesignationId : null,
      currentGradeId : null,
      currentScaleId : null,
      currentDepartmentName : null,
      currentDesignationName : null,
      currentSectionName : null,
      currentGradeName : null,
      currentScaleName : null,
      currentBasicPay : null,
      updateDesignationId : null,
      updateGradeId : null,
      updateScaleId : null,
      updateDesignationName : null,
      updateGradeName : null,
      updateScaleName : null,
      updateBasicPay : null,
      promotionIncrementType : 'Increment & Promotion',
      orderById : null,
      orderDate : null,
      orderNo: "",
      effectiveDate : null,
      applicationById : this.loginEmpId,
      isApproval : true,
      provideApprovalInfo: false,
      approveByIdCardNo: null,
      approveById : null,
      approveByName : null,
      approveByDepartmentName : null,
      approveByDesignationName : null,
      approveDate : null,
      approveStatus : null,
      approveRemark : null,
      applicationStatus : null,
      remark : null,
      menuPosition : null,
      isActive : true,
    });
  }

  patchEmpInfo(){
    this.isValidEmp = true;
    this.empPromotionIncrementService.empPromotionIncrement.empName = this.empPromotionIncrement.empName;
    this.empPromotionIncrementService.empPromotionIncrement.currentSectionName = this.empPromotionIncrement.currentSectionName;
    this.empPromotionIncrementService.empPromotionIncrement.currentDepartmentName = this.empPromotionIncrement.currentDepartmentName;
    this.empPromotionIncrementService.empPromotionIncrement.currentDesignationName = this.empPromotionIncrement.currentDesignationName;
    this.empPromotionIncrementService.empPromotionIncrement.currentDeptJoinDate = this.empPromotionIncrement.currentDeptJoinDate;
  }
  pathApproveStatusInfo(){
    this.empPromotionIncrementService.empPromotionIncrement.approveById = this.empPromotionIncrement.approveById;
    this.empPromotionIncrementService.empPromotionIncrement.approveStatus = this.empPromotionIncrement.approveStatus;
    this.empPromotionIncrementService.empPromotionIncrement.approveDate = this.empPromotionIncrement.approveDate;
    this.empPromotionIncrementService.empPromotionIncrement.approveRemark = this.empPromotionIncrement.approveRemark;
  }
  
  getEmpInfoByIdCardNo(idCardNo: string) {
    this.subscription.push(
      this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
      if (res) {
       this.subscription.push(
      this.empPromotionIncrementService.findByEmpId(res.id).subscribe((response) => {
          if(response){
            this.isValidEmp = false;
            this.toastr.warning('', 'Employee have pending Application', {
                    positionClass: 'toast-top-right',
            });
          }
          else {
            this.isValidEmp = true;
            this.empPromotionIncrementService.empPromotionIncrement.empName = res.firstName + " " + res.lastName;
            this.empPromotionIncrementService.empPromotionIncrement.empId = res.id;
            this.getEmpJobDetailsNewByEmpId(res.id);
            this.getEmpRewardPunishmentByEmpId(res.id);
            // this.subscription.push(
            //   this.empTransferPostingService.CurrentDeptJoinDateByEmpId(res.id).subscribe((res:any) => {
            //   this.empPromotionIncrementService.empPromotionIncrement.currentDeptJoinDate = res;
            // })
            // )
            
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

  getEmpRewardPunishmentByEmpId(id: number){
    // this.subscription = 
    this.subscription.push(
      this.empRewardPunishmentService.findByEmpId(+id).subscribe((res) => {
      this.empRewardPunishments = res;
    })
    )
    
  }

  getEmpJobDetailsByEmpId(id: number){
    // this.subscription = 
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empJobDetailsId = res.id;
        this.getDesignationByDepartmentId(res.departmentId, res.id);
        if(!this.empPromotionIncrement.id){
          this.empPromotionIncrementService.empPromotionIncrement.currentSectionName = res.sectionName;
          this.empPromotionIncrementService.empPromotionIncrement.currentDepartmentName = res.departmentName;
          this.empPromotionIncrementService.empPromotionIncrement.currentDesignationName = res.designationName;
          this.empPromotionIncrementService.empPromotionIncrement.currentDepartmentId = res.departmentId;
          this.empPromotionIncrementService.empPromotionIncrement.currentSectionId = res.sectionId;
          this.empPromotionIncrementService.empPromotionIncrement.currentDesignationId = res.designationId;
          this.empPromotionIncrementService.empPromotionIncrement.currentGradeId = res.presentGradeId;
          this.empPromotionIncrementService.empPromotionIncrement.currentGradeName = res.presentGradeName;
          this.empPromotionIncrementService.empPromotionIncrement.currentScaleId = res.presentScaleId;
          this.empPromotionIncrementService.empPromotionIncrement.currentScaleName = res.presentScaleName;
          this.empPromotionIncrementService.empPromotionIncrement.currentBasicPay = res.basicPay;
        }
      }
    })
    )
  }
  
  getEmpJobDetailsNewByEmpId(id: number){
    // this.subscription = 
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empJobDetailsId = res.id;
        this.getDesignationByDepartmentId(res.departmentId, res.id);
          this.empPromotionIncrementService.empPromotionIncrement.currentSectionName = res.sectionName;
          this.empPromotionIncrementService.empPromotionIncrement.currentDepartmentName = res.departmentName;
          this.empPromotionIncrementService.empPromotionIncrement.currentDesignationName = res.designationName;
          this.empPromotionIncrementService.empPromotionIncrement.currentDepartmentId = res.departmentId;
          this.empPromotionIncrementService.empPromotionIncrement.currentSectionId = res.sectionId;
          this.empPromotionIncrementService.empPromotionIncrement.currentDesignationId = res.designationId;
          this.empPromotionIncrementService.empPromotionIncrement.currentGradeId = res.presentGradeId;
          this.empPromotionIncrementService.empPromotionIncrement.currentGradeName = res.presentGradeName;
          this.empPromotionIncrementService.empPromotionIncrement.currentScaleId = res.presentScaleId;
          this.empPromotionIncrementService.empPromotionIncrement.currentScaleName = res.presentScaleName;
          this.empPromotionIncrementService.empPromotionIncrement.currentBasicPay = res.basicPay;
          this.empPromotionIncrementService.empPromotionIncrement.currentDeptJoinDate = res.currentPositionJoinDate;
      }
    })
    )
    
  }

  getDesignationByDepartmentId(departmentId: number | null, empJobDetailsId: number) {
    this.designations = [];
    this.empTransferPostingService.empTransferPosting.transferDesignationId = null;
    this.subscription.push(
    this.empJobDetailsService.getDesignationByDepartmentId(departmentId, empJobDetailsId).subscribe((res) => {
      this.designations = res;
    })
    )
   
  }

  SelectedModelGrade() {
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
      this.empPromotionIncrementService.empPromotionIncrement.updateBasicPay = res.basicPay;
    })
    )
    
  }

  

  onSubmit(form: NgForm): void {
    this.loading = true;
    if(form.value.provideApprovalInfo == true){
      form.value.approveById = this.loginEmpId;
    }
    this.empPromotionIncrementService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.empPromotionIncrementService.updateEmpPromotionIncrement(id, form.value)
      : this.empPromotionIncrementService.saveEmpPromotionIncrement(form.value);

    // this.subscription = 
    this.subscription.push(
    action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        
        // Notification Start
        if(id == 0) {
          if(this.empPromotionIncrementService.empPromotionIncrement.isApproval && !this.empPromotionIncrementService.empPromotionIncrement.provideApprovalInfo){
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empPromotionIncrementService.empPromotionIncrement.empId
            userNotification.toDeptId = this.empPromotionIncrementService.empPromotionIncrement.currentDepartmentId;
            userNotification.featurePath = 'incrementAndPromotionApproval';
            userNotification.nevigateLink = '/promotion/incrementAndPromotionApproval';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Promotion and Increment';
            userNotification.message = 'new Increment and Promotion application, Approval Pending.';
            this.notificationService.submit(userNotification).subscribe((res) => {});
          }
          else if(!this.empPromotionIncrementService.empPromotionIncrement.isApproval || this.empPromotionIncrementService.empPromotionIncrement.provideApprovalInfo){
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empPromotionIncrementService.empPromotionIncrement.empId;
            userNotification.toEmpId = this.empPromotionIncrementService.empPromotionIncrement.empId;
            userNotification.featurePath = 'profile';
            userNotification.nevigateLink = '/employee/profile';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Promotion and Increment';
            userNotification.message = 'your promotion and increment have been approved.';
            this.notificationService.submit(userNotification).subscribe((res) => {});
          }
        }
        // Notification End
        
        this.loading = false;
        this.router.navigate(['/promotion/manage-incrementAndPromotion']);
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

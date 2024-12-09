import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { EmpRewardPunishmentService } from '../service/emp-reward-punishment.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell, cilSearch } from '@coreui/icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { RewardPunishmentSetupService } from '../../basic-setup/service/reward-punishment-setup.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EmpTransferPostingService } from '../../transferPosting/service/emp-transfer-posting.service';
import { EmpJobDetailsService } from '../service/emp-job-details.service';
import { EmployeeListModalComponent } from '../employee-list-modal/employee-list-modal.component';

@Component({
  selector: 'app-reward-punishment',
  templateUrl: './reward-punishment.component.html',
  styleUrl: './reward-punishment.component.scss'
})
export class RewardPunishmentComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modalOpened: boolean = false;
  rewardType: SelectedModel[] = [];
  rewardPriority: SelectedModel[] = [];
  isValidEmp: boolean = false;
  empJobDetailsId: number = 0;
  isPriority = true;
  isWithdraw = true;

  @ViewChild('EmpRewardPunishmentFrom', { static: true }) EmpRewardPunishmentFrom!: NgForm;

  constructor(
    private toastr: ToastrService,
    public empRewardPunishmentService: EmpRewardPunishmentService,
    public rewardPunishmentSetupService: RewardPunishmentSetupService,
    public empTransferPostingService: EmpTransferPostingService,
    public empJobDetailsService : EmpJobDetailsService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    private modalService: BsModalService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell, cilSearch };

  ngOnInit(): void {
    console.log(this.id)
    this.initaialRewardPunishment();
    this.handleText();
    this.getRewardPunishmentInfo();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }

  handleText(){
    this.getSelectedRewardPunishmentType();
    this.getSelectedRewardPunishmentPriority();
    this.heading = 
      this.clickedButton === 'Edit' ? 'Edit Reward/Punishment Information' : 
      this.clickedButton === 'Create' ? 'Create Reward/Punishment Information' : 
      this.clickedButton === 'Withdraw' ? 'Withdraw Reward/Punishment Information' : 
      'Reward/Punishment Information';
      this.btnText = 
        this.clickedButton === 'Edit' ? 'Update' : 
        this.clickedButton === 'Create' ? 'Submit' : 
        this.clickedButton === 'Withdraw' ? 'Withdraw' : 
        'Submit';
      this.btnIcon = 
        this.clickedButton === 'Edit' ? 'update' : 
        this.clickedButton === 'Create' ? 'save' : 
        this.clickedButton === 'Withdraw' ? 'how_to_reg' : 
        'save';
  }

  getRewardPunishmentInfo() {
    this.subscription.push(
     this.empRewardPunishmentService.findById(this.id).subscribe((res) => {
      if(res){
        this.isValidEmp = true;
        // this.getEmpInfoByIdCardNo(res.empIdCardNo);
        this.getWithdrawAndPriorityStatus(res.rewardPunishmentTypeId);
        this.EmpRewardPunishmentFrom?.form.patchValue(res);
        this.empRewardPunishmentService.empRewardPunishment.empName = res.empName;
        this.empRewardPunishmentService.empRewardPunishment.departmentName = res.departmentName;
        this.empRewardPunishmentService.empRewardPunishment.sectionName = res.sectionName;
        this.empRewardPunishmentService.empRewardPunishment.designationName = res.designationName;
      }
    })
    )
  }

  getWithdrawAndPriorityStatus(id: number){
    this.subscription.push(
      this.rewardPunishmentSetupService.findRewardType(id).subscribe((res) =>{
        this.isPriority = res.isPriority;
        this.isWithdraw = res.isWithdraw;
      })
    )
  }
  
  getEmpInfoByIdCardNo(idCardNo: string) {
    this.subscription.push(
    this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
      if (res) {
            this.isValidEmp = true;
            this.empRewardPunishmentService.empRewardPunishment.empName = res.firstName + " " + res.lastName;
            this.empRewardPunishmentService.empRewardPunishment.empId = res.id;
            this.getEmpJobDetailsByEmpId(res.id);
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
  
  EmployeeListModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl'  });

    modalRef.content.employeeSelected.subscribe((idCardNo: string) => {
      if(idCardNo){
        this.getEmpInfoByIdCardNo(idCardNo);
        this.empTransferPostingService.empTransferPosting.empIdCardNo = idCardNo;
      }
    });
  }

  
  getEmpJobDetailsByEmpId(id: number){
    // this.subscription = 
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empJobDetailsId = res.id;
          this.empRewardPunishmentService.empRewardPunishment.departmentName = res.departmentName;
          this.empRewardPunishmentService.empRewardPunishment.sectionName = res.sectionName;
          this.empRewardPunishmentService.empRewardPunishment.designationName = res.designationName;
          this.empRewardPunishmentService.empRewardPunishment.departmentId = res.departmentId;
          this.empRewardPunishmentService.empRewardPunishment.sectionId = res.sectionId;
          this.empRewardPunishmentService.empRewardPunishment.designationId = res.designationId;
      }
    })
    )
    
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  initaialRewardPunishment(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empRewardPunishmentService.empRewardPunishment = {
      id : 0,
      empId : null,
      departmentId : null,
      sectionId : null,
      designationId : null,
      rewardPunishmentTypeId : null,
      rewardPunishmentPriorityId : null,
      rewardPunishmentDate : null,
      startDate : null,
      endDate : null,
      description : '',
      orderNo : '',
      orderDate : null,
      withdrawStatus : false,
      withdrawOrderNo : '',
      withdrawDate : null,
      orderBy : null,
      applicationBy : null,
      approveById : null,
      approveDate : null,
      approveStatus : null,
      menuPosition : 0,
      remark : '',
      isActive : true,
  
      empIdCardNo : '',
      empName : '',
      departmentName : '',
      sectionName : '',
      designationName : '',
      rewardPunishmentTypeName : '',
      rewardPunishmentPriorityName : '',
    };
  }

  getSelectedRewardPunishmentType(){
    // this.subscription = 
    this.subscription.push(
    this.rewardPunishmentSetupService.getSelectedRewardType().subscribe((res) =>{
      this.rewardType = res;
    })
    )
    
  }
  getSelectedRewardPunishmentPriority(){
    // this.subscription =
    this.subscription.push(
    this.rewardPunishmentSetupService.getSelectedRewardPriority().subscribe((res) =>{
      this.rewardPriority = res;
    })
    )
    
  }

  closeModal(): void {
    this.bsModalRef.hide();
  }
  
  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent): void {
    if (this.modalOpened) {
      const modalElement = this.el.nativeElement.querySelector('.modal-content');
      if (modalElement && !modalElement.contains(event.target as Node)) {
        this.shakeModal();
      }
    }
  }

  shakeModal(): void {
    const modalElement = this.el.nativeElement.querySelector('.modal-content');
    if (modalElement) {
      this.renderer.addClass(modalElement, 'shake');
      setTimeout(() => {
        this.renderer.removeClass(modalElement, 'shake');
      }, 500);
    }
  }

  onSubmit(form: NgForm): void {
    this.empRewardPunishmentService.cachedData = [];
    const id = form.value.id;
    console.log(form.value)
    const action$ = id
      ? this.empRewardPunishmentService.updateEmpRewardPunishment(id, form.value)
      : this.empRewardPunishmentService.saveEmpRewardPunishment(form.value);

    
    this.subscription.push(
    action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.closeModal();
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    })
    )
    
  }
}
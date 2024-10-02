import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { EmpRewardPunishmentService } from '../service/emp-reward-punishment.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { RewardPunishmentSetupService } from '../../basic-setup/service/reward-punishment-setup.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EmpTransferPostingService } from '../../transferPosting/service/emp-transfer-posting.service';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';

@Component({
  selector: 'app-reward-punishment',
  templateUrl: './reward-punishment.component.html',
  styleUrl: './reward-punishment.component.scss'
})
export class RewardPunishmentComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
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
    private renderer: Renderer2
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };

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
    this.heading = this.clickedButton == 'Edit' ? 'Edit Reward/Punishment Information' : 'Create Reward/Punishment Information';
    this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
    this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
  }

  getRewardPunishmentInfo() {
    this.subscription = this.empRewardPunishmentService.findById(this.id).subscribe((res) => {
      if(res){
        console.log(res)
        this.getEmpInfoByIdCardNo(res.empIdCardNo);
        this.EmpRewardPunishmentFrom?.form.patchValue(res);
      }
    });
  }
  
  getEmpInfoByIdCardNo(idCardNo: string) {
    this.subscription = this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
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
  }

  
  getEmpJobDetailsByEmpId(id: number){
    this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empJobDetailsId = res.id;
          this.empRewardPunishmentService.empRewardPunishment.departmentName = res.departmentName;
          this.empRewardPunishmentService.empRewardPunishment.designationName = res.designationName;
      }
    })
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  initaialRewardPunishment(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empRewardPunishmentService.empRewardPunishment = {
      id : 0,
      empId : null,
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
      designationName : '',
      rewardPunishmentTypeName : '',
      rewardPunishmentPriorityName : '',
    };
  }

  getSelectedRewardPunishmentType(){
    this.subscription = this.rewardPunishmentSetupService.getSelectedRewardType().subscribe((res) =>{
      this.rewardType = res;
    });
  }
  getSelectedRewardPunishmentPriority(){
    this.subscription = this.rewardPunishmentSetupService.getSelectedRewardPriority().subscribe((res) =>{
      this.rewardPriority = res;
    });
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
    const action$ = id
      ? this.empRewardPunishmentService.updateEmpRewardPunishment(id, form.value)
      : this.empRewardPunishmentService.saveEmpRewardPunishment(form.value);

    this.subscription = action$.subscribe((response: any) => {
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
    });
  }
}
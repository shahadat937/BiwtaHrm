import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { EmpPromotionIncrementService } from '../service/emp-promotion-increment.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';
import { EmpPromotionIncrement } from '../model/emp-promotion-increment';
import { UserNotification } from 'src/app/views/notifications/models/user-notification';
import { NotificationService } from 'src/app/views/notifications/service/notification.service';

@Component({
  selector: 'app-increment-and-promotion-approval',
  templateUrl: './increment-and-promotion-approval.component.html',
  styleUrl: './increment-and-promotion-approval.component.scss'
})
export class IncrementAndPromotionApprovalComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  empPromotionIncrement: EmpPromotionIncrement = new EmpPromotionIncrement();
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  modalOpened: boolean = false;
  loginEmpId : number = 0;
  loginEmpCurrentDepartmentId: any = null;
  loginEmpCurrentSectionId: any = null;
  loginEmpCurrentDesignationId : any = null;
  loginEmpCurrentResponsibilityTypeId : any = null;

  @ViewChild('EmpTransferPostingForm', { static: true }) EmpTransferPostingForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public empPromotionIncrementService: EmpPromotionIncrementService,
    public empJobDetailsService: EmpJobDetailsService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    public notificationService: NotificationService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };

  ngOnInit(): void {
    this.handleText();
    this.getTransferPostingInfo();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);

    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId;
    this.loginEmpCurrentDepartmentId = currentUserJSON.departmentId;
    this.loginEmpCurrentSectionId = currentUserJSON.sectionId;
    this.loginEmpCurrentDesignationId = currentUserJSON.designationId;
    this.loginEmpCurrentResponsibilityTypeId = currentUserJSON.responsibilityTypeId;
  }

  handleText(){
    this.heading = this.clickedButton == 'Approve' ? 'Approve Transfer Posting Application' :
                 this.clickedButton == 'Reject' ? 'Reject Transfer Posting Application' :
                 this.clickedButton == 'Edit' ? 'Edit Transfer Posting Application' : '';
  }

  getTransferPostingInfo() {
    // this.subscription = 
    this.subscription.push(
    this.empPromotionIncrementService.findById(this.id).subscribe((res) => {
      if(res){
        this.empPromotionIncrement = res;
        this.EmpTransferPostingForm?.form.patchValue(res);
      }
    })
    )
    
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
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

  onSubmit(ApproveStatus?: boolean){    
      let prevApproveStatus = this.empPromotionIncrement.approveStatus;

      this.empPromotionIncrementService.cachedData = [];
      if(ApproveStatus == true || ApproveStatus == false){
        this.empPromotionIncrement.approveStatus = ApproveStatus;
      }
      else{
        this.empPromotionIncrement.approveStatus = this.empPromotionIncrementService.empPromotionIncrement.approveStatus;
      }
      // if(this.empPromotionIncrementService.empPromotionIncrement.approveStatus != true || this.empPromotionIncrementService.empPromotionIncrement.approveStatus != false){
      //   this.empPromotionIncrement.approveStatus = null;  
      // }
      this.empPromotionIncrement.approveById = this.loginEmpId;
      this.empPromotionIncrement.approveByDepartmentId = this.loginEmpCurrentDepartmentId;
      this.empPromotionIncrement.approveBySectionId = this.loginEmpCurrentSectionId;
      this.empPromotionIncrement.approveByDesignationId = this.loginEmpCurrentDesignationId;
      this.empPromotionIncrement.approveByResponsibityTypeId = this.loginEmpCurrentResponsibilityTypeId;
      this.empPromotionIncrement.approveDate = new Date().toISOString().split('T')[0] as any as Date;
      this.empPromotionIncrement.approveRemark = this.empPromotionIncrementService.empPromotionIncrement.approveRemark;
      // this.subscription = 
      this.subscription.push(
      this.empPromotionIncrementService.updateEmpPromotionIncrement(this.empPromotionIncrement.id, this.empPromotionIncrement).subscribe((response: any) => {
        if (response.success) {
          if(ApproveStatus == true){
            this.toastr.success('', `Application Approved Successfull`, {
              positionClass: 'toast-top-right',
            });
            
            //Notification
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empPromotionIncrement.approveById;
            userNotification.toEmpId = this.empPromotionIncrement.empId;
            userNotification.featurePath = 'profile';
            userNotification.nevigateLink = '/employee/profile';
            userNotification.forEntryId = this.empPromotionIncrement.id;
            userNotification.title = 'Transfer and Posting';
            userNotification.message = 'approved your '+ this.empPromotionIncrement.promotionIncrementType;
            this.subscription.push(this.notificationService.submit(userNotification).subscribe((res) => {}));
          }
          else if(ApproveStatus == false){
            this.toastr.error('', `Application Rejected Successfull`, {
              positionClass: 'toast-top-right',
            });
            
            //Notification
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empPromotionIncrement.approveById;
            userNotification.toEmpId = this.empPromotionIncrement.applicationById;
            userNotification.featurePath = 'manage-incrementAndPromotion';
            userNotification.nevigateLink = '/promotion/manage-incrementAndPromotion';
            userNotification.forEntryId = this.empPromotionIncrement.id;
            userNotification.title = 'Transfer and Posting';
            userNotification.message = 'rejected your application for '+ this.empPromotionIncrement.promotionIncrementType;
            this.subscription.push(this.notificationService.submit(userNotification).subscribe((res) => {}));
          }
          else{
            this.toastr.success('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });

            //Notifications
            if(prevApproveStatus != this.empPromotionIncrement.approveStatus && this.empPromotionIncrement.approveStatus == true){
              const userNotification = new UserNotification();
              userNotification.fromEmpId = this.empPromotionIncrement.approveById;
              userNotification.toEmpId = this.empPromotionIncrement.empId;
              userNotification.featurePath = 'profile';
              userNotification.nevigateLink = '/employee/profile';
              userNotification.forEntryId = this.empPromotionIncrement.id;
              userNotification.title = 'Transfer and Posting';
              userNotification.message = 'approved your '+ this.empPromotionIncrement.promotionIncrementType;
              this.subscription.push(this.notificationService.submit(userNotification).subscribe((res) => {}));
            }
            else if(prevApproveStatus != this.empPromotionIncrement.approveStatus && this.empPromotionIncrement.approveStatus == false){
              const userNotification = new UserNotification();
              userNotification.fromEmpId = this.empPromotionIncrement.approveById;
              userNotification.toEmpId = this.empPromotionIncrement.applicationById;
              userNotification.featurePath = 'manage-incrementAndPromotion';
              userNotification.nevigateLink = '/promotion/manage-incrementAndPromotion';
              userNotification.forEntryId = this.empPromotionIncrement.id;
              userNotification.title = 'Transfer and Posting';
              userNotification.message = 'rejected your application for '+ this.empPromotionIncrement.promotionIncrementType;
              this.subscription.push(this.notificationService.submit(userNotification).subscribe((res) => {}));
            }
          }
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
        }
        this.closeModal();
      })
      )
      
    }
}
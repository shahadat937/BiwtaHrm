import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpJobDetailsService } from 'src/app/views/employee/service/emp-job-details.service';
import { EmpTransferPosting } from '../../model/emp-transfer-posting';
import { EmpTransferPostingService } from '../../service/emp-transfer-posting.service';
import { UserNotification } from 'src/app/views/notifications/models/user-notification';
import { NotificationService } from 'src/app/views/notifications/service/notification.service';

@Component({
  selector: 'app-joining-reporting',
  templateUrl: './joining-reporting.component.html',
  styleUrl: './joining-reporting.component.scss'
})
export class JoiningReportingComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  empTransferPosting: EmpTransferPosting = new EmpTransferPosting();
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  modalOpened: boolean = false;
  loginEmpId : number = 0;

  @ViewChild('EmpTransferPostingForm', { static: true }) EmpTransferPostingForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
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
  }

  handleText(){
    this.heading = this.clickedButton == 'Approve' ? 'Approve Transfer Posting Application' :
                 this.clickedButton == 'Reject' ? 'Reject Transfer Posting Application' :
                 this.clickedButton == 'Edit' ? 'Edit Transfer Posting Application' : '';
  }

  getTransferPostingInfo() {
    this.subscription.push(
    this.empTransferPostingService.findById(this.id).subscribe((res) => {
      if(res){
        this.empTransferPosting = res;
        // this.getEmpJobDetailsByEmpIdOfOrderOfficeBy(res.orderOfficeById || 0);
        this.getEmpJobDetailsByEmpIdOfTransferApproveBy( res.transferApproveById ||0 );
        this.getEmpJobDetailsByEmpIdDeptApproveBy( res.deptReleaseById ||0 );
        this.EmpTransferPostingForm?.form.patchValue(res);
      }
    })
    )
   
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  // getEmpJobDetailsByEmpIdOfOrderOfficeBy(id: number){
  //   this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
  //     if(res){
  //       this.empTransferPosting.orderByDepartmentName = res.departmentName;
  //       this.empTransferPosting.orderByDesignationName = res.designationName;
  //       this.empTransferPosting.orderBySectionName = res.sectionName;
  //     }
  //   })
  // }
  
  getEmpJobDetailsByEmpIdOfTransferApproveBy(id: number){
    this.subscription.push(
       this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empTransferPosting.approveByDepartmentName = res.departmentName;
        this.empTransferPosting.approveByDesignationName = res.designationName;
        this.empTransferPosting.approveBySectionName = res.sectionName;
      }
    })
    )
  
  }
  
  getEmpJobDetailsByEmpIdDeptApproveBy(id: number){
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empTransferPosting.deptReleaseByDepartmentName = res.departmentName;
        this.empTransferPosting.deptReleaseByDesignationName = res.designationName;
        this.empTransferPosting.deptReleaseBySectionName = res.sectionName;
      }
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

  onSubmit(joiningStatus?: boolean){
    
    let prevJoiningStatus = this.empTransferPosting.joiningStatus;

    this.empTransferPostingService.cachedData = [];
      if(joiningStatus == true || joiningStatus == false){
        this.empTransferPosting.joiningStatus = joiningStatus;
      }
      else{
        this.empTransferPosting.joiningStatus = this.empTransferPostingService.empTransferPosting.joiningStatus;
      }
      this.empTransferPosting.joiningReportingById = this.loginEmpId;
      this.empTransferPosting.joiningRemark = this.empTransferPostingService.empTransferPosting.joiningRemark;
      this.empTransferPosting.joiningDate = this.empTransferPostingService.empTransferPosting.joiningDate;
      
      this.subscription.push(
      this.empTransferPostingService.updateEmpTransferPostingStatus(this.empTransferPosting.id, this.empTransferPosting).subscribe((response: any) => {
        if (response.success) {
          if(joiningStatus == true){
            this.toastr.success('', `Application Approved Successfull`, {
              positionClass: 'toast-top-right',
            });

            //Notification
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empTransferPosting.joiningReportingById;
            userNotification.toEmpId = this.empTransferPosting.empId;
            userNotification.featurePath = 'profile';
            userNotification.nevigateLink = '/employee/profile';
            userNotification.forEntryId = this.empTransferPosting.id;
            userNotification.title = 'Transfer and Posting';
            userNotification.message = 'approved you joining in '+ this.empTransferPosting.departmentName + ' department on ' + this.empTransferPosting.joiningDate;
            this.subscription.push(this.notificationService.submit(userNotification).subscribe((res) => {}));
          }
          else if(joiningStatus == false){
            this.toastr.error('', `Application Rejected Successfull`, {
              positionClass: 'toast-top-right',
            });

             //Notification
             const userNotification = new UserNotification();
             userNotification.fromEmpId = this.empTransferPosting.joiningReportingById;
             userNotification.toEmpId = this.empTransferPosting.applicationById;
             userNotification.featurePath = 'transferPostingList';
             userNotification.nevigateLink = '/transferPosting/transferPostingList';
             userNotification.forEntryId = this.empTransferPosting.id;
             userNotification.title = 'Transfer and Posting';
             userNotification.message = 'rejected your application from Joining.';
             this.subscription.push(this.notificationService.submit(userNotification).subscribe((res) => {}));
          }
          else{
            this.toastr.success('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });

            //Notification
            if(prevJoiningStatus != this.empTransferPosting.joiningStatus && this.empTransferPosting.joiningStatus == true){
              const userNotification = new UserNotification();
              userNotification.fromEmpId = this.empTransferPosting.joiningReportingById;
              userNotification.toEmpId = this.empTransferPosting.empId;
              userNotification.featurePath = 'profile';
              userNotification.nevigateLink = '/employee/profile';
              userNotification.forEntryId = this.empTransferPosting.id;
              userNotification.title = 'Transfer and Posting';
              userNotification.message = 'approved you joining in '+ this.empTransferPosting.transferDepartmentName + ' department on ' + this.empTransferPosting.joiningDate;
              this.subscription.push(this.notificationService.submit(userNotification).subscribe((res) => {}));
            }
            else if(prevJoiningStatus != this.empTransferPosting.joiningStatus && this.empTransferPosting.joiningStatus == false){
              const userNotification = new UserNotification();
              userNotification.fromEmpId = this.empTransferPosting.joiningReportingById;
              userNotification.toEmpId = this.empTransferPosting.applicationById;
              userNotification.featurePath = 'transferPostingList';
              userNotification.nevigateLink = '/transferPosting/transferPostingList';
              userNotification.forEntryId = this.empTransferPosting.id;
              userNotification.title = 'Transfer and Posting';
              userNotification.message = 'rejected your application from Joining.';
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
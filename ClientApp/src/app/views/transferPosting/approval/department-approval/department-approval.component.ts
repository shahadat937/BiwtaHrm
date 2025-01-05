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
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { UserNotification } from 'src/app/views/notifications/models/user-notification';
import { NotificationService } from 'src/app/views/notifications/service/notification.service';

@Component({
  selector: 'app-department-approval',
  templateUrl: './department-approval.component.html',
  styleUrl: './department-approval.component.scss'
})
export class DepartmentApprovalComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  empTransferPosting: EmpTransferPosting = new EmpTransferPosting();
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  modalOpened: boolean = false;
  loginEmpId : number = 0;
  releaseTypes: SelectedModel[] = [];

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
    this.getSelectedReleaseType();
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
  
  getSelectedReleaseType(){

    this.subscription.push(
    this.empTransferPostingService.getSelectedReleaseType().subscribe((data) => {
      this.releaseTypes = data;
    })
    )
    
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

  onSubmit(deptApproveStatus?: boolean){
    this.empTransferPostingService.cachedData = [];
    if(this.empTransferPosting.joiningStatus!=null){
      this.toastr.warning('', `Already Joining Status Updated`, {
        positionClass: 'toast-top-right',
      });
    }
    else{
      if(deptApproveStatus == true || deptApproveStatus == false){
        this.empTransferPosting.deptApproveStatus = deptApproveStatus;
      }
      else{
        this.empTransferPosting.deptApproveStatus = this.empTransferPostingService.empTransferPosting.deptApproveStatus;
      }
      this.empTransferPosting.deptReleaseById = this.loginEmpId;
      this.empTransferPosting.deptReleaseTypeId = this.empTransferPostingService.empTransferPosting.deptReleaseTypeId;
      this.empTransferPosting.deptReleaseDate = this.empTransferPostingService.empTransferPosting.deptReleaseDate;
      this.empTransferPosting.referenceNo = this.empTransferPostingService.empTransferPosting.referenceNo;
      this.empTransferPosting.deptClearance = this.empTransferPostingService.empTransferPosting.deptClearance;
      this.empTransferPosting.deptRemark = this.empTransferPostingService.empTransferPosting.deptRemark;
      
      this.subscription.push(
      this.empTransferPostingService.updateEmpTransferPostingStatus(this.empTransferPosting.id, this.empTransferPosting).subscribe((response: any) => {
        if (response.success) {
          if(deptApproveStatus == true){
            this.toastr.success('', `Application Approved Successfull`, {
              positionClass: 'toast-top-right',
            });

            //Notification
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empTransferPosting.empId;
            userNotification.toDeptId = this.empTransferPosting.transferDepartmentId;
            userNotification.featurePath = 'joiningReportingList';
            userNotification.nevigateLink = '/transferPosting/joiningReportingList';
            userNotification.forEntryId = this.empTransferPosting.id;
            userNotification.title = 'Transfer and Posting';
            userNotification.message = 'released from Department, Joining Information Pending.';
            this.notificationService.submit(userNotification).subscribe((res) => {});
          
          }
          else if(deptApproveStatus == false){
            this.toastr.error('', `Application Rejected Successfull`, {
              positionClass: 'toast-top-right',
            });
            
            //Notification
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.empTransferPosting.deptReleaseById;
            userNotification.toEmpId = this.empTransferPosting.applicationById;
            userNotification.featurePath = 'transferPostingList';
            userNotification.nevigateLink = '/transferPosting/transferPostingList';
            userNotification.forEntryId = this.empTransferPosting.id;
            userNotification.title = 'Transfer and Posting';
            userNotification.message = 'rejected your application.';
            this.notificationService.submit(userNotification).subscribe((res) => {});
          }
          else{
            this.toastr.success('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
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
}
import { Component, ElementRef, Input, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { LeaveService } from "../../service/leave.service";
import { ToastrService } from 'ngx-toastr';
import { ManageLeaveService } from '../../service/manage-leave.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Subscription } from 'rxjs';
import { LeaveModel } from '../../models/leave-model';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthService } from 'src/app/core/service/auth.service';
import { environment } from 'src/environments/environment';
import { AddLeaveService } from '../../service/add-leave.service';
import { HttpParams } from '@angular/common/http';
import { ThanaService } from 'src/app/views/basic-setup/service/thana.service';
import { update } from 'lodash-es';
import { UserNotification } from '../../../notifications/models/user-notification';
import { NotificationService } from '../../../notifications/service/notification.service';

@Component({
  selector: 'app-leave-detail-view',
  templateUrl: './leave-detail-view.component.html',
  styleUrl: './leave-detail-view.component.scss'
})
export class LeaveDetailViewComponent implements OnInit, OnDestroy{

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;
  @Input() leaveRequestId: number = 0;
  @Input() CanApprove: boolean = false;
  @Input() Role : string = "Reviewer";
  leaveData : LeaveModel = new LeaveModel();
  updateLeaveData: LeaveModel = new LeaveModel();
  leaveStatusOption: string [] = [];
  modalOpened: boolean = false;
  baseImageUrl: string;
  totalLeave: number;
  leaveFiles: any[];
  IsUpdating: boolean
  constructor (
    public leaveService: ManageLeaveService,
    private notificationService: NotificationService,
    private addLeaveService: AddLeaveService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private authService: AuthService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {
    this.baseImageUrl = environment.imageUrl;
    this.totalLeave = 0;
    this.leaveFiles = [];
    this.IsUpdating = false;
  }

  ngOnInit(): void {
    //this.getLeaveStatusOption();
    this.loading = true;
    this.getLeaveRequestById();
    this.getLeaveFiles();
    //this.getWorkingDays();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);

  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    } 
  }

  getLeaveRequestById() {
    // this.subscription = 
    this.subscription.push(
      this.leaveService.getLeaveById(this.leaveRequestId).subscribe({
      next: response=> {
        this.leaveData = response;
        this.updateLeaveData = this.leaveData;
        this.getWorkingDays();
      },
      error: err => {
        this.loading = false;
        console.log(err);
      },
      complete: () => {
        this.loading = false;
      }
    })
    )
    
  }

  getLeaveStatusOption() {
    // this.subscription = 
    this.subscription.push(
      this.leaveService.getLeaveStatusOption().subscribe({
      next: option=> {
        this.leaveStatusOption = option;
      },
      error: err=> {
        console.log(err);
      }
    })
    )
    
  }

  modalClose() {
    this.bsModalRef.hide();
  }


  approveLeaveRequest() {
    this.loading = true;
    if(this.Role=='Approver') {
      // this.subscription = 
      this.subscription.push(
         this.leaveService.approveFinalLeaveRequest(this.leaveRequestId).subscribe({
        next: response=> {
          if(response.success == true) {
            this.toastr.success('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })

            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.leaveData.approvedBy;
            userNotification.toEmpId = this.leaveData.empId;
            userNotification.featurePath = 'personalleave';
            userNotification.nevigateLink = '/leave/personalleave';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Leave Application';
            userNotification.message = 'Your leave request is approved';
            this.notificationService.submit(userNotification).subscribe((res) => {});
            
            this.getLeaveRequestById();
          } else {
            this.toastr.warning('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
          }
        },
        error: error=> {
          this.loading = false;
        },
        complete: ()=> {
          this.loading = false;
        }
      })
      )
     
    } else if(this.Role=="Reviewer"){
      this.subscription.push(
        this.leaveService.approveLeaveRequestByReviewer(this.leaveRequestId).subscribe({
        next: response=> {
          if(response.success == true) {
            this.toastr.success('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })


            // Send notification to user
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.leaveData.reviewedBy;
            userNotification.toEmpId = this.leaveData.empId;
            userNotification.featurePath = 'personalleave';
            userNotification.nevigateLink = '/leave/personalleave';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Leave Application';
            userNotification.message = 'Your leave request is recommended';
            this.notificationService.submit(userNotification).subscribe((res) => {});


            //Send Notification to approver
            userNotification.fromEmpId = this.leaveData.reviewedBy;
            userNotification.toEmpId = this.leaveData.approvedBy;
            userNotification.featurePath = 'finalapprove';
            userNotification.nevigateLink = '/leave/finalapprove';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Leave Application';
            userNotification.message = 'recommended leave application, Approval Pending.';
            this.notificationService.submit(userNotification).subscribe((res) => {});

            this.getLeaveRequestById();
          } else {
            this.toastr.warning('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
          }
        },
        error: error=> {
          this.loading = false;
        },
        complete: ()=> {
          this.loading = false;
        }
      })
      )
      
    } else {
      this.toastr.warning('',"Invalid Role", {
        positionClass: 'toast-top-right'
      });
    }
  }
  
  denyLeaveRequest() {
    this.loading = true;
    if(this.Role=='Approver') {
      // this.subscription = 
      this.subscription.push(
        this.leaveService.denyFinalLeaveRequest(this.leaveRequestId).subscribe({
        next: response=> {
          if(response.success == true) {
            this.toastr.success('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })

            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.leaveData.approvedBy;
            userNotification.toEmpId = this.leaveData.empId;
            userNotification.featurePath = 'personalleave';
            userNotification.nevigateLink = '/leave/personalleave';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Leave Application';
            userNotification.message = 'Your leave request is denied';
            this.notificationService.submit(userNotification).subscribe((res) => {});

            this.getLeaveRequestById();
          } else {
            this.toastr.warning('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
          }
        },
        error: error=> {
          this.loading = false;
        },
        complete: ()=> {
          this.loading = false;
        }
      })
      )
      
    } else if (this.Role == "Reviewer"){
      // this.subscription = 
      this.subscription.push(
        this.leaveService.denyLeaveRequestByReviewer(this.leaveRequestId).subscribe({
        next: response=> {
          if(response.success == true) {
            this.toastr.success('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
            const userNotification = new UserNotification();
            userNotification.fromEmpId = this.leaveData.reviewedBy;
            userNotification.toEmpId = this.leaveData.empId;
            userNotification.featurePath = 'personalleave';
            userNotification.nevigateLink = '/leave/personalleave';
            userNotification.forEntryId = response.id;
            userNotification.title = 'Leave Application';
            userNotification.message = 'Your leave request is denied';
            this.notificationService.submit(userNotification).subscribe((res) => {});
            this.getLeaveRequestById();
          } else {
            this.toastr.warning('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
          }
        },
        error: error=> {
          this.loading = false;
        },
        complete: ()=> {
          this.loading = false;
        }
      })
      )
      
    } else {
      this.toastr.warning('',"Invalid Role");
    }
  }

  getWorkingDays() {
    if(this.leaveData.fromDate==""||this.leaveData.toDate=="") {
      console.log(this.leaveData);
      return;
      this.totalLeave = 0;
    }
    let params = new HttpParams();
    params = params.set("From", this.leaveData.fromDate);
    params = params.set("To", this.leaveData.toDate);
    // this.subscription = 
    this.subscription.push(
      this.addLeaveService.getWorkingDays(params).subscribe({
      next: response => {
        this.totalLeave = response;
      }
    })
    )
    
  }

  getLeaveFiles() {
    this.subscription.push(
      this.leaveService.getLeaveFiles(this.leaveRequestId).subscribe({
      next: response => {
        this.leaveFiles = response;
        console.log(this.leaveFiles);
      }
    })
    )
    
  }

  updateLeaveRequest() {
    this.loading = true;
    const formData = this.convertToFormData(this.updateLeaveData);
    this.subscription.push(
      this.leaveService.updateLeaveRequest(formData).subscribe({
      next: response => {
        if(response.success) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
          this.leaveData = this.updateLeaveData;
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: (err) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    })
    )
    
  }

  convertToFormData(model: any, fileFields: string[] = []): FormData {
    const formData = new FormData();
    for (const key in model) {
      if (model.hasOwnProperty(key)&&model[key]!=null) {
        if(key=="associatedFiles"||Array.isArray(model[key])) {
          model[key].forEach((data:any)=> {
            formData.append(key,data);
          })
          continue;
        }

        formData.append(key, model[key]);
      }
    }
    return formData;
  }

  toggleUpdate() {
    this.updateLeaveData = this.leaveData;
    this.IsUpdating = !this.IsUpdating;
  }
}

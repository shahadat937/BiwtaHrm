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

@Component({
  selector: 'app-leave-detail-view',
  templateUrl: './leave-detail-view.component.html',
  styleUrl: './leave-detail-view.component.scss'
})
export class LeaveDetailViewComponent implements OnInit, OnDestroy{

  loading: boolean = false;
  @Input() leaveRequestId: number = 0;
  subscription: Subscription = new Subscription();
  leaveData : LeaveModel = new LeaveModel();
  leaveStatusOption: string [] = [];
  modalOpened: boolean = false;
  constructor (
    public leaveService: ManageLeaveService,
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
  }

  ngOnInit(): void {
    this.getLeaveStatusOption();
    this.getLeaveRequestById();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);

  }

  ngOnDestroy(): void {
    
  }

  getLeaveRequestById() {
    this.subscription = this.leaveService.getLeaveById(this.leaveRequestId).subscribe({
      next: response=> {
        this.leaveData = response;
      },
      error: err => {
        console.log(err);
      }
    });
  }

  getLeaveStatusOption() {
    this.leaveService.getLeaveStatusOption().subscribe({
      next: option=> {
        this.leaveStatusOption = option;
      },
      error: err=> {
        console.log(err);
      }
    })
  }

  modalClose() {
    this.bsModalRef.hide();
  }


  approveLeaveRequest() {
    this.loading = true;
    if(this.authService.currentUserValue.role=='Admin') {
      this.leaveService.approveFinalLeaveRequest(this.leaveRequestId).subscribe({
        next: response=> {
          if(response.success == true) {
            this.toastr.success('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
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
    } else {
      this.leaveService.approveLeaveRequestByReviewer(this.leaveRequestId).subscribe({
        next: response=> {
          if(response.success == true) {
            this.toastr.success('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
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
    }
  }
  denyLeaveRequest() {
    this.loading = true;
    if(this.authService.currentUserValue.role=='Admin') {
      this.leaveService.denyFinalLeaveRequest(this.leaveRequestId).subscribe({
        next: response=> {
          if(response.success == true) {
            this.toastr.success('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
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
    } else {
      this.leaveService.denyLeaveRequestByReviewer(this.leaveRequestId).subscribe({
        next: response=> {
          if(response.success == true) {
            this.toastr.success('',`${response.message}`, {
              positionClass: 'toast-top-right'
            })
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
    }
  }
}

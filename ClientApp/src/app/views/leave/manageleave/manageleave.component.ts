import { Component, OnDestroy, OnInit } from '@angular/core';
import {ManageLeaveService} from '../service/manage-leave.service'
import { Subscription } from 'rxjs';
import { deepObjectsMerge } from '@coreui/utils';
import { LeaveModel } from '../models/leave-model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { LeaveDetailViewComponent } from './leave-detail-view/leave-detail-view.component';
@Component({
  selector: 'app-manageleave',
  templateUrl: './manageleave.component.html',
  styleUrl: './manageleave.component.scss'
})
export class ManageleaveComponent implements OnInit, OnDestroy {
  loading: boolean ;
  DepartmentOption: any[] = [];
  subscription: Subscription = new Subscription();
  selectedDepartment: number|null;
  leaves: any[] = [];
  leaveStatusOptions: any [] = [];
  selectedLeave: LeaveModel;

  constructor(
    public leaveService: ManageLeaveService,
    private modalService: BsModalService
  ) {
    this.loading = false;
    this.selectedDepartment = null;
    this.selectedLeave = new LeaveModel();
  }


  ngOnInit(): void {
    this.getDepartmentOption();
    this.getLeaves();

    this.leaveService.getLeaveStatusOption().subscribe( {
      next: option =>  {
        this.leaveStatusOptions = option;
      }
    })
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getDepartmentOption() {
    this.leaveService.getSelectedDepartment().subscribe({
      next: option => {
        this.DepartmentOption = option;
      }
    }) 
  }

  getLeaves() {
    this.leaveService.getLeaves().subscribe({
      next: response=> {
        this.leaves = response;
      
      },
      error: error=> {
        console.log(error);
      }
    })
  }

  selectLeave(leave:LeaveModel) {
    this.selectedLeave = leave;
  }


  onViewDetail(leaveRequestId:number) {

    interface LeaveDetailViewModalConfig {
      leaveRequestId: number;
    }
    const initialState:LeaveDetailViewModalConfig = {
      leaveRequestId : leaveRequestId 
    };
    const modalRef: BsModalRef = this.modalService.show(LeaveDetailViewComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      modalRef.onHide.subscribe(() => {
        this.getLeaves();
      });
    }
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import {ManageLeaveService} from '../service/manage-leave.service'
import { Subscription } from 'rxjs';
import { deepObjectsMerge } from '@coreui/utils';
import { LeaveModel } from '../models/leave-model';
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
  leaveOptions: any [] = [];
  selectedLeave: LeaveModel;

  constructor(
    public leaveService: ManageLeaveService
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
        this.leaveOptions = option;
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
        console.log(response);
      },
      error: error=> {
        console.log(error);
      }
    })
  }

  selectLeave(leave:LeaveModel) {
    this.selectedLeave = leave;
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { LeaveBalanceService } from '../service/leave-balance.service';

@Component({
  selector: 'app-leave-balance',
  templateUrl: './leave-balance.component.html',
  styleUrl: './leave-balance.component.scss'
})
export class LeaveBalanceComponent implements OnInit, OnDestroy {
  
  subscription: Subscription = new Subscription();
  empId: number | null;
  IdCardNo: string;
  leaveBalances: any[] = [];
  loading: boolean ;
  cols = [{header: "Leave Type Name", field: "leaveTypeName"}, {header: "Leave Due", field:"leaveDue"}, {header: "Total Leave", field: "totalAmount"}]
  empName:string;

  constructor(
    private authService: AuthService,
    private leaveBalanceService: LeaveBalanceService
  ) {
    this.empId = null;
    this.IdCardNo = "";
    this.loading = false;
    this.empName = "";
  }

  ngOnInit(): void {
    this.empId = parseInt(this.authService.currentUserValue.empId);
    this.subscription=this.leaveBalanceService.getEmpInfo(this.empId).subscribe({
      next: response => {
        this.IdCardNo = response.idCardNo;
        this.empName = response.firstName+' '+response.lastName;
      }
    })

    this.getLeaveBalance(this.empId);
  }

  onEmpCardChange() {
    this.subscription = this.leaveBalanceService.getEmpInfoByCardNo(this.IdCardNo).subscribe({
      next: (response) => {
        console.log(response)
        if(response==null) {
          this.leaveBalances = [];
          return;
        }
        this.empName = response.firstName+' '+response.lastName;
        this.subscription = this.subscription = this.leaveBalanceService.getLeaveBalance(response.id).subscribe({
          next: response => {
            console.log(response)
            this.leaveBalances = response;
          },
          error: err=> {
            this.leaveBalances = [];
          }
        })
      }
    })
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  getLeaveBalance(empId:number) {
    this.loading = true;
    this.subscription = this.leaveBalanceService.getLeaveBalance(empId).subscribe({
      next: response => {
        this.leaveBalances = response;        
      },
      error: err => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    })
  }
}

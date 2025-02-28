import { Component, OnDestroy, OnInit } from '@angular/core';
import { delay, of, Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { LeaveBalanceService } from '../service/leave-balance.service';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-leave-balance',
  templateUrl: './leave-balance.component.html',
  styleUrl: './leave-balance.component.scss'
})
export class LeaveBalanceComponent implements OnInit, OnDestroy {
  
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  empId: number | null;
  IdCardNo: string;
  leaveBalances: any[] = [];
  loading: boolean ;
  cols = [{header: "Type", field: "leaveTypeName"}, {header: "Total Leave", field:"totalAmount"}, {header: "Availed", field: "availed"},{header:"Balance",field:"leaveDue"},{header: "Applied", field: "applied"}]
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

    if(isNaN(this.empId))
      return;
    // this.subscription=
    this.subscription.push(
      this.leaveBalanceService.getEmpInfo(this.empId).subscribe({
      next: response => {
        this.IdCardNo = response.idCardNo;
        this.empName = response.firstName+' '+response.lastName;
      }
    })
    )
    this.getLeaveBalance(this.empId);
  }

  onEmpCardChange() {

    if(this.IdCardNo.trim()=="") {
      this.leaveBalances = [];
      this.empId = null;
      this.empName = "";
      return;
    }

    const source$ = of (this.IdCardNo).pipe(
      delay(700)
    );

    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }

    // this.subscription = 
    this.subscription.push(
      source$.subscribe(data => {
      this.leaveBalanceService.getEmpInfoByCardNo(this.IdCardNo).subscribe({
        next: (response) => {
          if(response==null) {
            this.leaveBalances = [];
            this.empId = null;
            this.empName = "";
            return;
          }
          this.empName = response.firstName+' '+response.lastName;
          this.empId = response.id;
          this.getLeaveBalance(response.id);
          //this.subscription = this.subscription = this.leaveBalanceService.getLeaveBalance(response.id).subscribe({
          //  next: response => {
          //    this.leaveBalances = response;
          //  },
          //  error: err=> {
          //    this.leaveBalances = [];
          //  }
          //})
        },
        error: err => {
          this.empId = null;
          this.leaveBalances = [];
          this.empName = "";
        }
      })

    })
    )
    
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    } 
  }

  getLeaveBalance(empId:number) {
    this.loading = true;
    let params = new HttpParams();
    params = params.set('empId',empId);
    this.subscription.push(
      this.leaveBalanceService.getLeaveBalance(params).subscribe({
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
    )
    
  }
}

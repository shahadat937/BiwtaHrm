import { Component, OnDestroy, OnInit } from '@angular/core';
import { LeaveStatus } from '../enum/leave-status';
import { AuthService } from 'src/app/core/service/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-final-approval',
  templateUrl: './final-approval.component.html',
  styleUrl: './final-approval.component.scss'
})
export class FinalApprovalComponent implements OnInit, OnDestroy {
  filterLeave: any;
  Role: string = "Approver";
  CanApprove : boolean = true;
  subscription: Subscription[] = [];

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.filterLeave = {Status: [LeaveStatus.ReviewerApproved,LeaveStatus.FinalApproved,LeaveStatus.FinalDenied]}

    const subs = this.authService.currentUser.subscribe(user => {
      if(user==null) {
        return;
      }
      let empId = parseInt(user.empId);

      if(!isNaN(empId)) {
        this.filterLeave['approvedBy'] = empId;
      }
    })

    const subs1 = this.route.queryParams.subscribe(params => {
      if(params['forNotificationId']) {
        this.filterLeave['leaveRequestId'] = params['forNotificationId'];
      } else if(this.filterLeave['leaveRequestId']) {
        const {leaveRequestId, ...obj} = this.filterLeave;
        this.filterLeave = obj;
      }
    })
    this.subscription.push(subs);
    this.subscription.push(subs1);
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.subscription.forEach(subs => subs.unsubscribe());
  }
}

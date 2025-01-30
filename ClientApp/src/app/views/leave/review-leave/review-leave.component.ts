import { Component, OnDestroy, OnInit } from '@angular/core';
import { LeaveStatus } from '../enum/leave-status';
import { Leave } from '../../basic-setup/model/Leave';
import { AuthService } from 'src/app/core/service/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-review-leave',
  templateUrl: './review-leave.component.html',
  styleUrl: './review-leave.component.scss'
})
export class ReviewLeaveComponent implements OnInit, OnDestroy {
  filterLeave: any;
  Role: string = "Reviewer";
  CanApprove : boolean = true;
  subscription: Subscription[] = [];

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {

    this.filterLeave = {Status: [LeaveStatus.Pending,LeaveStatus.ReviewerApproved,LeaveStatus.ReviewerDenied,LeaveStatus.FinalApproved,LeaveStatus.FinalDenied]}
    const subs = this.authService.currentUser.subscribe(user => {
      if(user==null) {
        return;
      }
      let empId = parseInt(user.empId);
      if(!isNaN(empId)) {
        this.filterLeave['reviewedBy'] = parseInt(user.empId);
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

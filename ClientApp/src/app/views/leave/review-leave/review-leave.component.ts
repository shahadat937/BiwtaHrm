import { Component } from '@angular/core';
import { LeaveStatus } from '../enum/leave-status';
import { Leave } from '../../basic-setup/model/Leave';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-review-leave',
  templateUrl: './review-leave.component.html',
  styleUrl: './review-leave.component.scss'
})
export class ReviewLeaveComponent {
  filterLeave: any;
  Role: string = "Reviewer";
  CanApprove : boolean = true;

  constructor(private authService: AuthService) {

    this.filterLeave = {Status: [LeaveStatus.Pending,LeaveStatus.ReviewerApproved,LeaveStatus.ReviewerDenied,LeaveStatus.FinalApproved,LeaveStatus.FinalDenied]}
    this.authService.currentUser.subscribe(user => {

      let empId = parseInt(user.empId);
      if(!isNaN(empId)) {
        this.filterLeave['reviewedBy'] = parseInt(user.empId);
      }
    })
  }
}

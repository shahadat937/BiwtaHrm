import { Component } from '@angular/core';
import { LeaveStatus } from '../enum/leave-status';
import { Leave } from '../../basic-setup/model/Leave';

@Component({
  selector: 'app-review-leave',
  templateUrl: './review-leave.component.html',
  styleUrl: './review-leave.component.scss'
})
export class ReviewLeaveComponent {
  filterLeave: any;
  Role: string = "Reviewer";
  CanApprove : boolean = true;

  constructor() {
    this.filterLeave = {Status: [LeaveStatus.Pending,LeaveStatus.ReviewerApproved,LeaveStatus.ReviewerDenied]}
  }
}

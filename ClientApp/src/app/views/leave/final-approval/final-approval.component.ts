import { Component } from '@angular/core';
import { LeaveStatus } from '../enum/leave-status';

@Component({
  selector: 'app-final-approval',
  templateUrl: './final-approval.component.html',
  styleUrl: './final-approval.component.scss'
})
export class FinalApprovalComponent {
  filterLeave: any;
  Role: string = "Approver";
  CanApprove : boolean = true;

  constructor() {
    this.filterLeave = {Status: [LeaveStatus.ReviewerApproved,LeaveStatus.FinalApproved,LeaveStatus.FinalDenied]}
  }
}

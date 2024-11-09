import { Component } from '@angular/core';
import { LeaveStatus } from '../enum/leave-status';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-final-approval',
  templateUrl: './final-approval.component.html',
  styleUrl: './final-approval.component.scss'
})
export class FinalApprovalComponent {
  filterLeave: any;
  Role: string = "Approver";
  CanApprove : boolean = true;

  constructor(private authService: AuthService) {
    this.filterLeave = {Status: [LeaveStatus.ReviewerApproved,LeaveStatus.FinalApproved,LeaveStatus.FinalDenied]}

    this.authService.currentUser.subscribe(user => {
      if(user==null) {
        return;
      }
      let empId = parseInt(user.empId);

      if(!isNaN(empId)) {
        this.filterLeave['approvedBy'] = empId;
      }
    })
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-personal-leave',
  templateUrl: './personal-leave.component.html',
  styleUrl: './personal-leave.component.scss'
})
export class PersonalLeaveComponent implements OnInit, OnDestroy {
  filterLeave: any;
  subscription: Subscription[]=[]
  Role: string = "User";
  CanApprove : boolean = false;
  refreshLink : string | null;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.refreshLink = null;
  }
  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    } 
  }

  ngOnInit(): void {

    this.subscription.push(
      this.authService.currentUser.subscribe(user => {
      let empId = user && user.empId != null? user.empId: 0;
      this.filterLeave = {empId:empId};
    })
    )

    this.subscription.push(
      this.route.queryParams.subscribe((params) => {
        if(params['forNotificationId']) {
          this.filterLeave['leaveRequestId']=params['forNotificationId'];
          this.refreshLink = '/leave/personalleave'
        } else if(this.filterLeave['leaveRequestId']) {
          const {leaveRequestId, ...obj} = this.filterLeave;
          this.refreshLink = null;
          this.filterLeave = obj;
      }
      })
    );
    
  }
}

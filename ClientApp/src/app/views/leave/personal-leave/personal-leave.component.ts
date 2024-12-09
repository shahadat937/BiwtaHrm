import { Component, OnDestroy, OnInit } from '@angular/core';
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

  constructor(
    private authService: AuthService
  ) {
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
    
  }
}

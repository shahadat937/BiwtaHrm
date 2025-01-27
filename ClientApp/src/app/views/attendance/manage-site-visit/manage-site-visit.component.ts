import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscribable, Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-manage-site-visit',
  templateUrl: './manage-site-visit.component.html',
  styleUrl: './manage-site-visit.component.scss'
})
export class ManageSiteVisitComponent implements OnInit, OnDestroy {
  IsUser = true;
  filter = {};
  subscriptions: Subscription[] = [];
  
  constructor(
    private authService: AuthService
  ) {
    this.authService.currentUser.subscribe(user => {
      let empId = user && user.empId != null? user.empId: 0
      this.filter = {EmpId: empId};
    }) 
  }

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(subs => subs.unsubscribe());
  }


}

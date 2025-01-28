import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscribable, Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';

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
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private roleFeatureService: RoleFeatureService
  ) {
    this.authService.currentUser.subscribe(user => {
      let empId = user && user.empId != null? user.empId: 0
      this.filter = {EmpId: empId};
    }) 
  }

  ngOnInit(): void {
    
  }


  getPermission(){
    this.subscriptions.push(
    this.roleFeatureService.getFeaturePermission('manageSiteVisit').subscribe((item) => {
      //this.featurePermission = item;
      if(item.viewStatus == false){
        this.router.navigate(['/dashboard']);
        this.roleFeatureService.unauthorizeAccress();
      }
    })
    )
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(subs => subs.unsubscribe());
  }


}

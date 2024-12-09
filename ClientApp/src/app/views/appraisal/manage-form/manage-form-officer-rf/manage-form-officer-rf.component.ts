import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormRecordFilter } from '../../models/form-record-filter';
import { filter, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/core/service/auth.service';
import { AppraisalRole } from '../../enum/appraisal-role';

@Component({
  selector: 'app-manage-form-officer-rf',
  templateUrl: './manage-form-officer-rf.component.html',
  styleUrl: './manage-form-officer-rf.component.scss'
})
export class ManageFormOfficerRfComponent implements OnInit, OnDestroy{
  filters: FormRecordFilter
  appraisalRole = AppraisalRole
  subscription: Subscription[]=[];

  constructor(
    private authService: AuthService
  ) {
    this.filters = new FormRecordFilter();
    //this.filters.formId = environment.officerFormId;
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  ngOnInit(): void {
    this.subscription.push(
      this.authService.currentUser.subscribe(user => {
      const userId = user.empId;

      if(userId!=null) {
        this.filters.reporterId = parseInt(userId)
      }
    })
    )
    
  }


}

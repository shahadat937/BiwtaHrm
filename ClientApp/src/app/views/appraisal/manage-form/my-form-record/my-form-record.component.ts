import { Component, OnDestroy, OnInit } from '@angular/core';
import {FormRecordFilter} from '../../models/form-record-filter';
import { AppraisalRole } from '../../enum/appraisal-role';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { textChangeRangeIsUnchanged } from 'typescript';

@Component({
  selector: 'app-my-form-record',
  templateUrl: './my-form-record.component.html',
  styleUrl: './my-form-record.component.scss'
})
export class MyFormRecordComponent implements OnInit, OnDestroy {
  filters: FormRecordFilter;
  appraisalRole = AppraisalRole;
  subscriptions: Subscription[] = [];

  constructor(
    private authService: AuthService
  ) {
    this.filters = new FormRecordFilter();
  }

  ngOnInit(): void {
    const subs = this.authService.currentUser.subscribe(user => {
      if(user&&user.empId) {
        const empId = parseInt(user.empId);

        if(empId==undefined) {
          this.filters.empId = 0;
          return;
        }

        this.filters.empId = empId;
      } else {
        this.filters.empId = 0;
      }
    })

    this.subscriptions.push(subs);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription)=> {
      subscription.unsubscribe();
    })
  }
}

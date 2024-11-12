import { Component, OnInit } from '@angular/core';
import { FormRecordFilter } from '../../models/form-record-filter';
import { filter } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/core/service/auth.service';
import { AppraisalRole } from '../../enum/appraisal-role';

@Component({
  selector: 'app-manage-form-officer-rf',
  templateUrl: './manage-form-officer-rf.component.html',
  styleUrl: './manage-form-officer-rf.component.scss'
})
export class ManageFormOfficerRfComponent implements OnInit{
  filters: FormRecordFilter
  appraisalRole = AppraisalRole

  constructor(
    private authService: AuthService
  ) {
    this.filters = new FormRecordFilter();
    this.filters.formId = environment.officerFormId;
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(user => {
      const userId = user.empId;

      if(userId!=null) {
        this.filters.reporterId = parseInt(userId)
      }
    })
  }


}

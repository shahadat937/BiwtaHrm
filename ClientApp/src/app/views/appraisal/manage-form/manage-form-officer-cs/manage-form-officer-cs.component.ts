import { Component } from '@angular/core';
import { AuthService } from 'src/app/core/service/auth.service';
import { FormRecordFilter } from '../../models/form-record-filter';
import { environment } from 'src/environments/environment';
import { AppraisalRole } from '../../enum/appraisal-role';

@Component({
  selector: 'app-manage-form-officer-cs',
  templateUrl: './manage-form-officer-cs.component.html',
  styleUrl: './manage-form-officer-cs.component.scss'
})
export class ManageFormOfficerCsComponent {
  filters: FormRecordFilter
  appraisalRole = AppraisalRole
  haveError: boolean;
  constructor(
    private authService: AuthService
  ) {
    this.filters = new FormRecordFilter();
    //this.filters.formId = environment.officerFormId;
    this.filters.reportingOfficerApproval = true
    this.haveError = false;
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(user => {
      const userId = user.empId;

      if(userId!=null) {
        this.filters.counterSignatureId = parseInt(userId)
      }
    })
  }
}

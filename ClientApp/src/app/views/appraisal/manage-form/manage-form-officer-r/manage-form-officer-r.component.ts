import { Component } from '@angular/core';
import { FormRecordFilter } from '../../models/form-record-filter';
import { AuthService } from 'src/app/core/service/auth.service';
import { environment } from 'src/environments/environment';
import { AppraisalRole } from '../../enum/appraisal-role';

@Component({
  selector: 'app-manage-form-officer-r',
  templateUrl: './manage-form-officer-r.component.html',
  styleUrl: './manage-form-officer-r.component.scss'
})
export class ManageFormOfficerRComponent {
  filters: FormRecordFilter
  appraisalRole = AppraisalRole

  constructor(
    private authService: AuthService
  ) {
    this.filters = new FormRecordFilter();
    //this.filters.formId = environment.officerFormId;
    this.filters.counterSignatoryApproval = true;
  }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(user => {
      const userId = user.empId;

      if(userId!=null) {
        this.filters.receiverId = parseInt(userId)
      }
    })
  }
}

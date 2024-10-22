import { Component } from '@angular/core';
import { FormRecordFilter } from '../../models/form-record-filter';
import { AuthService } from 'src/app/core/service/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-manage-form-officer-r',
  templateUrl: './manage-form-officer-r.component.html',
  styleUrl: './manage-form-officer-r.component.scss'
})
export class ManageFormOfficerRComponent {
  filters: FormRecordFilter

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
        this.filters.receiverId = parseInt(userId)
      }
    })
  }
}

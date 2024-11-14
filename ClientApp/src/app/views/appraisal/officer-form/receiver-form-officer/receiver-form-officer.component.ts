import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppraisalRole } from '../../enum/appraisal-role';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-receiver-form-officer',
  templateUrl: './receiver-form-officer.component.html',
  styleUrl: './receiver-form-officer.component.scss'
})
export class ReceiverFormOfficerComponent implements OnInit {
  ActiveSection: boolean[] = [false,false,false,false,false,false,true]
  ActiveSectionForStaff: boolean[] = [false,false,false,true]
  formRecordId: number = 0
  officerFormId: number = 0;
  staffFormId : number = 0;
  formId: number = 0;
  appraisalRole = AppraisalRole

  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.officerFormId = environment.officerFormId;
    this.staffFormId = environment.staffFormId;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const recordId = params.get('formRecordId');
      const formId = params.get('formId');
      this.formRecordId = recordId == null? 0: parseInt(recordId);
      this.formId = formId == null?0:parseInt(formId);
    })
  }
}

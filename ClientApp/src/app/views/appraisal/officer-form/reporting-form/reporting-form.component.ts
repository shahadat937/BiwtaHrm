import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AppraisalRole } from '../../enum/appraisal-role';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-reporting-form',
  templateUrl: './reporting-form.component.html',
  styleUrl: './reporting-form.component.scss'
})
export class ReportingFormComponent implements OnInit {
  ActiveSection: boolean[] = [false,true,true,true,true,false,false];
  ActiveSectionForStaff: boolean[] = [false,true,true,true];
  formRecordId: number = 0;
  officerFormId: number = 0;
  staffFormId: number = 0;
  formId: number = 0;
  appraisalRole = AppraisalRole
  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.officerFormId = environment.officerFormId;
    this.staffFormId = environment.staffFormId;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const recordId = params.get('formRecordId');
      const formId = params.get("formId");
      this.formRecordId = recordId == null? 0 : parseInt(recordId);
      this.formId = formId == null? 0 : parseInt(formId);
    })
  }


}
